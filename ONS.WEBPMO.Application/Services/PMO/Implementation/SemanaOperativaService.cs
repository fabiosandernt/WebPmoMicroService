using ONS.Common.Configuration;
using ONS.Common.Exceptions;
using ONS.Common.Seguranca;
using ONS.Common.Services.Impl;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.DTO;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Entities.Resources;
using ONS.WEBPMO.Domain.Enumerations;
using ONS.WEBPMO.Domain.Repository;
using System.Globalization;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class SemanaOperativaService : Service, ISemanaOperativaService
    {
        private readonly ISemanaOperativaRepository semanaOperativaRepository;
        private readonly IParametroService parametroService;
        private readonly ISituacaoSemanaOperativaRepository situacaoSemanaOperativaRepository;
        private readonly ISituacaoColetaInsumoRepository situacaoColetaInsumoRepository;
        private readonly IGabaritoRepository gabaritoRepository;
        private readonly IColetaInsumoRepository coletaInsumoRepository;
        private readonly IPMORepository pmoRepository;
        private readonly INotificacaoService notificacaoService;
        private readonly IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository;
        private readonly IColetaInsumoService coletaInsumoService;
        private readonly IArquivoRepository arquivoRepository;
        private readonly ISharePointService sharePointService;
        private readonly IHistoricoService historicoService;
        private readonly IArquivoSemanaOperativaRepository arquivoSemanaOperativaRepository;

        public SemanaOperativaService(
            ISemanaOperativaRepository semanaOperativaRepository,
            IParametroService parametroService,
            ISituacaoSemanaOperativaRepository situacaoSemanaOperativaRepository,
            ISituacaoColetaInsumoRepository situacaoColetaInsumoRepository,
            IGabaritoRepository gabaritoRepository,
            IColetaInsumoRepository coletaInsumoRepository,
            IPMORepository pmoRepository,
            INotificacaoService notificacaoService,
            IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository,
            IColetaInsumoService coletaInsumoService,
            IArquivoRepository arquivoRepository,
            ISharePointService sharePointService,
            IHistoricoService historicoService,
            IArquivoSemanaOperativaRepository arquivoSemanaOperativaRepository)
        {
            this.semanaOperativaRepository = semanaOperativaRepository;
            this.parametroService = parametroService;
            this.situacaoSemanaOperativaRepository = situacaoSemanaOperativaRepository;
            this.situacaoColetaInsumoRepository = situacaoColetaInsumoRepository;
            this.gabaritoRepository = gabaritoRepository;
            this.coletaInsumoRepository = coletaInsumoRepository;
            this.pmoRepository = pmoRepository;
            this.notificacaoService = notificacaoService;
            this.dadoColetaNaoEstruturadoRepository = dadoColetaNaoEstruturadoRepository;
            this.coletaInsumoService = coletaInsumoService;
            this.arquivoRepository = arquivoRepository;
            this.sharePointService = sharePointService;
            this.historicoService = historicoService;
            this.arquivoSemanaOperativaRepository = arquivoSemanaOperativaRepository;
        }

        public SemanaOperativa ObterSemanaOperativaPorChaveParaInformarDados(int chave)
        {
            SemanaOperativa semanaOperativa = semanaOperativaRepository.FindByKey(chave);
            ValidarSituacaoInformarDados(semanaOperativa);
            return semanaOperativa;
        }

        public SemanaOperativa ObterSemanaOperativaPorChave(int chave)
        {
            return semanaOperativaRepository.FindByKey(chave);
        }

        public SemanaOperativa ObterSemanaOperativaPorChave(int chave, byte[] versao)
        {
            return semanaOperativaRepository.FindByKeyConcurrencyValidate(chave, versao);
        }

        public IList<SemanaOperativa> ConsultarSemanasOperativasComGabarito()
        {
            return semanaOperativaRepository.ConsultarSemanasOperativasComGabarito();
        }

        public IList<SemanaOperativa> ConsultarEstudoPorNome(string nomeEstudo)
        {
            int quantidadeMaxima = ONSConfigurationManager.GetSettings(
                ONSConfigurationManager.ConfigNameMaxResultsAutoComplete, 10);

            return semanaOperativaRepository.ConsultarEstudoPorNome(nomeEstudo, quantidadeMaxima);
        }

        /// <summary>
        /// Método utilizado para retornar os estudos passíveis de convergência de PLD, considerando o nome do estudo como critério de pesquisa.
        /// </summary>
        /// <param name="nomeEstudo"></param>
        /// <returns></returns>
        public IList<SemanaOperativa> ConsultarEstudoConvergenciaPldPorNome(string nomeEstudo)
        {
            int quantidadeMaxima = ONSConfigurationManager.GetSettings(
                ONSConfigurationManager.ConfigNameMaxResultsAutoComplete, 10);

            return semanaOperativaRepository.ConsultarEstudoPorNomeEStatus(nomeEstudo, (int)SituacaoSemanaOperativaEnum.ConvergenciaCCEE, quantidadeMaxima);
        }

        public void AtualizarSemanasOperativasInclusao(IEnumerable<SemanaOperativa> semanasOperativas,
            int ano, string nomeMes)
        {
            foreach (var semanaOperativa in semanasOperativas)
            {
                semanaOperativa.Revisao++;
                semanaOperativa.Nome = ObterNomeSemanaOperativa(ano, nomeMes, semanaOperativa.Revisao);
                semanaOperativa.DataHoraAtualizacao = DateTime.Now;
            }
        }

        public void ExcluirSemana(SemanaOperativa semanaOperativa)
        {
            historicoService.ExcluirHistoricoColetaInsumoViaSemanaOperativa(semanaOperativa.Id);
            historicoService.ExcluirHistoricoSemanaOperativa(semanaOperativa.Id);

            gabaritoRepository.Delete(semanaOperativa.Gabaritos);
            coletaInsumoRepository.Delete(semanaOperativa.ColetasInsumos);
            semanaOperativaRepository.Delete(semanaOperativa);
        }

        #region Abrir Estudo

        public SemanaOperativa ObterSemanaOperativaValidaParaAbrirEstudo(DadosSemanaOperativaDTO dto)
        {
            SemanaOperativa semana = ObterSemanaOperativaPorChave(dto.IdSemanaOperativa);
            if (semana != null)
            {
                // Significa que a chamada deste método tem a intenção de verificar a versão do PMO
                if (dto.VersaoPMO != null)
                {
                    var pmo = pmoRepository.FindByKeyConcurrencyValidate(semana.PMO.Id, dto.VersaoPMO);
                }

                if (semana.Situacao != null)
                {
                    throw new ONSBusinessException(SGIPMOMessages.MS008);
                }
            }
            return semana;
        }

        public void AbrirEstudo(AberturaEstudoDTO dto)
        {
            AssociarGabarito(dto.IdSemanaOperativa, dto.IdEstudo, dto.IsPadrao, dto.VersaoPMO, dto.VersaoSemanaOperativa);
        }

        private void AssociarGabarito(int idSemanaOperativa, int? idSemanaEstudoGabarito, bool isPadrao, byte[] versaoPMO, byte[] versaoSemanaOperativa)
        {
            SemanaOperativa semanaOperativa = versaoSemanaOperativa == null
                ? ObterSemanaOperativaPorChave(idSemanaOperativa)
                : ObterSemanaOperativaPorChave(idSemanaOperativa, versaoSemanaOperativa);

            if (semanaOperativa != null)
            {
                if (versaoPMO != null)
                {
                    ONS.WEBPMO.Domain.Entities.PMO.PMO pmo = pmoRepository.FindByKeyConcurrencyValidate(semanaOperativa.PMO.Id, versaoPMO);
                    if (pmo != null)
                    {
                        pmo.Versao = versaoPMO;
                    }
                }

                if (versaoSemanaOperativa != null) semanaOperativa.Versao = versaoSemanaOperativa;

                if (isPadrao)
                {
                    AssociarGabaritoPadrao(semanaOperativa);
                }
                else
                {
                    AssociarGabaritoEstudoAnterior(idSemanaEstudoGabarito, semanaOperativa);
                }

                //semanaOperativa.SituacaoId = (int)SituacaoSemanaOperativaEnum.Configuracao;
                semanaOperativa.Situacao = situacaoSemanaOperativaRepository.FindByKey((int)SituacaoSemanaOperativaEnum.Configuracao);
                semanaOperativa.DataHoraAtualizacao = DateTime.Now;

                historicoService.CriarSalvarHistoricoSemanaOperativa(semanaOperativa);
            }

        }

        private void AssociarGabaritoEstudoAnterior(int? idSemanaEstudoGabarito, SemanaOperativa semanaOperativa)
        {
            if (idSemanaEstudoGabarito.HasValue)
            {
                SemanaOperativa semanaEstudoGabarito = ObterSemanaOperativaPorChave(idSemanaEstudoGabarito.Value);
                IEnumerable<Gabarito> gabaritosCopiados = CopiarGabaritos(semanaEstudoGabarito.Gabaritos.ToList());
                SituacaoColetaInsumo situacao = situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.NaoIniciado);
                IEnumerable<ColetaInsumo> coletasCopiadas = semanaEstudoGabarito.ColetasInsumos.ToList().Where(ci => ci.Insumo.Ativo).Select(ci =>
                    CriarColetaInsumo(ci.Agente, ci.Insumo, ci.CodigoPerfilONS, semanaOperativa, situacao));

                AssociarGabaritosColetasASemana(semanaOperativa, gabaritosCopiados, coletasCopiadas);
            }
        }

        private void AssociarGabaritoPadrao(SemanaOperativa semanaOperativa)
        {
            var filtro = new GabaritoConfiguracaoFilter { IsPadrao = true };

            var gabaritosPadrao = gabaritoRepository.ConsultarPorGabaritoFilter(filtro);

            var gabaritosCopiados = CopiarGabaritos(gabaritosPadrao);

            var gabaritosPadraoAgrupados = gabaritosPadrao.GroupBy(g => new { g.Insumo, g.Agente, g.CodigoPerfilONS });

            var situacaoColetaInsumo = situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.NaoIniciado);

            var coletasCriadas = gabaritosPadraoAgrupados.Where(g => g.Key.Insumo.Ativo)
                .Select(g => CriarColetaInsumo(g.Key.Agente, g.Key.Insumo, g.Key.CodigoPerfilONS,
                    semanaOperativa, situacaoColetaInsumo));

            AssociarGabaritosColetasASemana(semanaOperativa, gabaritosCopiados, coletasCriadas);

        }

        private void AssociarGabaritosColetasASemana(SemanaOperativa semanaOperativa, IEnumerable<Gabarito> gabaritos,
            IEnumerable<ColetaInsumo> coletasInsumos)
        {
            // É preciso limpar a lista para que o EF limpe os gabaritos e coletas
            // pois esse fluxo é chamado também pelo ResetarGabarito.
            gabaritoRepository.DeletarPorIdSemanaOperativa(semanaOperativa.Id);
            foreach (var gabarito in gabaritos)
            {
                semanaOperativa.Gabaritos.Add(gabarito);
            }

            historicoService.ExcluirHistoricoColetaInsumoViaSemanaOperativa(semanaOperativa.Id);
            coletaInsumoRepository.DeletarPorIdSemanaOperativa(semanaOperativa.Id);
            foreach (var coletaInsumo in coletasInsumos)
            {
                semanaOperativa.ColetasInsumos.Add(coletaInsumo);
                historicoService.CriarSalvarHistoricoColetaInsumo(coletaInsumo);
            }
        }

        private IEnumerable<Gabarito> CopiarGabaritos(IEnumerable<Gabarito> gabaritos)
        {
            foreach (var gabarito in gabaritos)
            {
                yield return new Gabarito
                {
                    Agente = gabarito.Agente,
                    Insumo = gabarito.Insumo,
                    OrigemColeta = gabarito.OrigemColeta,
                    IsPadrao = false,
                    CodigoPerfilONS = gabarito.CodigoPerfilONS
                };
            }
        }

        private ColetaInsumo CriarColetaInsumo(Agente agente, Insumo insumo, string codigoPerfilONS,
            SemanaOperativa semanaOperativa, SituacaoColetaInsumo situacao)
        {
            return new ColetaInsumo
            {
                Insumo = insumo,
                Agente = agente,
                CodigoPerfilONS = codigoPerfilONS,
                Situacao = situacao,
                SemanaOperativa = semanaOperativa,
                LoginAgenteAlteracao = UserInfo.UserName,
                DataHoraAtualizacao = DateTime.Now
            };
        }

        #endregion

        #region Gerar Semana Operativa

        public ISet<SemanaOperativa> GerarSugestaoSemanasOperativas(int ano, int mes)
        {
            ISet<SemanaOperativa> semanasOperativas = new SortedSet<SemanaOperativa>();

            var cultura = CultureInfo.CurrentCulture;
            string nomeMes = cultura.TextInfo.ToTitleCase(cultura.DateTimeFormat.GetMonthName(mes));

            DateTime dataInicioSemana = new DateTime(ano, mes, 1);
            DateTime ultimoDiaMes = dataInicioSemana.AddMonths(1).AddDays(-1);
            DateTime dataFimPMO = ultimoDiaMes;

            if (dataInicioSemana.DayOfWeek != DayOfWeek.Saturday)
            {
                // A semana operativa desve ser sempre de Sábado a Sexta
                // Se o primeiro dia do mês não for um Sábado é preciso obter a quantidade de dias 
                // que se deve retroceder para chegar ao Sábado
                int qtdDiasParaSabado = -(int)dataInicioSemana.DayOfWeek - 1;
                dataInicioSemana = dataInicioSemana.AddDays(qtdDiasParaSabado);
            }

            if (ultimoDiaMes.DayOfWeek != DayOfWeek.Friday)
            {
                // A semana operativa desve ser sempre de Sábado a Sexta
                // Se último dia do mês não for uma Sexta é preciso obter a quantidade de dias 
                // para chegar à Sexta
                int qtdDiasParaSexta = ultimoDiaMes.DayOfWeek == DayOfWeek.Saturday ?
                    6 : (int)DayOfWeek.Friday - (int)ultimoDiaMes.DayOfWeek;
                dataFimPMO = ultimoDiaMes.AddDays(qtdDiasParaSexta);
            }

            int revisao = 0;
            while (dataInicioSemana <= ultimoDiaMes)
            {
                SemanaOperativa semanaOperativa = GerarSemanaOperativa(ano, nomeMes, dataInicioSemana, dataFimPMO, revisao);
                if (semanaOperativa != null)
                {
                    semanasOperativas.Add(semanaOperativa);
                    revisao++;
                }
                dataInicioSemana = dataInicioSemana.AddDays(7);
            }
            return semanasOperativas;
        }

        public SemanaOperativa GerarSemanaOperativa(int ano, string nomeMes, DateTime dataInicioSemana,
            DateTime dataFimPMO, int revisao)
        {
            SemanaOperativa semanaOperativa = new SemanaOperativa
            {
                DataInicioSemana = dataInicioSemana,
                DataFimSemana = dataInicioSemana.AddDays(6),
                DataReuniao = ObterDataReuniao(dataInicioSemana, revisao),
                Revisao = revisao,
                DataInicioManutencao = dataInicioSemana,
                DataFimManutencao = dataFimPMO,
                Nome = ObterNomeSemanaOperativa(ano, nomeMes, revisao)
            };

            return semanaOperativa;
        }

        private string ObterNomeSemanaOperativa(int ano, string nomeMes, int revisao)
        {
            string nomeSemanaOperativa = revisao == 0 ?
                string.Format("PMO {0} {1}", nomeMes, ano) :
                string.Format("PMO {0} {1} - Revisão {2}", nomeMes, ano, revisao);
            return nomeSemanaOperativa;
        }

        private DateTime ObterDataReuniao(DateTime dataInicioSemana, int revisao)
        {
            Parametro parametro;
            int valorDiaReuniao = 0;
            if (revisao == 0)
            {
                parametro = parametroService.ObterParametro(ParametroEnum.DiaReuniaoPMO);
                if (parametro != null)
                {
                    valorDiaReuniao = int.Parse(parametro.Valor);
                }
            }
            else
            {
                parametro = parametroService.ObterParametro(ParametroEnum.DiaReuniaoRevisao);
                if (parametro != null)
                {
                    valorDiaReuniao = int.Parse(parametro.Valor);
                }
            }
            int valorDeDiasParaRetroceder = valorDiaReuniao == (int)DayOfWeek.Saturday
                ? -7
                : valorDiaReuniao - (int)DayOfWeek.Saturday;
            return dataInicioSemana.AddDays(valorDeDiasParaRetroceder);
        }

        #endregion

        #region Alterar Semana Operativa

        public void AlterarSemanaOperativa(DadosAlteracaoSemanaOperativaDTO dadosAlteracao)
        {
            ValidarDataAlteracao(dadosAlteracao);
            SemanaOperativa semanaOperativa = semanaOperativaRepository.FindByKey(dadosAlteracao.Id);
            if (semanaOperativa != null)
            {
                semanaOperativa.DataReuniao = dadosAlteracao.DataReuniao;
                semanaOperativa.DataInicioManutencao = dadosAlteracao.DataInicioManutencao;
                semanaOperativa.DataFimManutencao = dadosAlteracao.DataFimManutencao;
                semanaOperativa.DataHoraAtualizacao = DateTime.Now;
            }
        }

        #endregion

        #region Resetar Gabarito

        public void ResetarGabarito(ResetGabaritoDTO dto)
        {
            AssociarGabarito(dto.IdSemanaOperativa, dto.IdEstudo, dto.IsPadrao, dto.VersaoPMO, dto.VersaoSemanaOperativa);
        }

        #endregion

        #region Validações

        private void ValidarDataAlteracao(DadosAlteracaoSemanaOperativaDTO dadosAlteracao)
        {
            const string dataReuniao = "Data da Reunião";
            const string dataInicioManutencao = "Data Início da Manutenção";
            const string dataFimManutencao = "Data Termino da Manutenção";

            bool isDataInvalida = false;
            DateTime dataAtual = DateTime.Now.Date;

            IList<string> mensagens = new List<string>();

            if (dadosAlteracao.DataReuniao.CompareTo(dataAtual) < 0)
            {
                mensagens.Add(string.Format(SGIPMOMessages.MS006, dataReuniao));
            }

            if (dadosAlteracao.DataInicioManutencao.CompareTo(dataAtual) < 0)
            {
                mensagens.Add(string.Format(SGIPMOMessages.MS006, dataInicioManutencao));
            }

            if (dadosAlteracao.DataFimManutencao.CompareTo(dataAtual) < 0)
            {
                mensagens.Add(string.Format(SGIPMOMessages.MS006, dataFimManutencao));
            }

            if (mensagens.Any())
            {
                throw new ONSBusinessException(mensagens);
            }
        }

        public SemanaOperativa ObterSemanaOperativaValidaParaResetarGabarito(int idSemanaOperativa)
        {
            SemanaOperativa semana = ObterSemanaOperativaPorChave(idSemanaOperativa);
            if (semana != null)
            {
                if (semana.Situacao == null
                    || semana.Situacao.IdTpsituacaosemanaoper == (int)SituacaoSemanaOperativaEnum.GeracaoBlocos
                    || semana.Situacao.IdTpsituacaosemanaoper == (int)SituacaoSemanaOperativaEnum.ConvergenciaCCEE
                    || semana.Situacao.IdTpsituacaosemanaoper == (int)SituacaoSemanaOperativaEnum.Publicado
                    || semana.Situacao.IdTpsituacaosemanaoper == (int)SituacaoSemanaOperativaEnum.ColetaDados)
                {
                    throw new ONSBusinessException(SGIPMOMessages.MS052);
                }
            }
            return semana;
        }

        private void ValidarSituacaoInformarDados(SemanaOperativa semanaOperativa)
        {
            if (semanaOperativa.Situacao.IdTpsituacaosemanaoper != (int)SituacaoSemanaOperativaEnum.ColetaDados)
            {
                throw new ONSBusinessException(SGIPMOMessages.MS031);
            }
        }

        /// <summary>
        /// [UC1004][RN003] Caso o estudo selecionado não esteja no estado/processo "Geração de Blocos" o sistema não 
        /// deverá permitir anexar ou excluir arquivo, nem iniciar processo de convergência com CCEE. [RS_MENS_046]
        /// </summary>
        /// <param name="semanaOperativa"></param>
        /// <param name="mensagens"></param>
        private void ValidarEstudoSituacaoParaInicializacaoConvergenciaCCEE(SemanaOperativa semanaOperativa, IList<string> mensagens)
        {
            if (semanaOperativa.Situacao.IdTpsituacaosemanaoper != (int)SituacaoSemanaOperativaEnum.GeracaoBlocos)
            {
                mensagens.Add(SGIPMOMessages.MS046);
            }
        }

        /// <summary>
        /// [UC1030][RN006] Validando se o estudo está no estado "Convergência CCEE"
        /// </summary>
        /// <param name="semanaOperativa"></param>
        /// <param name="mensagens"></param>
        private void ValidarEstudoSituacaoParaConvergirPLD(SemanaOperativa semanaOperativa, IList<string> mensagens, bool levantarException = false)
        {
            if (semanaOperativa.Situacao.IdTpsituacaosemanaoper != (int)SituacaoSemanaOperativaEnum.ConvergenciaCCEE)
            {
                mensagens.Add(SGIPMOMessages.MS051);
                if (levantarException) VerificarONSBusinessException(mensagens);
            }
        }

        /// <summary>
        /// [UC1030][RN007] Validando a obrigatoriedade do campo Observação quando "Não Convergir PLD"
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="mensagens"></param>
        private void ValidarEstudoNaoConvergirPLDSemObservacao(ConvergirPLDDTO dto, IList<string> mensagens)
        {
            if (!dto.Convergir && string.IsNullOrEmpty(dto.ObservacoesConvergenciaPld))
            {
                string mensagem = string.Format(SGIPMOMessages.MS001, "Observação");
                mensagens.Add(mensagem);
            }
        }

        /// <summary>
        /// [UC1004][RN003] Para concluir a ação será necessário anexar ao menos um arquivo
        /// </summary>
        /// <param name="arquivos"></param>
        /// <param name="mensagens"></param>
        private void ValidarArquivosInicializacaoConvergenciaCCEE(ISet<ArquivoDadoNaoEstruturadoDTO> arquivos, IList<string> mensagens)
        {
            if (arquivos == null || !arquivos.Any())
            {
                mensagens.Add(SGIPMOMessages.MS050);
            }
        }

        /// <summary>
        /// [UC1005][RN003] Só deverá permitir Publicação de Resultados para estudo que esteja nos estado/processo "PLD Convergido" ou "Publicado". [RS_MENS_049]
        /// </summary>
        /// <param name="semanaOperativa"></param>
        /// <param name="mensagens"></param>
        private void ValidarEstudoSituacaoParaPublicacaoResultados(SemanaOperativa semanaOperativa, IList<string> mensagens)
        {
            if (semanaOperativa.Situacao.IdTpsituacaosemanaoper != (int)SituacaoSemanaOperativaEnum.PLDConvergido &&
                semanaOperativa.Situacao.IdTpsituacaosemanaoper != (int)SituacaoSemanaOperativaEnum.ConvergenciaCCEE &&
                semanaOperativa.Situacao.IdTpsituacaosemanaoper != (int)SituacaoSemanaOperativaEnum.Publicado)
            {
                mensagens.Add(SGIPMOMessages.MS049);
            }
        }

        /// <summary>
        /// [UC1005][RN007] Só deverá permitir Reprocessar PMO para estudo que esteja nos estado/processo "PLD Convergido" ou "Publicado". [RS_MENS_049]
        /// </summary>
        /// <param name="semanaOperativa"></param>
        /// <param name="mensagens"></param>
        private void ValidarEstudoSituacaoParaReprocessarPMO(SemanaOperativa semanaOperativa, IList<string> mensagens)
        {
            if (semanaOperativa.Situacao.IdTpsituacaosemanaoper != (int)SituacaoSemanaOperativaEnum.PLDConvergido &&
                semanaOperativa.Situacao.IdTpsituacaosemanaoper != (int)SituacaoSemanaOperativaEnum.ConvergenciaCCEE &&
                semanaOperativa.Situacao.IdTpsituacaosemanaoper != (int)SituacaoSemanaOperativaEnum.Publicado)
            {
                mensagens.Add(SGIPMOMessages.MS049);
            }
        }

        /// <summary>
        /// [UC1005][RN003] Para concluir a ação será necessário anexar ao menos um arquivo
        /// </summary>
        /// <param name="arquivos"></param>
        /// <param name="mensagens"></param>
        private void ValidarArquivosPublicacaoResultados(ISet<ArquivoDadoNaoEstruturadoDTO> arquivos, IList<string> mensagens)
        {
            List<ArquivoDadoNaoEstruturadoDTO> arquivosNovos = arquivos.Where(elemento => elemento.Id == Guid.Empty).ToList();
            if (!arquivosNovos.Any())
            {
                mensagens.Add(SGIPMOMessages.MS050);
            }
        }

        private void VerificarONSBusinessException(IList<string> mensagens)
        {
            if (mensagens.Any())
            {
                throw new ONSBusinessException(mensagens);
            }
        }

        #endregion

        #region Arquivos

        /// <summary>
        /// Este método tem o objetivo de retornar um objeto DTO com os principais dados de um arquivo, desconsiderando o conteúdo "byte[]".
        /// </summary>
        /// <param name="arquivo"></param>
        /// <returns></returns>
        private ArquivoDadoNaoEstruturadoDTO CastArquivoParaDTO(Arquivo arquivo, string descricaoAdicional = null)
        {
            return new ArquivoDadoNaoEstruturadoDTO()
            {
                Id = arquivo.Id,
                Nome = arquivo.Nome + (string.IsNullOrEmpty(descricaoAdicional) ? "" : descricaoAdicional),
                Tamanho = arquivo.Tamanho
            };
        }

        #endregion

        #region Convergência CCEE

        /// <summary>
        /// Este método tem o objetivo de consultar arquivos de uma determinada semana operativa que serão visualizados no momento de efetuar a Convergência CCEE.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ArquivosSemanaOperativaDTO ConsultarArquivosSemanaOperativaConvergenciaCcee(ArquivosSemanaOperativaFilter filtro)
        {
            ArquivosSemanaOperativaDTO retorno = new ArquivosSemanaOperativaDTO();

            // Consultando os arquivos associados aos insumos de Processamento
            IList<DadoColetaNaoEstruturado> dadosNaoEstruturados = dadoColetaNaoEstruturadoRepository.ObterDadosColetaNaoEstruturado(filtro);
            foreach (var dadoColetaNaoEstruturado in dadosNaoEstruturados)
            {
                foreach (var arquivo in dadoColetaNaoEstruturado.Arquivos)
                {
                    retorno.ArquivosInsumos.Add(new ArquivoDadoNaoEstruturadoConsultaInsumoDTO()
                    {
                        Arquivo = CastArquivoParaDTO(arquivo),
                        DescricaoInsumo = dadoColetaNaoEstruturado.ColetaInsumo.Insumo.Nome
                    });
                }
            }

            // Consultando os arquivos que foram enviados durante o estado de Convergência com CCEE
            SemanaOperativa semanaOperativa = semanaOperativaRepository.FindByKey(filtro.IdSemanaOperativa);
            foreach (var arquivoSemanaOperativa in semanaOperativa.Arquivos)
            {
                if (arquivoSemanaOperativa.Arquivo != null && arquivoSemanaOperativa.Situacao.IdTpsituacaosemanaoper == (int)SituacaoSemanaOperativaEnum.ConvergenciaCCEE)
                {
                    retorno.ArquivosEnviados.Add(CastArquivoParaDTO(arquivoSemanaOperativa.Arquivo));
                }
            }

            retorno.SituacaoSemanaOperativa = semanaOperativa.Situacao;

            return retorno;
        }

        /// <summary>
        /// Método responsável por efetuar o início de Convergência com CCEE.
        /// </summary>
        /// <param name="dto"></param>
        public void IniciarConvergenciaCCEE(InicializacaoConvergenciaCceeDTO dto)
        {
            SemanaOperativa semanaOperativa = semanaOperativaRepository.FindByKeyConcurrencyValidate(dto.IdSemanaOperativa, dto.VersaoSemanaOperativa);
            if (semanaOperativa != null)
            {
                IList<string> mensagens = new List<string>();
                ValidarEstudoSituacaoParaInicializacaoConvergenciaCCEE(semanaOperativa, mensagens);
                ValidarArquivosInicializacaoConvergenciaCCEE(dto.Arquivos, mensagens);
                VerificarONSBusinessException(mensagens);

                // Mudando a situação da semana operativa
                SituacaoSemanaOperativa situacaoSemanaOperativaConvergenciaCcee = situacaoSemanaOperativaRepository.FindByKey((int)SituacaoSemanaOperativaEnum.ConvergenciaCCEE);
                semanaOperativa.Situacao = situacaoSemanaOperativaConvergenciaCcee;
                semanaOperativa.DataHoraAtualizacao = DateTime.Now;

                historicoService.CriarSalvarHistoricoSemanaOperativa(semanaOperativa);

                // Obtendo os arquivos que foram enviados para a pasta temporária
                ISet<Arquivo> arquivosUploadAindaNaoGravados = coletaInsumoService.ObterArquivosUpload(dto.Arquivos, true);
                foreach (var arquivoUploadAindaNaoGravado in arquivosUploadAindaNaoGravados)
                {
                    // Gravando o arquivo no banco de dados e obtendo a instância dele a fim de evitar sobrecarga no SaveChanges.
                    Arquivo arquivoRecemGravadoBancoDados =
                        arquivoRepository.SalvarArquivoContentFile(arquivoUploadAindaNaoGravado);

                    // Fazendo a associação entre o novo arquivo e a semana operativa.
                    ArquivoSemanaOperativa novoArquivoSemanaOperativa = new ArquivoSemanaOperativa();
                    novoArquivoSemanaOperativa.Arquivo = arquivoRecemGravadoBancoDados;
                    novoArquivoSemanaOperativa.IsPublicado = false;
                    novoArquivoSemanaOperativa.Situacao = situacaoSemanaOperativaConvergenciaCcee;
                    semanaOperativa.Arquivos.Add(novoArquivoSemanaOperativa);
                }

                // Enviando notificação para Representantes CCEE
                Parametro parametroMensagemNotificacaoCcee = parametroService.ObterParametro(ParametroEnum.MensagemNotificacaoConvergenciaCcee);

                var cultura = CultureInfo.CurrentCulture;
                string nomeMes = cultura.TextInfo.ToTitleCase(
                        cultura.DateTimeFormat.GetMonthName(semanaOperativa.PMO.MesReferencia));

                string tituloNotificacao =
                    string.Format("[ONS-WEBPMO] Início da convergência do PLD do PMO {0} de {1} – Revisão {2}",
                        nomeMes, semanaOperativa.PMO.AnoReferencia, semanaOperativa.Revisao);

                notificacaoService.NotificarUsuariosPorPerfil(RolePermissoesPopEnum.RepresentanteCCEE, tituloNotificacao, parametroMensagemNotificacaoCcee.Valor);
            }
        }

        #endregion

        #region Convergir PLD

        /// <summary>
        /// Serviço que consulta os arquivos de insumos coletados durante o processo de coleta e também os arquivos que foram enviados ao iniciar Convergência com CCEE.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ArquivosSemanaOperativaConvergirPldDTO ConsultarArquivosSemanaOperativaConvergenciaPLD(ArquivosSemanaOperativaFilter filtro)
        {
            ArquivosSemanaOperativaConvergirPldDTO retorno = new ArquivosSemanaOperativaConvergirPldDTO();
            retorno.SemanaOperativa = semanaOperativaRepository.FindByKey(filtro.IdSemanaOperativa);

            // Consultando os arquivos associados aos insumos de Processamento
            IList<DadoColetaNaoEstruturado> dadosNaoEstruturados =
                dadoColetaNaoEstruturadoRepository.ObterDadosColetaNaoEstruturado(filtro);

            foreach (var dadoColetaNaoEstruturado in dadosNaoEstruturados)
            {
                if (dadoColetaNaoEstruturado.Arquivos.Count > 0)
                {
                    foreach (var arquivo in dadoColetaNaoEstruturado.Arquivos)
                    {
                        if (arquivo != null)
                        {
                            retorno.Arquivos.Add(
                                CastArquivoParaDTO(arquivo, " - Insumo " + dadoColetaNaoEstruturado.ColetaInsumo.Insumo.Nome));
                        }
                    }
                }
            }

            // Consultando os arquivos que foram enviados durante o estado de Convergência com CCEE
            IList<ArquivoSemanaOperativa> arquivosSemanaOperativa =
                retorno.SemanaOperativa.Arquivos.Where(
                    a => a.Situacao.IdTpsituacaosemanaoper == (int)SituacaoSemanaOperativaEnum.ConvergenciaCCEE).ToList();

            foreach (var arquivoSemanaOperativa in arquivosSemanaOperativa)
            {
                if (arquivoSemanaOperativa.Arquivo != null)
                {
                    retorno.Arquivos.Add(CastArquivoParaDTO(arquivoSemanaOperativa.Arquivo));
                }
            }

            return retorno;
        }

        /// <summary>
        /// Método utilizado para realizar a convergência com PLD ("Convergir PLD" ou "Não Convergir PLD")
        /// </summary>
        /// <param name="dto"></param>
        public void ConvergirPLD(ConvergirPLDDTO dto)
        {
            SemanaOperativa semanaOperativa = semanaOperativaRepository.FindByKeyConcurrencyValidate(dto.IdSemanaOperativa, dto.VersaoSemanaOperativa);
            if (semanaOperativa != null)
            {
                IList<string> mensagens = new List<string>();
                ValidarEstudoSituacaoParaConvergirPLD(semanaOperativa, mensagens, true);
                ValidarEstudoNaoConvergirPLDSemObservacao(dto, mensagens);
                VerificarONSBusinessException(mensagens);

                if (semanaOperativa.DadoConvergencia == null)
                {
                    semanaOperativa.DadoConvergencia = new DadoConvergencia();
                }

                semanaOperativa.DadoConvergencia.LoginConvergenciaRepresentanteCCEE = Context.GetUserName();
                semanaOperativa.DadoConvergencia.ObservacaoConvergenciaCCEE = dto.ObservacoesConvergenciaPld;
                semanaOperativa.DataHoraAtualizacao = DateTime.Now;

                semanaOperativa.Situacao = situacaoSemanaOperativaRepository.FindByKey(dto.Convergir
                    ? (int)SituacaoSemanaOperativaEnum.PLDConvergido
                    : (int)SituacaoSemanaOperativaEnum.GeracaoBlocos);

                historicoService.CriarSalvarHistoricoSemanaOperativa(semanaOperativa);
            }
        }

        #endregion

        #region Publicação de Resultados

        /// <summary>
        /// Método utilizado para consultar os arquivos a serem considerados na etapa de publicação de resultados.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ArquivosSemanaOperativaDTO ConsultarArquivosSemanaOperativaPublicacaoResultados(ArquivosSemanaOperativaFilter filtro)
        {
            ArquivosSemanaOperativaDTO retorno = new ArquivosSemanaOperativaDTO();

            // Consultando os arquivos que foram enviados durante o estado de Publicação de Resultados
            SemanaOperativa semanaOperativa = semanaOperativaRepository.FindByKey(filtro.IdSemanaOperativa);
            foreach (var arquivoSemanaOperativa in semanaOperativa.Arquivos)
            {
                if (arquivoSemanaOperativa.Arquivo != null && arquivoSemanaOperativa.IsPublicado)
                {
                    ArquivoDadoNaoEstruturadoDTO arquivoEnviado = CastArquivoParaDTO(arquivoSemanaOperativa.Arquivo);
                    arquivoEnviado.IsPublicado = arquivoSemanaOperativa.IsPublicado;
                    retorno.ArquivosEnviados.Add(arquivoEnviado);
                }
            }

            // Consultando os arquivos associados aos insumos q serem exibidos na estapa de Publicação
            bool IsSemanaOperativaPublicada = semanaOperativa.Situacao.IdTpsituacaosemanaoper == (int)SituacaoSemanaOperativaEnum.Publicado;
            IList<DadoColetaNaoEstruturado> dadosNaoEstruturados = dadoColetaNaoEstruturadoRepository.ObterDadosColetaNaoEstruturado(filtro);
            foreach (var dadoColetaNaoEstruturado in dadosNaoEstruturados)
            {
                foreach (var arquivo in dadoColetaNaoEstruturado.Arquivos)
                {
                    ArquivoDadoNaoEstruturadoDTO arquivoDTO = CastArquivoParaDTO(arquivo);
                    arquivoDTO.IsPublicado = IsSemanaOperativaPublicada;
                    retorno.ArquivosInsumos.Add(new ArquivoDadoNaoEstruturadoConsultaInsumoDTO()
                    {
                        Arquivo = arquivoDTO,
                        DescricaoInsumo = dadoColetaNaoEstruturado.ColetaInsumo.Insumo.Nome
                    });
                }
            }

            retorno.SituacaoSemanaOperativa = semanaOperativa.Situacao;

            return retorno;
        }

        /// <summary>
        /// Método utilizado para efetuar a publicação de resultados de uma semana operativa.
        /// </summary>
        /// <param name="dto"></param>
        public void PublicarResultados(PublicacaoResultadosDTO dto)
        {
            SemanaOperativa semanaOperativa = semanaOperativaRepository.FindByKeyConcurrencyValidate(dto.IdSemanaOperativa, dto.VersaoSemanaOperativa);
            if (semanaOperativa != null)
            {
                if (!dto.IsEncerradoDiretamente)
                {
                    IList<string> mensagens = new List<string>();
                    ValidarEstudoSituacaoParaPublicacaoResultados(semanaOperativa, mensagens);
                    ValidarArquivosPublicacaoResultados(dto.Arquivos, mensagens);
                    VerificarONSBusinessException(mensagens);
                }

                // Situação "Publicado" da semana operativa que será usada para atualizar a própria semana operativa.
                SituacaoSemanaOperativa situacaoSemanaOperativaPublicado =
                    situacaoSemanaOperativaRepository.FindByKey((int)SituacaoSemanaOperativaEnum.Publicado);

                bool hasNovosArquivosAdicionados = false;

                if (!dto.IsEncerradoDiretamente)
                {
                    // Obtendo os arquivos que foram enviados para a pasta temporária
                    ISet<Arquivo> novosArquivosUploadAindaNaoSalvos = coletaInsumoService.ObterArquivosUpload(dto.Arquivos, true);
                    foreach (var arquivoUploadAindaNaoSalvo in novosArquivosUploadAindaNaoSalvos)
                    {
                        // Gravando o arquivo no banco de dados a fim de evitar sobrecarrgar o SaveChanges().
                        Arquivo arquivoRecemGravadoBanco = arquivoRepository.SalvarArquivoContentFile(arquivoUploadAindaNaoSalvo);

                        // Instanciando associação entre SemanaOperativa e arquivo recém gravado.
                        ArquivoSemanaOperativa novoArquivoSemanaOperativa = new ArquivoSemanaOperativa();
                        novoArquivoSemanaOperativa.Arquivo = arquivoRecemGravadoBanco;
                        novoArquivoSemanaOperativa.IsPublicado = true;
                        novoArquivoSemanaOperativa.Situacao = situacaoSemanaOperativaPublicado;

                        semanaOperativa.Arquivos.Add(novoArquivoSemanaOperativa);
                        hasNovosArquivosAdicionados = true;
                    }

                    // Enviando arquivos para o SharePoint
                    if (hasNovosArquivosAdicionados) sharePointService.EnviarArquivosSharePoint(dto.Arquivos, semanaOperativa.Nome);

                }

                // Tratando o status da semana operativa
                if (semanaOperativa.Situacao.IdTpsituacaosemanaoper != (int)SituacaoSemanaOperativaEnum.Publicado)
                {
                    semanaOperativa.Situacao = situacaoSemanaOperativaPublicado; // Modificando o status da semana operativa

                    historicoService.CriarSalvarHistoricoSemanaOperativa(semanaOperativa);
                }
                else
                {
                    // Forçando a atualização da versão para evitar concorrência de dados
                    if (hasNovosArquivosAdicionados) semanaOperativa.Versao = dto.VersaoSemanaOperativa;
                }

                semanaOperativa.DataHoraAtualizacao = DateTime.Now;
            }
        }

        /// <summary>
        /// Método utilizado para efetuar o reprocessamento do PMO, que é acionado na mesma tela em que os resultados são publicados.
        /// </summary>
        /// <param name="dto"></param>
        public void ReprocessarPMO(ReprocessamentoPMODTO dto)
        {
            SemanaOperativa semanaOperativa = semanaOperativaRepository.FindByKeyConcurrencyValidate(dto.IdSemanaOperativa, dto.VersaoSemanaOperativa);
            if (semanaOperativa != null)
            {
                IList<string> mensagens = new List<string>();
                ValidarEstudoSituacaoParaReprocessarPMO(semanaOperativa, mensagens);
                VerificarONSBusinessException(mensagens);

                // Alterando o estado/processo do estudo para “Geração de Blocos”, 
                semanaOperativa.Situacao = situacaoSemanaOperativaRepository.FindByKey((int)SituacaoSemanaOperativaEnum.GeracaoBlocos);
                semanaOperativa.DataHoraAtualizacao = DateTime.Now;

                historicoService.CriarSalvarHistoricoSemanaOperativa(semanaOperativa);

                // Excluindo os arquivos desta semana operativa que foram enviados nos processos de Convergência CCEE e Publicação Resultados;
                if (semanaOperativa.Arquivos.Any())
                {
                    var arquivosSemanaExcluiveis =
                        semanaOperativa.Arquivos.Where(
                            a => a.SituacaoId == (int)SituacaoSemanaOperativaEnum.ConvergenciaCCEE
                                 || a.SituacaoId == (int)SituacaoSemanaOperativaEnum.Publicado)
                                 .Select(a => a).ToList();

                    foreach (var arquivoSemana in arquivosSemanaExcluiveis)
                    {
                        arquivoRepository.Delete(arquivoSemana.Arquivo);
                    }
                    arquivoSemanaOperativaRepository.Delete(arquivosSemanaExcluiveis);
                }
            }
        }

        #endregion

    }
}
