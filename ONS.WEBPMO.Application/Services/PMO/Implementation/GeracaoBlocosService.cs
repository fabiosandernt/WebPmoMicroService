using ONS.Common.Exceptions;
using ONS.Common.Services.Impl;
using ONS.Common.Util.Files;
using ONS.Infra.Core.Extensions;
using ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Entities.Resources;
using ONS.WEBPMO.Domain.Enumerations;
using ONS.WEBPMO.Domain.Repository;
using System.Text;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{

    public class GeracaoBlocosService : Service, IGeracaoBlocosService
    {
        private readonly IColetaInsumoService coletaInsumoService;

        private readonly ISemanaOperativaRepository semanaOperativaRepository;
        private readonly IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository;
        private readonly IDadoColetaEstruturadoRepository dadoColetaEstruturadoRepository;
        private readonly IDadoColetaManutencaoRepository dadoColetaManutencaoRepository;

        public GeracaoBlocosService(
            ISemanaOperativaRepository semanaOperativaRepository,
            IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository,
            IDadoColetaEstruturadoRepository dadoColetaEstruturadoRepository,
            IDadoColetaManutencaoRepository dadoColetaManutencaoRepository,
            IColetaInsumoService coletaInsumoService)
        {
            this.semanaOperativaRepository = semanaOperativaRepository;
            this.dadoColetaNaoEstruturadoRepository = dadoColetaNaoEstruturadoRepository;
            this.dadoColetaEstruturadoRepository = dadoColetaEstruturadoRepository;
            this.dadoColetaManutencaoRepository = dadoColetaManutencaoRepository;
            this.coletaInsumoService = coletaInsumoService;
        }

        public void GerarBlocos(int idSemanaOperativa, byte[] versao, bool SomenteAprovados)
        {
            SemanaOperativa semanaOperativa = semanaOperativaRepository
                .FindByKeyConcurrencyValidate(idSemanaOperativa, versao);

            IList<string> mensagens = new List<string>();
            if (SomenteAprovados)
                ValidarSemanaOperativaEmGeracaoBlocosInsumosAprovados(semanaOperativa, mensagens);
            else
            {
                ValidarExisteGeracaoBlocos(semanaOperativa, mensagens);
                ValidarSemanaOperativaEmGeracaoBlocos(semanaOperativa, mensagens);
            }
            VerificarONSBusinessException(mensagens);

            IList<DadoColetaBloco> dadosColetaBloco = coletaInsumoService.ConsultarDadosColetaParaGeracaoBloco(idSemanaOperativa);

            if (SomenteAprovados && dadosColetaBloco.Where(x => x.ColetaInsumo.SituacaoId == 5).Count() == 0)
            {
                mensagens.Add(SGIPMOMessages.MS066);
                VerificarONSBusinessException(mensagens);
            }

            ONS.WEBPMO.Domain.DTO.DadosSemanaOperativaDTO dtoAbrirOuFecharColeta = new ONS.WEBPMO.Domain.DTO.DadosSemanaOperativaDTO()
            {
                IdSemanaOperativa = idSemanaOperativa,
                VersaoSemanaOperativa = versao
            };
            if (SomenteAprovados)
            {
                coletaInsumoService.DeletarArquivos(dtoAbrirOuFecharColeta);
            }

            List<CompressableFile> arquivosCompressable = new List<CompressableFile>();
            arquivosCompressable.AddRange(GerarArquivosPorAgentePerfil(semanaOperativa, dadosColetaBloco, SomenteAprovados));
            arquivosCompressable.AddRange(GerarArquivoGNL(semanaOperativa, dadosColetaBloco, SomenteAprovados));
            arquivosCompressable.AddRange(GerarArquivosInsumoNaoEstruturado(semanaOperativa));

            if (arquivosCompressable.Count() == 0)
            {
                mensagens.Add(SGIPMOMessages.MS066);
                VerificarONSBusinessException(mensagens);
            }

            CompressedFileMemory compressed = Compression.CompactarArquivosEmMemoria(arquivosCompressable, semanaOperativa.Nome);
            byte[] zipData = compressed.Content;

            Arquivo arquivo = new Arquivo
            {
                Content = new BinaryData { Data = zipData },
                MimeType = "application/zip",
                Nome = string.Format("{0}.zip", semanaOperativa.Nome),
                Tamanho = zipData.Length,
                HashVerificacao = FileUtil.GetMD5Hash(zipData),
                Id = Guid.NewGuid()
            };

            ArquivoSemanaOperativa arquivoSemanaOperativa = new ArquivoSemanaOperativa();
            arquivoSemanaOperativa.Arquivo = arquivo;
            arquivoSemanaOperativa.IsPublicado = false;
            arquivoSemanaOperativa.Situacao = semanaOperativa.Situacao;
            arquivoSemanaOperativa.ArquivoId = arquivo.Id;

            semanaOperativa.Arquivos.Add(arquivoSemanaOperativa);
            semanaOperativa.DataHoraAtualizacao = DateTime.Now;
        }

        private IList<CompressableFile> GerarArquivosInsumoNaoEstruturado(SemanaOperativa semanaOperativa)
        {
            IList<CompressableFile> arquivosCompressable = new List<CompressableFile>();

            ArquivosSemanaOperativaFilter filter = new ArquivosSemanaOperativaFilter
            {
                IdSemanaOperativa = semanaOperativa.Id,
                IsConsiderarInsumosDECOMP = true
            };

            var dadosPorAgenteGroup = dadoColetaNaoEstruturadoRepository
                .ObterDadosColetaNaoEstruturado(filter)
                .Where(d => d.Arquivos.Any(a => a.Nome.EndsWith(".txt")))
                .GroupBy(d => d.ColetaInsumo.Agente)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.ToList());

            foreach (var dadosPorAgente in dadosPorAgenteGroup)
            {
                IList<byte[]> byteArrayList = new List<byte[]>();

                var arquivosDadoColeta = dadosPorAgente.Value
                    .SelectMany(d => d.Arquivos.Where(a => a.Nome.EndsWith(".txt")));

                foreach (Arquivo arquivoDado in arquivosDadoColeta)
                {
                    byteArrayList.Add(arquivoDado.Content.Data);
                    byteArrayList.Add(Encoding.UTF8.GetBytes(Environment.NewLine));
                }

                int offset = 0;
                byte[] fullArray = new byte[byteArrayList.Sum(a => a.Length)];
                foreach (byte[] array in byteArrayList)
                {
                    Buffer.BlockCopy(array, 0, fullArray, offset, array.Length);
                    offset += array.Length;
                }

                string nomeAgente = dadosPorAgente.Key.Nome;
                ColetaInsumo coletaInsumo = dadosPorAgente.Value.First().ColetaInsumo;

                if (!string.IsNullOrEmpty(coletaInsumo.CodigoPerfilONS))
                {
                    nomeAgente = string.Format("{0}_{1}", dadosPorAgente.Key.Nome, coletaInsumo.CodigoPerfilONS);
                }

                string nomeArquivo = string.Format("{0}_{1:00}{2}_{3:yyyyMMddHHmm}_{4:000}.RV{5}",
                    nomeAgente,
                    semanaOperativa.PMO.MesReferencia,
                    semanaOperativa.PMO.AnoReferencia.ToString().Substring(2),
                    DateTime.Now.AddMinutes(1),
                    dadosPorAgente.Key.Id,
                    semanaOperativa.Revisao);

                arquivosCompressable.Add(new CompressableFile
                {
                    Content = fullArray,
                    Filename = nomeArquivo
                });
            }

            return arquivosCompressable;
        }

        private IList<CompressableFile> GerarArquivoGNL(SemanaOperativa semanaOperativa, IList<DadoColetaBloco> dadosColetaBloco, bool SomenteAprovados)
        {
            IList<CompressableFile> arquivosCompressable = new List<CompressableFile>();

            IList<DadoColetaEstruturado> dados = dadoColetaEstruturadoRepository
                .ConsultarDadosComInsumoEGrandezaParticipaBlocoGNL(semanaOperativa.Id);

            IList<DadoColetaBloco> dadosBlocoTg = dadosColetaBloco
                .Where(d => d.Insumo.TipoBloco == TipoBlocoEnum.TG.ToDescription())
                .ToList();

            if (dadosBlocoTg.Any())
            {
                ColetaInsumo coletaInsumo = dadosBlocoTg.First().ColetaInsumo;
                CompressableFile compressable = GerarArquivo(
                    coletaInsumo.Agente,
                    coletaInsumo.CodigoPerfilONS,
                    semanaOperativa,
                    dados.ToList<DadoColeta>(),
                    dadosBlocoTg,
                    SomenteAprovados);

                if (compressable != null)
                {
                    arquivosCompressable.Add(compressable);
                }
            }

            return arquivosCompressable;
        }

        private IList<CompressableFile> GerarArquivosPorAgentePerfil(SemanaOperativa semanaOperativa, IList<DadoColetaBloco> dadosColetaBloco, bool SomenteAprovados)
        {
            IList<CompressableFile> arquivosCompressable = new List<CompressableFile>();

            List<DadoColeta> dadosColeta = new List<DadoColeta>();
            dadosColeta.AddRange(dadoColetaManutencaoRepository.ConsultarDadosComInsumoParticipaBlocoMP(semanaOperativa.Id));
            dadosColeta.AddRange(dadoColetaEstruturadoRepository.ConsultarDadosComInsumoEGrandezaParticipaBloco(semanaOperativa.Id));

            /* Bloco TG deve ser gerado em um arquivo separado */
            var dadosBlocoGroup = dadosColetaBloco
                .Where(d => d.Insumo.TipoBloco != TipoBlocoEnum.TG.ToDescription())
                .GroupBy(dado => new { dado.ColetaInsumo.Agente, dado.ColetaInsumo.CodigoPerfilONS }); ;

            foreach (var dadosPorAgente in dadosBlocoGroup)
            {
                var dadosColetados = dadosColeta
                    .Where(d => d.ColetaInsumo.AgenteId == dadosPorAgente.Key.Agente.Id
                        && d.ColetaInsumo.CodigoPerfilONS == dadosPorAgente.Key.CodigoPerfilONS)
                    .ToList();

                CompressableFile compressable = GerarArquivo(
                    dadosPorAgente.Key.Agente,
                    dadosPorAgente.Key.CodigoPerfilONS,
                    semanaOperativa,
                    dadosColetados,
                    dadosPorAgente.ToList(),
                    SomenteAprovados);

                if (compressable != null)
                {
                    arquivosCompressable.Add(compressable);
                }
            }

            return arquivosCompressable;
        }

        private CompressableFile GerarArquivo(Agente agente, string codigoPerfilOns, SemanaOperativa semanaOperativa,
            IList<DadoColeta> dadosColeta, IList<DadoColetaBloco> dadosColetaBloco, bool SomenteAprovados)
        {
            ArquivoMontador montador = new ArquivoMontador(agente, codigoPerfilOns, semanaOperativa, dadosColeta, dadosColetaBloco, SomenteAprovados);

            string dadosArquivo = montador.GerarArquivo();

            CompressableFile file = null;

            if (dadosArquivo.FindAll(Environment.NewLine).Count() > 1)
            {
                byte[] conteudoArquivo = Encoding.UTF8.GetBytes(dadosArquivo);

                file = new CompressableFile
                {
                    Content = conteudoArquivo,
                    Filename = montador.Nome
                };
            }

            return file;
        }

        #region Validações
        private void VerificarONSBusinessException(IList<string> mensagens)
        {
            if (mensagens.Any())
            {
                throw new ONSBusinessException(mensagens);
            }
        }

        /// <summary>
        /// Valida a situação do Estudo.
        /// Caso o estudo selecionado não esteja no estado/processo "Geração de Blocos"
        /// o sistema não deverá permitir gerar arquivo de blocos. [RS_MENS_046]
        /// </summary>
        /// <param name="semanaOperativa">Semana Operativa a ser validada</param>
        /// <param name="mensagens">Lista de mensagens de erro</param>
        private void ValidarSemanaOperativaEmGeracaoBlocos(SemanaOperativa semanaOperativa, IList<string> mensagens)
        {
            SituacaoSemanaOperativaEnum situacao = (SituacaoSemanaOperativaEnum)semanaOperativa.Situacao.IdTpsituacaosemanaoper;
            if (situacao != SituacaoSemanaOperativaEnum.GeracaoBlocos)
            {
                mensagens.Add(SGIPMOMessages.MS046);
            }
        }

        /// <summary>
        /// Valida a situação do Estudo.
        /// Caso o estudo selecionado não esteja no estado/processo "Coleta de Dados"
        /// o sistema não deverá permitir gerar arquivo de blocos. [RS_MENS_046]
        /// </summary>
        /// <param name="semanaOperativa">Semana Operativa a ser validada</param>
        /// <param name="mensagens">Lista de mensagens de erro</param>
        private void ValidarSemanaOperativaEmGeracaoBlocosInsumosAprovados(SemanaOperativa semanaOperativa, IList<string> mensagens)
        {
            if (semanaOperativa.Situacao.IdTpsituacaosemanaoper != (int)SituacaoSemanaOperativaEnum.ColetaDados)
            {
                mensagens.Add(SGIPMOMessages.MS023);
            }
        }

        /// <summary>
        /// O sistema não deverá permitir gerar arquivo de blocos se ele já tiver sido gerado. 
        /// Para efetuar nova geração do arquivo é necessário excluir a versão atual, </summary>
        /// reabrindo a coleta de dados do estudo. [RS_MENS_047]
        /// <param name="semanaOperativa">Semana Operativa a ser validada</param>
        /// <param name="mensagens">Lista de mensagens de erro</param>
        private void ValidarExisteGeracaoBlocos(SemanaOperativa semanaOperativa, IList<string> mensagens)
        {
            if (semanaOperativa.Arquivos.Any(arquivo => arquivo.Situacao.IdTpsituacaosemanaoper == (int)SituacaoSemanaOperativaEnum.GeracaoBlocos))
            {
                mensagens.Add(SGIPMOMessages.MS047);
            }
        }

        #endregion

    }
}
