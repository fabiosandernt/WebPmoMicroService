using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;
using ONS.WEBPMO.Domain.Enumerations;
using ONS.WEBPMO.Domain.Repository;

namespace ONS.WEBPMO.Domain.Presentations.Impl
{
    public class ColetaInsumoPresentation : IColetaInsumoPresentation
    {
        private readonly IAgenteService agenteService;
        private readonly IInsumoService insumoService;
        private readonly ISituacaoColetaInsumoRepository situacaoColetaInsumoRepository;
        private readonly IOrigemColetaService origemColetaService;
        private readonly IDadoColetaManutencaoService dadoColetaManutencaoService;
        private readonly ISGIService SGIService;
        private readonly IColetaInsumoRepository coletaInsumoRepository;
        private readonly ISemanaOperativaRepository semanaOperativaRepository;
        private readonly IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository;
        private readonly ISGIService sgiService;
        private readonly IColetaInsumoService coletaInsumoService;
        private readonly IDadoColetaManutencaoService dadoColetaManutencao;
        private readonly IGabaritoRepository gabaritoRepository;

        public ColetaInsumoPresentation(
            IAgenteService agenteService,
            IInsumoService insumoService,
            ISituacaoColetaInsumoRepository situacaoColetaInsumoRepository,
            IOrigemColetaService origemColetaService,
            IDadoColetaManutencaoService dadoColetaManutencaoService,
            ISGIService SGIService,
            IColetaInsumoRepository coletaInsumoRepository,
            ISemanaOperativaRepository semanaOperativaRepository,
            IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository,
            ISGIService sgiService, IColetaInsumoService coletaInsumoService,
            IDadoColetaManutencaoService dadoColetaManutencao,
            IGabaritoRepository gabaritoRepository)
        {
            this.agenteService = agenteService;
            this.insumoService = insumoService;
            this.situacaoColetaInsumoRepository = situacaoColetaInsumoRepository;
            this.origemColetaService = origemColetaService;
            this.dadoColetaManutencaoService = dadoColetaManutencaoService;
            this.SGIService = SGIService;
            this.coletaInsumoRepository = coletaInsumoRepository;
            this.semanaOperativaRepository = semanaOperativaRepository;
            this.dadoColetaNaoEstruturadoRepository = dadoColetaNaoEstruturadoRepository;
            this.sgiService = sgiService;
            this.coletaInsumoService = coletaInsumoService;
            this.dadoColetaManutencao = dadoColetaManutencao;
            this.gabaritoRepository = gabaritoRepository;
        }

        public DadosPesquisaColetaInsumoDTO ObterDadosPesquisaColetaInsumo(
            int? idSemanaOperativa = null, bool isMonitorar = false, bool? ordernarListagens = true)
        {
            IList<Agente> agentes = new List<Agente>();
            IList<Insumo> insumos = new List<Insumo>();

            if (idSemanaOperativa.HasValue)
            {
                agentes = isMonitorar
                    ? agenteService.ConsultarAgentesParticipamGabarito(
                        new GabaritoParticipantesFilter { IdSemanaOperativa = idSemanaOperativa })
                    : agenteService.ConsultarAgentesParticipanteGabaritoRepresentadoUsuarioLogado(idSemanaOperativa);

                insumos = insumoService.ConsultarInsumosPorSemanaOperativaAgentes(
                    idSemanaOperativa.Value, agentes.Select(a => a.Id).ToArray());
            }

            IList<SituacaoColetaInsumo> situacoesColeta = situacaoColetaInsumoRepository.GetAll();

            DadosPesquisaColetaInsumoDTO dadosPesquisa = new DadosPesquisaColetaInsumoDTO();
            if (ordernarListagens.HasValue && ordernarListagens.Value)
            {
                dadosPesquisa.Agentes = agentes.Select(a => new ChaveDescricaoDTO<int>(a.Id, a.Nome)).OrderBy(o => o.Descricao).ToList();
                dadosPesquisa.Insumos = insumos.Select(i => new ChaveDescricaoDTO<int>(i.Id, i.Nome)).OrderBy(o => o.Descricao).ToList();
            }
            else
            {
                dadosPesquisa.Agentes = agentes.Select(a => new ChaveDescricaoDTO<int>(a.Id, a.Nome)).ToList();
                dadosPesquisa.Insumos = insumos.Select(i => new ChaveDescricaoDTO<int>(i.Id, i.Nome)).ToList();
            }
            dadosPesquisa.SituacoesColeta = situacoesColeta
                .Select(s => new ChaveDescricaoDTO<int>(s.Id, s.Descricao))
                .OrderBy(situacao => situacao.Descricao)
                .ToList();

            if (idSemanaOperativa.HasValue)
            {
                SemanaOperativa semanaOperativa = semanaOperativaRepository.FindByKey(idSemanaOperativa.Value);
                dadosPesquisa.NomeSemanaOperativaSituacao = string.Format("{0} - {1}", semanaOperativa.Nome,
                    semanaOperativa.Situacao.DscSituacaosemanaoper);
                dadosPesquisa.IsSemanaOperativaEmConfiguracao = semanaOperativa.Situacao.IdTpsituacaosemanaoper == (int)SituacaoSemanaOperativaEnum.Configuracao;
                dadosPesquisa.SemanasOperativas.Add(new ChaveDescricaoDTO<int>(semanaOperativa.Id, semanaOperativa.Nome));
                dadosPesquisa.VersaoStringSemanaOperativa = Convert.ToBase64String(semanaOperativa.Versao);
            }

            return dadosPesquisa;
        }

        public DadosInclusaoDadoColetaManutencaoDTO ObterDadosInclusaoDadoColetaManutencao(int idColetaInsumo)
        {
            DadosInclusaoDadoColetaManutencaoDTO dto = new DadosInclusaoDadoColetaManutencaoDTO();

            var usinas = origemColetaService.ConsultarUsinaParticipanteGabaritoPorColetaInsumo(idColetaInsumo);
            dto.Usinas = usinas.Select(usina => new ChaveDescricaoDTO<string>(usina.Id, usina.Nome)).ToList();
            dto.IdColetaInsumo = idColetaInsumo;

            return dto;
        }

        public DadosAlteracaoDadoColetaManutencaoDTO ObterDadosAlteracaoDadoColetaManutencao(int idDadoColeta)
        {
            DadoColetaManutencao dadoColeta = dadoColetaManutencaoService.ObterPorChave(idDadoColeta);

            DadosAlteracaoDadoColetaManutencaoDTO dto = new DadosAlteracaoDadoColetaManutencaoDTO();
            dto.IdColetaInsumo = dadoColeta.ColetaInsumo.Id;
            dto.VersaoColetaInsumo = dadoColeta.ColetaInsumo.Versao;
            dto.IdDadoColeta = dadoColeta.Id;
            dto.Justificativa = dadoColeta.Justificativa;
            dto.Numero = dadoColeta.Numero;
            dto.TempoRetorno = dadoColeta.TempoRetorno;
            dto.DataInicio = dadoColeta.DataInicio;
            dto.DataFim = dadoColeta.DataFim;
            dto.NomeUnidade = dadoColeta.Gabarito.OrigemColeta.Nome;

            if (dadoColeta.Gabarito.OrigemColeta is UnidadeGeradora)
            {
                UnidadeGeradora unidadeGeradora = (UnidadeGeradora)dadoColeta.Gabarito.OrigemColeta;
                dto.NomeUsina = unidadeGeradora.Usina.Nome;
            }

            return dto;
        }

        public IList<DadoColetaManutencao> ConsultarManutencaoSGI(int idColetaInsumo)
        {
            string[] idsUnidadesGeradoras = origemColetaService
                .ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumo(idColetaInsumo)
                .Select(unidade => unidade.Id)
                .ToArray();

            ColetaInsumo coletaInsumo = coletaInsumoRepository.FindByKey(idColetaInsumo);
            SemanaOperativa semanaOperativa = coletaInsumo.SemanaOperativa;
            IList<DadoColetaManutencao> dadoColetaList = SGIService.ObterManutencoesPorChaves(idsUnidadesGeradoras,
                semanaOperativa.DataInicioManutencao, semanaOperativa.DataFimManutencao);

            return dadoColetaList;
        }

        public DadosPesquisaGeracaoBlocosDTO ObterDadosPesquisaGeracaoBloco(int idSemanaOperativa)
        {
            DadosPesquisaGeracaoBlocosDTO dados = new DadosPesquisaGeracaoBlocosDTO();

            ArquivosSemanaOperativaFilter filtro = new ArquivosSemanaOperativaFilter
            {
                IdSemanaOperativa = idSemanaOperativa,
                IsConsiderarInsumosProcessamento = true
            };

            IList<DadoColetaNaoEstruturado> dadosNaoEstruturados = dadoColetaNaoEstruturadoRepository
                .ObterDadosColetaNaoEstruturado(filtro);

            foreach (DadoColetaNaoEstruturado dadoNaoEstruturado in dadosNaoEstruturados)
            {
                foreach (Arquivo arquivo in dadoNaoEstruturado.Arquivos)
                {
                    dados.ArquivosDadoNaoEstruturado.Add(new InsumoArquivoDTO
                    {
                        IdArquivo = arquivo.Id,
                        NomeArquivo = arquivo.Nome,
                        Tamanho = arquivo.Tamanho / 1024,
                        IdInsumo = dadoNaoEstruturado.ColetaInsumo.Insumo.Id,
                        NomeInsumo = dadoNaoEstruturado.ColetaInsumo.Insumo.Nome
                    });
                }
            }

            SemanaOperativa semanaOperativa = semanaOperativaRepository.FindByKey(filtro.IdSemanaOperativa);
            IList<ArquivoSemanaOperativa> arquivosGeracaoBlocos = semanaOperativa.Arquivos
                .Where(a => a.Situacao.IdTpsituacaosemanaoper == (int)SituacaoSemanaOperativaEnum.GeracaoBlocos || a.Situacao.IdTpsituacaosemanaoper == (int)SituacaoSemanaOperativaEnum.ColetaDados)
                .ToList();

            foreach (ArquivoSemanaOperativa arquivoSemanaOperativa in arquivosGeracaoBlocos)
            {
                int tamanho = arquivoSemanaOperativa.Arquivo.Tamanho / 1024;

                dados.ArquivosGeracaoBlocos.Add(new ArquivoDTO
                {
                    IdArquivo = arquivoSemanaOperativa.Arquivo.Id,
                    NomeArquivo = arquivoSemanaOperativa.Arquivo.Nome,
                    Tamanho = tamanho < 1 ? 1 : tamanho
                });
            }

            dados.SituacaoSemanaOperativa = (SituacaoSemanaOperativaEnum)semanaOperativa.Situacao.IdTpsituacaosemanaoper;

            return dados;
        }

        public void ImportarCronogramaManutencaoHidraulicaTermica(int idSemanaOperativa, IList<int> idsInsumos)
        {
            IList<ColetaInsumo> coletaInsumos = coletaInsumoRepository.ObterColetaInsumoPorSemanaOperativaInsumos(idSemanaOperativa, idsInsumos);

            if (coletaInsumos.Count > 0)
            {
                var semanaOperativa = coletaInsumos.FirstOrDefault().SemanaOperativa;

                List<UnidadeGeradoraManutencaoSGIDTO> listaUnidadesGeradoras = origemColetaService
                    .ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumos(coletaInsumos.Select(ci => ci.Id).ToList()).ToList();

                string[] idsUnidadesGeradoras = listaUnidadesGeradoras.Select(unidade => unidade.IdUnidadeGeradora).ToArray();

                IList<DadoColetaManutencao> dadoColetaList = SGIService.ObterManutencoesPorChaves(idsUnidadesGeradoras,
                    semanaOperativa.DataInicioManutencao, semanaOperativa.DataFimManutencao);

                foreach (var dadoColeta in dadoColetaList)
                {
                    UnidadeGeradoraManutencaoSGIDTO unidadeGeradoraManutencaoSGIDTO = listaUnidadesGeradoras.Where(ug => ug.IdUnidadeGeradora == dadoColeta.UnidadeGeradora.Id).FirstOrDefault();
                    ColetaInsumo coletaInsumo = coletaInsumos.Where(ci => ci.Id == unidadeGeradoraManutencaoSGIDTO.IdColetaInsumo).FirstOrDefault();

                    if (coletaInsumo != null)
                    {
                        coletaInsumo.SituacaoId = (int)SituacaoColetaInsumoEnum.Aprovado;
                        dadoColeta.ColetaInsumo = coletaInsumo;
                        dadoColeta.TipoDadoColeta = char.ToString((char)TipoDadoColetaEnum.Manutencao);
                        dadoColeta.Gabarito = gabaritoRepository.FindByKey(unidadeGeradoraManutencaoSGIDTO.IdGabarito);
                    }
                }

                dadoColetaManutencao.IncluirDadoColetaSeNaoExiste(dadoColetaList);

            }
        }
    }
}
