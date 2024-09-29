using ONS.Common.Exceptions;
using ONS.Common.Seguranca;
using ONS.Infra.Core.Files;
using ONS.Infra.Core.Pagination;
using ONS.Infra.Temp;
using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;
using ONS.WEBPMO.Domain.Entities.Resources;
using ONS.WEBPMO.Domain.Enumerations;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Domain.Repository.BDT;
using System.Configuration;
using System.Globalization;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class ColetaInsumoService : IColetaInsumoService
    {
        private readonly IColetaInsumoRepository coletaInsumoRepository;
        private readonly ISemanaOperativaRepository semanaOperativaRepository;
        private readonly IParametroService parametroService;
        private readonly ISituacaoColetaInsumoRepository situacaoColetaInsumoRepository;
        private readonly IDadoColetaEstruturadoRepository dadoColetaEstruturadoRepository;
        private readonly IAgenteService agenteService;
        private readonly IDadoColetaManutencaoService dadoColetaManutencaoService;
        private readonly IGabaritoRepository gabaritoRepository;
        private readonly ITipoLimiteRepository tipoLimiteRepository;
        private readonly ITipoPatamarRepository tipoPatamarRepository;
        private readonly IGrandezaRepository grandezaRepository;
        private readonly INotificacaoService notificacaoService;
        private readonly ISituacaoSemanaOperativaRepository situacaoSemanaOperativaRepository;
        private readonly IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository;
        private readonly IArquivoRepository arquivoRepository;
        private readonly IHistoricoService historicoService;
        private readonly IArquivoSemanaOperativaRepository arquivoSemanaOperativaRepository;
        private readonly ILogNotificacaoService logNotificacaoService;
        private readonly IInstanteVolumeReservatorioRepository instanteVolumeReservatorioRepository;
        private readonly IDadoColetaManutencaoRepository dadoColetaManutencaoRepository;

        public ColetaInsumoService(
            IColetaInsumoRepository coletaInsumoRepository,
            ISemanaOperativaRepository semanaOperativaRepository,
            IParametroService parametroService,
            ISituacaoColetaInsumoRepository situacaoColetaInsumoRepository,
            IAgenteService agenteService,
            IGabaritoRepository gabaritoRepository,
            ITipoLimiteRepository tipoLimiteRepository,
            ITipoPatamarRepository tipoPatamarRepository,
            IGrandezaRepository grandezaRepository,
            IDadoColetaManutencaoService dadoColetaManutencaoService,
            IDadoColetaEstruturadoRepository dadoColetaEstruturadoRepository,
            INotificacaoService notificacaoService,
            ISituacaoSemanaOperativaRepository situacaoSemanaOperativaRepository,
            IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository,
            IArquivoRepository arquivoRepository,
            IHistoricoService historicoService,
            IArquivoSemanaOperativaRepository arquivoSemanaOperativaRepository,
            ILogNotificacaoService logNotificacaoService,
            IInstanteVolumeReservatorioRepository instanteVolumeReservatorioRepository,
            IDadoColetaManutencaoRepository _dadoColetaManutencaoRepository)
        {
            this.coletaInsumoRepository = coletaInsumoRepository;
            this.semanaOperativaRepository = semanaOperativaRepository;
            this.parametroService = parametroService;
            this.situacaoColetaInsumoRepository = situacaoColetaInsumoRepository;
            this.agenteService = agenteService;
            this.gabaritoRepository = gabaritoRepository;
            this.tipoLimiteRepository = tipoLimiteRepository;
            this.tipoPatamarRepository = tipoPatamarRepository;
            this.grandezaRepository = grandezaRepository;
            this.dadoColetaManutencaoService = dadoColetaManutencaoService;
            this.dadoColetaEstruturadoRepository = dadoColetaEstruturadoRepository;
            this.notificacaoService = notificacaoService;
            this.situacaoSemanaOperativaRepository = situacaoSemanaOperativaRepository;
            this.dadoColetaNaoEstruturadoRepository = dadoColetaNaoEstruturadoRepository;
            this.arquivoRepository = arquivoRepository;
            this.historicoService = historicoService;
            this.arquivoSemanaOperativaRepository = arquivoSemanaOperativaRepository;
            this.logNotificacaoService = logNotificacaoService;
            this.instanteVolumeReservatorioRepository = instanteVolumeReservatorioRepository;
            dadoColetaManutencaoRepository = _dadoColetaManutencaoRepository;
        }

        public ColetaInsumo ObterPorChave(int chave)
        {
            return coletaInsumoRepository.FindByKey(chave);
        }

        public ColetaInsumo ObterColetaInsumoInformarDadosPorChave(int idColetaInsumo)
        {
            return coletaInsumoRepository.FindByKey(idColetaInsumo);
        }

        public ColetaInsumo ObterValidarColetaInsumoInformarDadosPorChave(int idColetaInsumo, byte[] versaoColetaInsumo = null, bool atualizaParaAndamento = false)
        {

            // recupera dados da coleta
            ColetaInsumo coletaInsumo = versaoColetaInsumo == null
                ? coletaInsumoRepository.FindByKey(idColetaInsumo)
                : coletaInsumoRepository.FindByKeyConcurrencyValidate(idColetaInsumo, versaoColetaInsumo);

            // valida informacoes da coleta
            IList<string> mensagens = new List<string>();
            ValidarEstudoSituacaoColetaDados(coletaInsumo.SemanaOperativa, mensagens);
            ValidarSituacaoColetaAoInformarDados(coletaInsumo, mensagens);
            VerificarONSBusinessException(mensagens);

            // atualiza status da coleta para em andamento
            if (atualizaParaAndamento && coletaInsumo.Situacao.Id == (int)SituacaoColetaInsumoEnum.Informado)
            {
                var situacaoEmAndamento = situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.EmAndamento);
                coletaInsumo.Situacao = situacaoEmAndamento;
                coletaInsumo.LoginAgenteAlteracao = UserInfo.UserName;
                coletaInsumo.DataHoraAtualizacao = DateTime.Now;
                historicoService.CriarSalvarHistoricoColetaInsumo(coletaInsumo);
            }

            return coletaInsumo;
        }

        public ColetaInsumo ObterValidarColetaInsumoMonitorarDadosPorChave(int idColetaInsumo, int idSituacaoColeta)
        {
            ColetaInsumo coletaInsumo = coletaInsumoRepository.FindByKey(idColetaInsumo);

            if (coletaInsumo.SituacaoId != idSituacaoColeta)
            {
                throw new Exception(SGIPMOMessages.MS014);
            }

            IList<string> mensagens = new List<string>();
            ValidarEstudoSituacaoColetaDados(coletaInsumo.SemanaOperativa, mensagens);
            ValidarSituacaoMonitorarDados(coletaInsumo, mensagens);
            VerificarONSBusinessException(mensagens);

            return coletaInsumo;
        }

        public PagedResult<ColetaInsumo> ConsultarColetasInsumoParaInformarDadosPaginado(PesquisaMonitorarColetaInsumoFilter filter)
        {
            PagedResult<ColetaInsumo> coletasInsumo = new PagedResult<ColetaInsumo>(new List<ColetaInsumo>(), 0, 0, 0);

            if (!filter.IdsAgentes.Any())
            {
                filter.IdsAgentes = UserInfo.ConsultarIdsAgentesUsuarioLogado();
            }

            Parametro parametroAgenteONS = parametroService.ObterParametro(ParametroEnum.CodigoAgenteONS);

            int idAgenteONS = int.Parse(parametroAgenteONS.Valor);
            if (filter.IdsAgentes.Any(id => id == idAgenteONS))
            {
                var escopos = UserInfo.ConsultarAcessosInstituicao();
                var perfis = escopos
                    .Where(e => e.IdEscopo == idAgenteONS.ToString())
                    .SelectMany(e => e.PermissaoLista)
                    .ToList();

                filter.PerfisONS = perfis;
            }

            if (filter.IdsAgentes.Any())
            {
                coletasInsumo = coletaInsumoRepository.ConsultarParaInformarDados(filter);
            }

            return coletasInsumo;
        }

        public PagedResult<ColetaInsumo> ConsultarColetasInsumoParaMonitorarDadosPaginado(
            PesquisaMonitorarColetaInsumoFilter filter)
        {
            return coletaInsumoRepository.ConsultarParaInformarDados(filter);
        }

        public void EnviarDadosColetaInsumo(EnviarDadosColetaInsumoFilter filter)
        {
            IList<string> mensagens = new List<string>();
            ValidarExisteColetaInsumoNaLista(filter.IdsColetaInsumo, mensagens);
            ValidarEstudoSituacaoColetaDados(filter.IdSemanaOperativa, mensagens);
            ValidarInsumoManutencaoTermica(filter.IdSemanaOperativa, filter.IdsColetaInsumo, mensagens);
            VerificarONSBusinessException(mensagens);

            IList<ColetaInsumo> coletaInsumoList = coletaInsumoRepository.FindByKeys(filter.IdsColetaInsumo);
            for (int i = 0; i < filter.IdsColetaInsumo.Count; i++)
            {
                ColetaInsumo coletaInsumoVerificacao = coletaInsumoList.Single(linha => linha.Id == filter.IdsColetaInsumo[i]);
                byte[] versaoEmArrayByte = Convert.FromBase64String(filter.VersoesString[i]);
                coletaInsumoRepository.ValidateConcurrency(coletaInsumoVerificacao, versaoEmArrayByte);
            }

            ValidarSituacaoColetaInsumoParaEnvioDados(coletaInsumoList, mensagens);
            VerificarONSBusinessException(mensagens);

            SituacaoColetaInsumo situacaoColetaInsumoInformado = situacaoColetaInsumoRepository
                .FindByKey((int)SituacaoColetaInsumoEnum.Informado);

            SituacaoColetaInsumo situacaoColetaInsumoAprovado = situacaoColetaInsumoRepository
                .FindByKey((int)SituacaoColetaInsumoEnum.Aprovado);

            SituacaoColetaInsumo situacaoColetaInsumoPreAprovado = situacaoColetaInsumoRepository
                .FindByKey((int)SituacaoColetaInsumoEnum.PreAprovado);

            foreach (ColetaInsumo coletaInsumo in coletaInsumoList)
            {
                //Insumo pré-aprovado
                coletaInsumo.Situacao = coletaInsumo.Insumo.PreAprovado
                    ? situacaoColetaInsumoAprovado
                    : VerificarSeDadosInsumoIguaisColetaAnterior(coletaInsumo)
                    ? situacaoColetaInsumoPreAprovado : situacaoColetaInsumoInformado;
                coletaInsumo.LoginAgenteAlteracao = "ons\\fabio.sander";//UserInfo.UserName;
                coletaInsumo.DataHoraAtualizacao = DateTime.Now;
                historicoService.CriarSalvarHistoricoColetaInsumo(coletaInsumo);
            }

            ColetaInsumo cInsumo = coletaInsumoList.FirstOrDefault();
            if (cInsumo != null)
            {
                SemanaOperativa semanaOperativa = cInsumo.SemanaOperativa;
                semanaOperativa.DataHoraAtualizacao = DateTime.Now;
            }
        }

        public bool VerificarSeDadosInsumoIguaisColetaAnterior(ColetaInsumo coletaInsumo)
        {
            List<string> grandezasPreAprovadasAlteradas = new List<string>();
            coletaInsumo.NomesGrandezasNaoEstagioAlteradas = null;

            if (coletaInsumo?.SemanaOperativa?.Revisao > 0 && coletaInsumo.DadosColeta != null)
            {
                if (!grandezaRepository.ExisteGrandezaPorEstagio(coletaInsumo.InsumoId) &&
                    !grandezaRepository.ExistePreAprovadoComAlteracao(coletaInsumo.InsumoId))
                    return false;

                ColetaInsumo coletaInsumoAnterior = coletaInsumoRepository.ObterColetaInsumoAnterior(coletaInsumo);

                if (coletaInsumoAnterior != null)
                {
                    var tipoDadoColeta = ((char)TipoDadoColetaEnum.Estruturado).ToString();

                    IList<DadoColetaEstruturado> dadosEstruturadosPorEstagio = dadoColetaEstruturadoRepository
                    .ConsultarPorFiltro(new DadoColetaInsumoFilter
                    {
                        IdsDadoColeta = coletaInsumo.DadosColeta.Where(dado => dado.TipoDadoColeta == tipoDadoColeta)
                                        .Select(d => d.Id).ToList()
                    }).ToList();
                    var EstagiosAtuais = dadosEstruturadosPorEstagio.Select(e => e.Estagio).Distinct();
                    var listUsinasColetaAtual = dadosEstruturadosPorEstagio.Select(o => o.Gabarito.OrigemColeta.Id).Distinct().ToList();

                    IList<DadoColetaEstruturado> dadosEstruturadosPorEstagioAnterior = dadoColetaEstruturadoRepository
                    .ConsultarPorFiltro(new DadoColetaInsumoFilter
                    {
                        IdsDadoColeta = coletaInsumoAnterior.DadosColeta.Where(dado => dado.TipoDadoColeta == tipoDadoColeta)
                                        .Select(d => d.Id).ToList()
                    }).ToList();

                    var listUsinasColetaAnterior = dadosEstruturadosPorEstagioAnterior.Select(o => o.Gabarito.OrigemColeta.Id).Distinct().ToList();

                    if (!listUsinasColetaAtual.OrderBy(x => x).SequenceEqual(listUsinasColetaAnterior.OrderBy(x => x)))
                        return false;

                    dadosEstruturadosPorEstagio = dadosEstruturadosPorEstagio.Where(d => !string.IsNullOrEmpty(d.Valor)).ToList();
                    dadosEstruturadosPorEstagioAnterior = dadosEstruturadosPorEstagioAnterior.Where(d => !string.IsNullOrEmpty(d.Valor)).ToList();
                    foreach (DadoColetaEstruturado dadoAnterior in dadosEstruturadosPorEstagioAnterior.Where(d => d.Grandeza.IsColetaPorEstagio && EstagiosAtuais.Contains(d.Estagio)).ToList())
                    {
                        DadoColetaEstruturado dado = null;

                        if (dadoAnterior.Grandeza.IsColetaPorEstagio)
                            dado = dadosEstruturadosPorEstagio.FirstOrDefault(d => d.Gabarito.OrigemColetaId == dadoAnterior.Gabarito.OrigemColetaId && d.GrandezaId == dadoAnterior.GrandezaId && d.TipoPatamarId == dadoAnterior.TipoPatamarId && d.TipoLimiteId == dadoAnterior.TipoLimiteId && dadoAnterior.Estagio == d.Estagio);
                        else if (dadoAnterior.Grandeza.IsPreAprovadoComAlteracao)
                            dado = dadosEstruturadosPorEstagio.FirstOrDefault(d => d.Gabarito.OrigemColetaId == dadoAnterior.Gabarito.OrigemColetaId && d.GrandezaId == dadoAnterior.GrandezaId && d.TipoPatamarId == dadoAnterior.TipoPatamarId && d.TipoLimiteId == dadoAnterior.TipoLimiteId);
                        if (dado != null)
                        {
                            if (dado.Valor?.Trim() != dadoAnterior.Valor?.Trim())
                            {
                                return false;
                            }
                        }
                        else if (!string.IsNullOrEmpty(dadoAnterior.Valor?.Trim()))
                        {
                            if (dadoAnterior.Grandeza.IsColetaPorEstagio)
                            {
                                return false;
                            }
                        }

                    }

                    if (dadosEstruturadosPorEstagio?.Where(d => d.Grandeza.IsColetaPorEstagio || d.Grandeza.IsPreAprovadoComAlteracao).Count() > 0 && dadosEstruturadosPorEstagioAnterior?.Where(d => d.Grandeza.IsColetaPorEstagio || d.Grandeza.IsPreAprovadoComAlteracao).Count() > 0)
                    {
                        foreach (DadoColetaEstruturado dado in dadosEstruturadosPorEstagio)
                        {
                            DadoColetaEstruturado dadoAnterior = null;

                            if (dado.Grandeza.IsColetaPorEstagio)
                                dadoAnterior = dadosEstruturadosPorEstagioAnterior.FirstOrDefault(d => d.Gabarito.OrigemColetaId == dado.Gabarito.OrigemColetaId && d.GrandezaId == dado.GrandezaId && d.TipoPatamarId == dado.TipoPatamarId && d.TipoLimiteId == dado.TipoLimiteId && dado.Estagio == d.Estagio);
                            else if (dado.Grandeza.IsPreAprovadoComAlteracao)
                                dadoAnterior = dadosEstruturadosPorEstagioAnterior.FirstOrDefault(d => d.Gabarito.OrigemColetaId == dado.Gabarito.OrigemColetaId && d.GrandezaId == dado.GrandezaId && d.TipoPatamarId == dado.TipoPatamarId && d.TipoLimiteId == dado.TipoLimiteId);

                            if (dadoAnterior != null)
                            {
                                if (dado.Valor?.Trim() != dadoAnterior.Valor?.Trim())
                                {
                                    if (dado.Grandeza.IsColetaPorEstagio)
                                    {
                                        return false;
                                    }
                                    else if (dado.Grandeza.IsPreAprovadoComAlteracao)
                                        grandezasPreAprovadasAlteradas.Add(dado.Grandeza.Nome);
                                }
                            }
                            else if (!string.IsNullOrEmpty(dado.Valor?.Trim()))
                            {
                                if (dado.Grandeza.IsColetaPorEstagio)
                                {
                                    return false;
                                }
                                else if (dado.Grandeza.IsPreAprovadoComAlteracao)
                                    grandezasPreAprovadasAlteradas.Add(dado.Grandeza.Nome);
                            }
                        }
                        //return true;
                    }
                    else if (!(dadosEstruturadosPorEstagio.Where(d => d.Grandeza.IsColetaPorEstagio || d.Grandeza.IsPreAprovadoComAlteracao).Count() == 0 && dadosEstruturadosPorEstagioAnterior.Where(d => d.Grandeza.IsColetaPorEstagio || d.Grandeza.IsPreAprovadoComAlteracao).Count() == 0))
                        return false;
                }
                else
                    return false;
            }
            else
            {
                return false;
            }

            if (grandezasPreAprovadasAlteradas.Any())
                coletaInsumo.NomesGrandezasNaoEstagioAlteradas = string.Join(",", grandezasPreAprovadasAlteradas.Distinct().ToArray());
            else
                coletaInsumo.NomesGrandezasNaoEstagioAlteradas = null;


            return true;

        }

        public void EnviarDadosColetaInsumoManutencao(EnviarDadosColetaInsumoManutencaoFilter filter)
        {
            ColetaInsumo coletaInsumo = coletaInsumoRepository.FindByKeyConcurrencyValidate(
                filter.IdColetaInsumo, filter.Versao);

            IList<string> mensagens = new List<string>();
            ValidarEstudoSituacaoColetaDados(coletaInsumo.SemanaOperativa.Id, mensagens);
            ValidarSituacaoColetaInsumoParaEnvioDados(coletaInsumo, mensagens);
            VerificarONSBusinessException(mensagens);

            SituacaoColetaInsumo situacaoColetaInsumoInformado = situacaoColetaInsumoRepository
                .FindByKey((int)SituacaoColetaInsumoEnum.Informado);

            SituacaoColetaInsumo situacaoColetaInsumoAprovado = situacaoColetaInsumoRepository
                .FindByKey((int)SituacaoColetaInsumoEnum.Aprovado);

            coletaInsumo.Situacao = coletaInsumo.Insumo.PreAprovado
                ? situacaoColetaInsumoAprovado
                : situacaoColetaInsumoInformado;

            historicoService.CriarSalvarHistoricoColetaInsumo(coletaInsumo);

            coletaInsumo.LoginAgenteAlteracao = UserInfo.UserName;
            coletaInsumo.DataHoraAtualizacao = DateTime.Now;

            coletaInsumo.SemanaOperativa.DataHoraAtualizacao = DateTime.Now;
        }

        #region Dado Coleta Estruturado

        public DadosInformarColetaInsumoDTO ObterDadosParaInformarDadosPorChaveColetaInsumo(ColetaInsumoFilter filter)
        {
            /*
             comentado devido a replicacao de codigo da branche sprint18_Web-PMO_Bug-76601
            ColetaInsumo coletaInsumo = coletaInsumoRepository.FindByKey(filter.IdColetaInsumo);
            */
            ColetaInsumo coletaInsumo = coletaInsumoRepository.GetByKey(filter.IdColetaInsumo.Value);

            int rowListPage;
            int qtdTotal = dadoColetaEstruturadoRepository.ContarQuantidadeLinhasDadosEstruturados(filter.IdColetaInsumo.Value);

            if (filter.PageSize == -1)
            {
                filter.PageSize = 1;
                rowListPage = qtdTotal;
            }
            else
            {
                int.TryParse(ConfigurationManager.AppSettings["grid.rowListPage"], out rowListPage);
                if (rowListPage == 0)
                {
                    rowListPage = 15;
                }
            }

            PagedResult<Gabarito> gabaritos = gabaritoRepository.ConsultarPorGabaritoFilterPaginado(
                new GabaritoConfiguracaoFilter
                {
                    IdSemanaOperativa = coletaInsumo.SemanaOperativa.Id,
                    IsPadrao = false,
                    IdAgente = coletaInsumo.Agente.Id,
                    IdInsumo = coletaInsumo.Insumo.Id,
                    CodigoPerfilONS = coletaInsumo.CodigoPerfilONS,
                    IsNullCodigoPerfilONS = string.IsNullOrEmpty(coletaInsumo.CodigoPerfilONS),
                    PageIndex = filter.PageIndex,
                    PageSize = rowListPage
                });

            bool existeGrandezaPorEstagio = grandezaRepository.ExisteGrandezaPorEstagio(coletaInsumo.Insumo.Id);

            IList<DadoColetaDTO> dadosColetaGrandeza = new List<DadoColetaDTO>();

            foreach (var gabarito in gabaritos.List)
            {
                InsumoEstruturado insumo = gabarito.Insumo as InsumoEstruturado;
                var grandezas = insumo.Grandezas.Where(g => g.Ativo).OrderBy(g => g.OrdemExibicao);
                foreach (var grandeza in grandezas)
                {
                    DadoColetaDTO dadoColetaGrandeza = new DadoColetaDTO
                    {
                        OrigemColetaId = gabarito.OrigemColeta == null ? "" : gabarito.OrigemColeta.Id,
                        OrigemColetaNome = gabarito.OrigemColeta == null ? "" : gabarito.OrigemColeta.Nome,
                        GrandezaId = grandeza.Id,
                        GrandezaNome = grandeza.Nome,
                        GrandezaOrdemExibicao = grandeza.OrdemExibicao,
                        IsColetaPorPatamar = grandeza.IsColetaPorPatamar,
                        IsColetaPorLimite = grandeza.IsColetaPorLimite,
                        IsDestacaDiferenca = grandeza.DestacaDiferenca,
                        IsRecuperaValor = grandeza.PodeRecuperarValor,
                        IsColetaPorEstagio = grandeza.IsColetaPorEstagio,
                        AceitaValorNegativo = grandeza.AceitaValorNegativo,
                        IsObrigatorio = grandeza.IsObrigatorio,
                        TipoDadoGrandeza = (TipoDadoGrandezaEnum)grandeza.TipoDadoGrandeza.Id,
                        QuantidadeCasasInteira = grandeza.QuantidadeCasasInteira ?? 0,
                        QuantidadeCasasDecimais = grandeza.QuantidadeCasasDecimais ?? 0,
                    };
                    dadosColetaGrandeza.Add(dadoColetaGrandeza);
                }
            }

            IList<TipoPatamar> tiposPatamar = tipoPatamarRepository.GetAll();
            IList<DadoColetaDTO> dadosColetaGrandezaPatamar = dadosColetaGrandeza.ToList();

            foreach (var dadoColetaDto in dadosColetaGrandezaPatamar)
            {
                if (dadoColetaDto.IsColetaPorPatamar)
                {
                    dadosColetaGrandeza.Remove(dadoColetaDto);
                    foreach (var tipoPatamar in tiposPatamar)
                    {
                        DadoColetaDTO dadosColetaPatamar = new DadoColetaDTO(dadoColetaDto);
                        dadosColetaPatamar.TipoPatamarId = tipoPatamar.Id;
                        dadosColetaPatamar.TipoPatamarDescricao = tipoPatamar.Descricao;

                        dadosColetaGrandeza.Add(dadosColetaPatamar);
                    }
                }
                else
                {
                    dadoColetaDto.TipoPatamarDescricao = string.Empty;
                }
            }

            IList<TipoLimite> tiposLimites = tipoLimiteRepository.GetAll();
            IList<DadoColetaDTO> dadosColetaGrandezaLimite = dadosColetaGrandeza.ToList();

            foreach (var dadoColetaDto in dadosColetaGrandezaLimite)
            {
                if (dadoColetaDto.IsColetaPorLimite)
                {
                    dadosColetaGrandeza.Remove(dadoColetaDto);
                    foreach (var tipoLimite in tiposLimites)
                    {
                        DadoColetaDTO dadosColetaLimite = new DadoColetaDTO(dadoColetaDto);
                        dadosColetaLimite.TipoLimiteId = tipoLimite.Id;
                        dadosColetaLimite.TipoLimiteDescricao = tipoLimite.Descricao;

                        dadosColetaGrandeza.Add(dadosColetaLimite);
                    }
                }
                else
                {
                    dadoColetaDto.TipoLimiteDescricao = string.Empty;
                }
            }

            int numeroRevisao = coletaInsumo.SemanaOperativa.Revisao;
            if (existeGrandezaPorEstagio)
            {
                InsumoEstruturado insumo = coletaInsumo.Insumo as InsumoEstruturado;
                int qtdSemanas = coletaInsumo.SemanaOperativa.PMO.SemanasOperativas.Count;
                int qtdMesesAdiante = insumo.QuantidadeMesesAdiante
                    ?? coletaInsumo.SemanaOperativa.PMO.QuantidadeMesesAdiante
                    ?? default(int);
                int qtdRevisao = qtdSemanas + qtdMesesAdiante;

                IList<SemanaOperativa> semanasPmo = coletaInsumo.SemanaOperativa.PMO.SemanasOperativas
                    .OrderBy(s => s.Revisao)
                    .ToList();

                foreach (var dadoColetaDto in dadosColetaGrandeza)
                {
                    for (int i = numeroRevisao; i < qtdRevisao; i++)
                    {
                        ValorDadoColetaDTO valorDadoColetaDto = new ValorDadoColetaDTO(dadoColetaDto);
                        valorDadoColetaDto.Estagio = i >= qtdSemanas
                            ? string.Format("M{0}", i - qtdSemanas + 1)
                            : string.Format("S{0}", i + 1);
                        valorDadoColetaDto.Valor = "";
                        valorDadoColetaDto.ValorEstudoAnterior = "";
                        valorDadoColetaDto.PeriodoSemana = i < qtdSemanas
                            ? string.Format("{0} - {1}",
                                semanasPmo[i].DataInicioSemana.ToString("dd/MM/yyyy"),
                                semanasPmo[i].DataFimSemana.ToString("dd/MM/yyyy"))
                            : string.Empty;
                        dadoColetaDto.ValoresDadoColeta.Add(valorDadoColetaDto);
                    }
                }
            }
            else
            {
                foreach (var dadoColetaDto in dadosColetaGrandeza)
                {
                    ValorDadoColetaDTO valorDadoColetaDto = new ValorDadoColetaDTO(dadoColetaDto);
                    valorDadoColetaDto.Valor = string.Empty;
                    valorDadoColetaDto.ValorEstudoAnterior = string.Empty;
                    valorDadoColetaDto.Estagio = string.Format("S{0}", coletaInsumo.SemanaOperativa.Revisao + 1);
                    valorDadoColetaDto.PeriodoSemana = string.Format("{0} - {1}",
                        coletaInsumo.SemanaOperativa.DataInicioSemana.ToShortDateString(),
                        coletaInsumo.SemanaOperativa.DataFimSemana.ToShortDateString());

                    dadoColetaDto.ValoresDadoColeta.Add(valorDadoColetaDto);
                }
            }

            dadosColetaGrandeza = dadosColetaGrandeza
                .OrderBy(c => c.OrigemColetaId)
                .ThenBy(c => c.GrandezaOrdemExibicao)
                .ThenBy(c => c.GrandezaId)
                .ToList();

            PreencherInformacoesDadosColeta(dadosColetaGrandeza, coletaInsumo, false, numeroRevisao > 0 ? true : false);

            ColetaInsumo coletaInsumoAnterior = new ColetaInsumo();
            if (numeroRevisao > 0)
                coletaInsumoAnterior = coletaInsumoRepository.ObterColetaInsumoAnterior(coletaInsumo);
            else if (numeroRevisao == 0)
                coletaInsumoAnterior = coletaInsumoRepository.ObterColetaInsumoSemanaOperativaAnterior(coletaInsumo);

            if (coletaInsumoAnterior != null)
                PreencherInformacoesDadosColeta(dadosColetaGrandeza, coletaInsumoAnterior, true, numeroRevisao > 0 ? true : false);

            PreencherRowspan(dadosColetaGrandeza);

            //int qtdTotal = dadoColetaEstruturadoRepository.ContarQuantidadeLinhasDadosEstruturados(filter.IdColetaInsumo.Value);

            PagedResult<DadoColetaDTO> dadosPaginado = new PagedResult<DadoColetaDTO>(
                dadosColetaGrandeza, qtdTotal, (int)filter.PageIndex, dadosColetaGrandeza.Count);

            DadosInformarColetaInsumoDTO dadosInformarColetaInsumoDto = new DadosInformarColetaInsumoDTO
            {
                DadosColetaInsumoPaginado = dadosPaginado
            };

            return dadosInformarColetaInsumoDto;
        }

        public DadosInformarColetaInsumoDTO ObterDadosDoGabaritoColetaInsumo(ColetaInsumo coletaInsumo, List<string> idsOrigemColeta)
        {

            int qtdTotal = dadoColetaEstruturadoRepository.ContarQuantidadeLinhasDadosEstruturados(coletaInsumo.Id);
            int rowListPage;
            ColetaInsumoFilter filter = new ColetaInsumoFilter
            {
                IdColetaInsumo = coletaInsumo.Id,
                
            };
            int.TryParse(ConfigurationManager.AppSettings["grid.rowListPage"], out rowListPage);
            if (rowListPage == 0)
            {
                rowListPage = 15;
            }

            PagedResult<Gabarito> gabaritos = gabaritoRepository.ConsultarPorGabaritoFilterPaginado(
                new GabaritoConfiguracaoFilter
                {
                    IdsOrigemColeta = idsOrigemColeta,
                    IdSemanaOperativa = coletaInsumo.SemanaOperativa.Id,
                    IsPadrao = false,
                    IdAgente = coletaInsumo.Agente.Id,
                    IdInsumo = coletaInsumo.Insumo.Id,
                    CodigoPerfilONS = coletaInsumo.CodigoPerfilONS,
                    IsNullCodigoPerfilONS = string.IsNullOrEmpty(coletaInsumo.CodigoPerfilONS),
                    PageIndex = filter.PageIndex,
                    PageSize = rowListPage
                });

            bool existeGrandezaPorEstagio = grandezaRepository.ExisteGrandezaPorEstagio(coletaInsumo.Insumo.Id);

            IList<DadoColetaDTO> dadosColetaGrandeza = new List<DadoColetaDTO>();

            foreach (var gabarito in gabaritos.List)
            {
                InsumoEstruturado insumo = gabarito.Insumo as InsumoEstruturado;
                var grandezas = insumo.Grandezas.Where(g => g.Ativo).OrderBy(g => g.OrdemExibicao);
                foreach (var grandeza in grandezas)
                {
                    DadoColetaDTO dadoColetaGrandeza = new DadoColetaDTO
                    {
                        OrigemColetaId = gabarito.OrigemColeta == null ? "" : gabarito.OrigemColeta.Id,
                        OrigemColetaNome = gabarito.OrigemColeta == null ? "" : gabarito.OrigemColeta.Nome,
                        GrandezaId = grandeza.Id,
                        GrandezaNome = grandeza.Nome,
                        GrandezaOrdemExibicao = grandeza.OrdemExibicao,
                        IsColetaPorPatamar = grandeza.IsColetaPorPatamar,
                        IsColetaPorLimite = grandeza.IsColetaPorLimite,
                        IsDestacaDiferenca = grandeza.DestacaDiferenca,
                        IsRecuperaValor = grandeza.PodeRecuperarValor,
                        IsColetaPorEstagio = grandeza.IsColetaPorEstagio,
                        AceitaValorNegativo = grandeza.AceitaValorNegativo,
                        IsObrigatorio = grandeza.IsObrigatorio,
                        TipoDadoGrandeza = (TipoDadoGrandezaEnum)grandeza.TipoDadoGrandeza.Id,
                        QuantidadeCasasInteira = grandeza.QuantidadeCasasInteira ?? 0,
                        QuantidadeCasasDecimais = grandeza.QuantidadeCasasDecimais ?? 0,
                    };
                    dadosColetaGrandeza.Add(dadoColetaGrandeza);
                }
            }

            IList<TipoPatamar> tiposPatamar = tipoPatamarRepository.GetAll();
            IList<DadoColetaDTO> dadosColetaGrandezaPatamar = dadosColetaGrandeza.ToList();

            foreach (var dadoColetaDto in dadosColetaGrandezaPatamar)
            {
                if (dadoColetaDto.IsColetaPorPatamar)
                {
                    dadosColetaGrandeza.Remove(dadoColetaDto);
                    foreach (var tipoPatamar in tiposPatamar)
                    {
                        DadoColetaDTO dadosColetaPatamar = new DadoColetaDTO(dadoColetaDto);
                        dadosColetaPatamar.TipoPatamarId = tipoPatamar.Id;
                        dadosColetaPatamar.TipoPatamarDescricao = tipoPatamar.Descricao;

                        dadosColetaGrandeza.Add(dadosColetaPatamar);
                    }
                }
                else
                {
                    dadoColetaDto.TipoPatamarDescricao = string.Empty;
                }
            }

            IList<TipoLimite> tiposLimites = tipoLimiteRepository.GetAll();
            IList<DadoColetaDTO> dadosColetaGrandezaLimite = dadosColetaGrandeza.ToList();

            foreach (var dadoColetaDto in dadosColetaGrandezaLimite)
            {
                if (dadoColetaDto.IsColetaPorLimite)
                {
                    dadosColetaGrandeza.Remove(dadoColetaDto);
                    foreach (var tipoLimite in tiposLimites)
                    {
                        DadoColetaDTO dadosColetaLimite = new DadoColetaDTO(dadoColetaDto);
                        dadosColetaLimite.TipoLimiteId = tipoLimite.Id;
                        dadosColetaLimite.TipoLimiteDescricao = tipoLimite.Descricao;

                        dadosColetaGrandeza.Add(dadosColetaLimite);
                    }
                }
                else
                {
                    dadoColetaDto.TipoLimiteDescricao = string.Empty;
                }
            }

            int numeroRevisao = coletaInsumo.SemanaOperativa.Revisao;
            if (existeGrandezaPorEstagio)
            {
                InsumoEstruturado insumo = coletaInsumo.Insumo as InsumoEstruturado;
                int qtdSemanas = coletaInsumo.SemanaOperativa.PMO.SemanasOperativas.Count;
                int qtdMesesAdiante = insumo.QuantidadeMesesAdiante
                    ?? coletaInsumo.SemanaOperativa.PMO.QuantidadeMesesAdiante
                    ?? default(int);
                int qtdRevisao = qtdSemanas + qtdMesesAdiante;

                IList<SemanaOperativa> semanasPmo = coletaInsumo.SemanaOperativa.PMO.SemanasOperativas
                    .OrderBy(s => s.Revisao)
                    .ToList();

                foreach (var dadoColetaDto in dadosColetaGrandeza)
                {
                    for (int i = numeroRevisao; i < qtdRevisao; i++)
                    {
                        ValorDadoColetaDTO valorDadoColetaDto = new ValorDadoColetaDTO(dadoColetaDto);
                        valorDadoColetaDto.Estagio = i >= qtdSemanas
                            ? string.Format("M{0}", i - qtdSemanas + 1)
                            : string.Format("S{0}", i + 1);
                        valorDadoColetaDto.Valor = "";
                        valorDadoColetaDto.ValorEstudoAnterior = "";
                        valorDadoColetaDto.PeriodoSemana = i < qtdSemanas
                            ? string.Format("{0} - {1}",
                                semanasPmo[i].DataInicioSemana.ToString("dd/MM/yyyy"),
                                semanasPmo[i].DataFimSemana.ToString("dd/MM/yyyy"))
                            : string.Empty;
                        dadoColetaDto.ValoresDadoColeta.Add(valorDadoColetaDto);
                    }
                }
            }
            else
            {
                foreach (var dadoColetaDto in dadosColetaGrandeza)
                {
                    ValorDadoColetaDTO valorDadoColetaDto = new ValorDadoColetaDTO(dadoColetaDto);
                    valorDadoColetaDto.Valor = string.Empty;
                    valorDadoColetaDto.ValorEstudoAnterior = string.Empty;
                    valorDadoColetaDto.Estagio = string.Format("S{0}", coletaInsumo.SemanaOperativa.Revisao + 1);
                    valorDadoColetaDto.PeriodoSemana = string.Format("{0} - {1}",
                        coletaInsumo.SemanaOperativa.DataInicioSemana.ToShortDateString(),
                        coletaInsumo.SemanaOperativa.DataFimSemana.ToShortDateString());

                    dadoColetaDto.ValoresDadoColeta.Add(valorDadoColetaDto);
                }
            }

            dadosColetaGrandeza = dadosColetaGrandeza
                .OrderBy(c => c.OrigemColetaId)
                .ThenBy(c => c.GrandezaOrdemExibicao)
                .ThenBy(c => c.GrandezaId)
                .ToList();

            PreencherInformacoesDadosColeta(dadosColetaGrandeza, coletaInsumo, false, numeroRevisao > 0 ? true : false);

            PreencherRowspan(dadosColetaGrandeza);

            PagedResult<DadoColetaDTO> dadosPaginado = new PagedResult<DadoColetaDTO>(
                dadosColetaGrandeza, qtdTotal, (int)filter.PageIndex, dadosColetaGrandeza.Count);

            DadosInformarColetaInsumoDTO dadosInformarColetaInsumoDto = new DadosInformarColetaInsumoDTO
            {
                DadosColetaInsumoPaginado = dadosPaginado
            };

            return dadosInformarColetaInsumoDto;
        }

        private void PreencherRowspan(IList<DadoColetaDTO> dadosColetaDtos)
        {
            var dadosRowspan = from d in dadosColetaDtos
                               group d by new
                               {
                                   d.OrigemColetaId,
                                   d.GrandezaId
                               }
                                   into grupo
                               select new
                               {
                                   grupo.Key,
                                   RowspanUsina = dadosColetaDtos.Count(e => e.OrigemColetaId == grupo.Key.OrigemColetaId),
                                   RowspanGrandeza = grupo.Count()
                               };

            string idOrigemColeta = null;
            foreach (var item in dadosRowspan)
            {
                var dadoColetaDto = dadosColetaDtos.FirstOrDefault(d => d.OrigemColetaId == item.Key.OrigemColetaId
                                                                        && d.GrandezaId == item.Key.GrandezaId);
                if (dadoColetaDto != null)
                {
                    if (idOrigemColeta != item.Key.OrigemColetaId)
                    {
                        dadoColetaDto.RowspanUsina = item.RowspanUsina;
                        idOrigemColeta = item.Key.OrigemColetaId;
                    }
                    dadoColetaDto.RowspanGrandeza = item.RowspanGrandeza;
                }
            }
        }

        private void PreencherInformacoesDadosColeta(IList<DadoColetaDTO> dadosColetaDtos, ColetaInsumo coletaInsumo,
            bool isColetaAnterior, bool isColetaAnteriorRevisao)
        {
            List<DadoColetaEstruturado> dadosColetaEstruturado = coletaInsumo.DadosColeta
                .Cast<DadoColetaEstruturado>()
                .ToList();

            if (dadosColetaEstruturado.Any())
            {
                foreach (var dto in dadosColetaDtos)
                {
                    var dadosColeta = string.IsNullOrEmpty(dto.OrigemColetaId)
                        ? dadosColetaEstruturado.Where(d => d.Gabarito.OrigemColeta == null).ToList()
                        : dadosColetaEstruturado.Where(d => d.Gabarito.OrigemColeta.Id == dto.OrigemColetaId).ToList();

                    dadosColeta = dadosColeta
                        .Where(d => d.Grandeza.Id == dto.GrandezaId
                            && (dto.TipoPatamarId == 0 && d.TipoPatamar == null
                                || d.TipoPatamar != null && dto.TipoPatamarId == d.TipoPatamar.Id)
                            && (dto.TipoLimiteId == 0 && d.TipoLimite == null
                                || d.TipoLimite != null && dto.TipoLimiteId == d.TipoLimite.Id))
                        .ToList();

                    /* Ordenar por estágio */
                    dadosColeta.Sort();

                    if (dadosColeta.Any(d => d.Grandeza.IsColetaPorEstagio))
                    {
                        foreach (var valorDadoColeta in dto.ValoresDadoColeta)
                        {
                            var dadoColeta = dadosColeta.FirstOrDefault(d => d.Estagio == valorDadoColeta.Estagio);

                            if (isColetaAnterior)
                            {
                                if (dadoColeta != null)
                                {
                                    valorDadoColeta.ValorEstudoAnterior = string.IsNullOrEmpty(dadoColeta.Valor)
                                        ? string.Empty : dadoColeta.Valor;
                                }
                                //Se for a primeira semana operativa do estudo, replica os valores para todas as colunas
                                else if (!isColetaAnteriorRevisao)
                                {
                                    dadoColeta = dadosColeta.Where(x => x.Estagio.Contains("S")).OrderByDescending(ci => ci.Estagio).FirstOrDefault();
                                    if (dadoColeta != null)
                                    {
                                        valorDadoColeta.ValorEstudoAnterior = string.IsNullOrEmpty(dadoColeta.Valor)
                                            ? string.Empty : dadoColeta.Valor;
                                    }
                                }
                            }
                            else
                            {
                                if (dadoColeta != null)
                                {
                                    valorDadoColeta.Id = dadoColeta.Id;
                                    valorDadoColeta.Valor = dadoColeta.Valor;
                                    valorDadoColeta.DestacaModificacao = dadoColeta.DestacaModificacao;
                                }
                            }
                        }
                    }
                    else
                    {
                        var valorDadoColeta = dto.ValoresDadoColeta.FirstOrDefault();
                        var dadoColeta = dadosColeta.FirstOrDefault();

                        if (dadoColeta != null)
                        {
                            if (isColetaAnterior)
                            {
                                valorDadoColeta.ValorEstudoAnterior = string.IsNullOrEmpty(dadoColeta.Valor)
                                    ? string.Empty : dadoColeta.Valor;
                            }
                            else
                            {
                                valorDadoColeta.Id = dadoColeta.Id;
                                valorDadoColeta.Valor = dadoColeta.Valor;
                                valorDadoColeta.DestacaModificacao = dadoColeta.DestacaModificacao;
                            }
                        }
                    }
                }
            }
        }

        public void EnviarColetaDadosEstruturados(IList<ValorDadoColetaDTO> dtos, int idColetaInsumo, string versao)
        {
            byte[] versaoColetaInsumo = Convert.FromBase64String(versao);
            ColetaInsumo coletaInsumo = coletaInsumoRepository.FindByKeyConcurrencyValidate(
                idColetaInsumo, versaoColetaInsumo);


            List<string> mensagens = new List<string>();
            ValidarEstudoSituacaoColetaDados(coletaInsumo.SemanaOperativa.Id, mensagens);
            ValidarSituacaoColetaInsumoParaEnvioDados(coletaInsumo, mensagens);
            VerificarONSBusinessException(mensagens);
            ValidarPreenchimentoDaGrandezaCausa(coletaInsumo, dtos);
            ValidarPreenchimentoDaInflexibilidadeDisponibilidade(coletaInsumo, dtos);
            ValidarPreenchimentoDaInflexibilidadeDisponibilidadeEletrobras(coletaInsumo, dtos);
            coletaInsumo.DataHoraAtualizacao = DateTime.Now;
            coletaInsumo.LoginAgenteAlteracao = UserInfo.UserName;

            coletaInsumo.SemanaOperativa.DataHoraAtualizacao = DateTime.Now;

            SituacaoColetaInsumo situacaoColetaInsumoInformado = situacaoColetaInsumoRepository
                .FindByKey((int)SituacaoColetaInsumoEnum.Informado);

            SituacaoColetaInsumo situacaoColetaInsumoAprovado = situacaoColetaInsumoRepository
                .FindByKey((int)SituacaoColetaInsumoEnum.Aprovado);

            SituacaoColetaInsumo situacaoColetaInsumoPreAprovado = situacaoColetaInsumoRepository
                .FindByKey((int)SituacaoColetaInsumoEnum.PreAprovado);

            //Insumo pré-aprovado
            //coletaInsumo.Situacao = coletaInsumo.Insumo.PreAprovado
            //    ? situacaoColetaInsumoAprovado : situacaoColetaInsumoInformado;

            if (dtos.Any())
            {
                SalvarDadosEstruturados(dtos, coletaInsumo, false, true);
            }


            List<string> auxMensages = new List<string>();
            List<int> ids = new List<int> { idColetaInsumo };
            ValidarInsumoManutencaoTermica(coletaInsumo.SemanaOperativa.Id, ids, auxMensages);
            // sem mensagens de validacao --> pode enviar
            if (auxMensages.Count == 0)
            {
                coletaInsumo.Situacao = coletaInsumo.Insumo.PreAprovado
                        ? situacaoColetaInsumoAprovado
                        : VerificarSeDadosInsumoIguaisColetaAnterior(coletaInsumo)
                        ? situacaoColetaInsumoPreAprovado : situacaoColetaInsumoInformado;

                historicoService.CriarSalvarHistoricoColetaInsumo(coletaInsumo);
            }
            else
            {
                // com mensagem de validacao - nao marcar como envido, somente em andamento
                VerificarONSBusinessException(auxMensages);

                if (coletaInsumo.Situacao.Id == (int)SituacaoColetaInsumoEnum.NaoIniciado
                    || coletaInsumo.Situacao.Id == (int)SituacaoColetaInsumoEnum.Rejeitado)
                {
                    coletaInsumo.Situacao =
                        situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.EmAndamento);

                    historicoService.CriarSalvarHistoricoColetaInsumo(coletaInsumo);
                }

            }

            //if (dtos.Any())
            //{
            //    SalvarDadosEstruturados(dtos, coletaInsumo);
            //}

        }



        public void SalvarColetaDadosEstruturados(IList<ValorDadoColetaDTO> dtos, DadoColetaInsumoDTO dto)
        {
            byte[] versaoColetaInsumo = Convert.FromBase64String(dto.VersaoString);
            ColetaInsumo coletaInsumo = coletaInsumoRepository.FindByKeyConcurrencyValidate(
                dto.IdColetaInsumo, versaoColetaInsumo);


            ValidarPermiteManutencaoColetaInsumo(coletaInsumo, dto.IsMonitorar, null);
            ValidarPreenchimentoDaGrandezaCausa(coletaInsumo, dtos);
            ValidarPreenchimentoDaInflexibilidadeDisponibilidade(coletaInsumo, dtos);
            ValidarPreenchimentoDaInflexibilidadeDisponibilidadeEletrobras(coletaInsumo, dtos);

            coletaInsumo.MotivoAlteracaoONS = dto.MotivoAlteracaoONS;
            coletaInsumo.DataHoraAtualizacao = DateTime.Now;
            coletaInsumo.LoginAgenteAlteracao = UserInfo.UserName;
            coletaInsumo.SemanaOperativa.DataHoraAtualizacao = DateTime.Now;

            bool isAdmProcesso =
                UserInfo.IsUserInRole(UserInfo.UserName, RolePermissoesPopEnum.AdministradorProcesso.ToDescription());

            if (coletaInsumo.Situacao.Id == (int)SituacaoColetaInsumoEnum.NaoIniciado
                || coletaInsumo.Situacao.Id == (int)SituacaoColetaInsumoEnum.Rejeitado)
            {
                coletaInsumo.Situacao =
                    situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.EmAndamento);

                historicoService.CriarSalvarHistoricoColetaInsumo(coletaInsumo);
            }

            if (dtos.Any())
            {
                SalvarDadosEstruturados(dtos, coletaInsumo, isAdmProcesso);
            }
        }


        public void SalvarColetaDadosEstruturadosEmLote(IList<ValorDadoColetaDTO> dtos, DadoColetaInsumoDTO dto)
        {
            byte[] versaoColetaInsumo = Convert.FromBase64String(dto.VersaoString);
            ColetaInsumo coletaInsumo = coletaInsumoRepository.FindByKeyConcurrencyValidate(
                dto.IdColetaInsumo, versaoColetaInsumo);

            //ValidarPermiteManutencaoColetaInsumo(coletaInsumo, dto.IsMonitorar, null);

            coletaInsumo.MotivoAlteracaoONS = dto.MotivoAlteracaoONS;
            coletaInsumo.DataHoraAtualizacao = DateTime.Now;
            coletaInsumo.LoginAgenteAlteracao = UserInfo.UserName;
            coletaInsumo.SemanaOperativa.DataHoraAtualizacao = DateTime.Now;

            bool isAdmProcesso =
                UserInfo.IsUserInRole(UserInfo.UserName, RolePermissoesPopEnum.AdministradorProcesso.ToDescription());

            if (coletaInsumo.Situacao.Id == (int)SituacaoColetaInsumoEnum.NaoIniciado
                || coletaInsumo.Situacao.Id == (int)SituacaoColetaInsumoEnum.Rejeitado)
            {
                coletaInsumo.Situacao =
                    situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.EmAndamento);

                historicoService.CriarSalvarHistoricoColetaInsumo(coletaInsumo);
            }

            if (dtos.Any())
            {
                SalvarDadosEstruturados(dtos, coletaInsumo, isAdmProcesso);
            }
        }


        private void SalvarDadosEstruturados(IList<ValorDadoColetaDTO> dtos, ColetaInsumo coletaInsumo,
            bool isAdmProcesso = false, bool saveChanges = false)
        {
            ValidarTipoDadoColetado(dtos);

            var dadosAlteracao = dtos.Where(v => v.Id > 0).ToList();
            var dadosInsercao = dtos.Where(v => v.Id == 0).ToList();

            if (dadosAlteracao.Any())
            {
                IList<int> idsDadoColeta = dadosAlteracao.Select(d => d.Id).ToList();

                IList<DadoColetaEstruturado> dadosEstruturadosAlteracao = dadoColetaEstruturadoRepository
                    .ConsultarPorFiltro(new DadoColetaInsumoFilter { IdsDadoColeta = idsDadoColeta });

                foreach (var dadoColeta in dadosEstruturadosAlteracao)
                {
                    var valorDadoColetaDto = dadosAlteracao.FirstOrDefault(d => d.Id == dadoColeta.Id);
                    if (valorDadoColetaDto != null)
                    {
                        dadoColeta.Valor = valorDadoColetaDto.Valor ?? string.Empty;
                        dadoColeta.DestacaModificacao = isAdmProcesso;
                    }
                }
            }

            if (dadosInsercao.Any())
            {
                IList<TipoLimite> tiposLimites = tipoLimiteRepository.GetAll();
                IList<TipoPatamar> tiposPatamar = tipoPatamarRepository.GetAll();

                GabaritoConfiguracaoFilter filter = new GabaritoConfiguracaoFilter()
                {
                    IdAgente = coletaInsumo.Agente.Id,
                    IdInsumo = coletaInsumo.Insumo.Id,
                    IdSemanaOperativa = coletaInsumo.SemanaOperativa.Id,
                    CodigoPerfilONS = coletaInsumo.CodigoPerfilONS,
                    IsNullCodigoPerfilONS = string.IsNullOrEmpty(coletaInsumo.CodigoPerfilONS)
                };

                IList<Gabarito> gabaritos = gabaritoRepository.ConsultarPorGabaritoFilter(filter);

                IList<int> idsGrandeza = dtos.Select(d => d.GrandezaId).ToList();
                IList<Grandeza> grandezas = grandezaRepository
                    .ConsultarPorFiltro(new GrandezaFilter { IdsGrandeza = idsGrandeza });

                foreach (var valorDadoColetaDto in dadosInsercao)
                {
                    DadoColetaEstruturado dadoColeta = new DadoColetaEstruturado
                    {
                        ColetaInsumo = coletaInsumo,
                        Estagio = valorDadoColetaDto.Estagio,
                        Gabarito = string.IsNullOrEmpty(valorDadoColetaDto.OrigemColetaId)
                            ? gabaritos.FirstOrDefault(g => g.OrigemColeta == null) //Geração Complementar
                            : gabaritos.FirstOrDefault(g => g.OrigemColeta.Id == valorDadoColetaDto.OrigemColetaId),
                        Grandeza = grandezas.FirstOrDefault(g => g.Id == valorDadoColetaDto.GrandezaId),
                        TipoDadoColeta = ((char)TipoDadoColetaEnum.Estruturado).ToString(),
                        TipoLimite = tiposLimites.FirstOrDefault(t => t.Id == valorDadoColetaDto.TipoLimiteId),
                        TipoPatamar = tiposPatamar.FirstOrDefault(t => t.Id == valorDadoColetaDto.TipoPatamarId),
                        Valor = valorDadoColetaDto.Valor,
                        DestacaModificacao = isAdmProcesso
                    };

                    dadoColetaEstruturadoRepository.Add(dadoColeta, false);
                }

                if (saveChanges)
                {
                    dadoColetaEstruturadoRepository.Add();
                }
            }
        }

        public void AprovarColetaDadosEstruturados(DadoColetaInsumoDTO dadoColetaInsumoDto, IList<ValorDadoColetaDTO> valoresDto)
        {
            SalvarColetaDadosEstruturados(valoresDto, dadoColetaInsumoDto);

            ColetaInsumo coletaInsumo = coletaInsumoRepository.FindByKey(dadoColetaInsumoDto.IdColetaInsumo);

            coletaInsumo.Situacao = situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.Aprovado);

            historicoService.CriarSalvarHistoricoColetaInsumo(coletaInsumo);
        }

        public void AprovarColetaDadosEstruturadosEmLote(DadoColetaInsumoDTO dadoColetaInsumoDto, IList<ValorDadoColetaDTO> valoresDto)
        {
            SalvarColetaDadosEstruturadosEmLote(valoresDto, dadoColetaInsumoDto);

            ColetaInsumo coletaInsumo = coletaInsumoRepository.FindByKey(dadoColetaInsumoDto.IdColetaInsumo);

            coletaInsumo.Situacao = situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.Aprovado);

            historicoService.CriarSalvarHistoricoColetaInsumo(coletaInsumo);
        }

        public void RejeitarColetaDadosEstruturados(DadoColetaInsumoDTO dadoColetaInsumoDto)
        {
            IList<string> mensagens = new List<string>();
            ValidarMotivoRejeicaoColeta(dadoColetaInsumoDto.MotivoRejeicaoONS, mensagens);
            VerificarONSBusinessException(mensagens);

            byte[] versaoColetaInsumo = Convert.FromBase64String(dadoColetaInsumoDto.VersaoString);
            ColetaInsumo coletaInsumo = coletaInsumoRepository.FindByKeyConcurrencyValidate(
                dadoColetaInsumoDto.IdColetaInsumo, versaoColetaInsumo);
            coletaInsumo.MotivoAlteracaoONS = dadoColetaInsumoDto.MotivoAlteracaoONS;
            coletaInsumo.MotivoRejeicaoONS = dadoColetaInsumoDto.MotivoRejeicaoONS;
            coletaInsumo.DataHoraAtualizacao = DateTime.Now;
            coletaInsumo.LoginAgenteAlteracao = UserInfo.UserName;

            coletaInsumo.SemanaOperativa.DataHoraAtualizacao = DateTime.Now;

            coletaInsumo.Situacao = situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.Rejeitado);

            historicoService.CriarSalvarHistoricoColetaInsumo(coletaInsumo);

            Parametro parametro = parametroService.ObterParametro(ParametroEnum.MensagemRejeicaoColeta);

            string assunto = string.Format("[ONS-WEBPMO] Rejeição da coleta do insumo {0} do agente {1}",
                coletaInsumo.Insumo.Nome, coletaInsumo.Agente.Nome);

            List<int> idAgentes = new List<int> { coletaInsumo.AgenteId };
            notificacaoService.NotificarUsuariosPorAgente(coletaInsumo.Agente.Id, assunto, parametro.Valor);
            //notificacaoService.NotificarUsuariosPorAgentes(idAgentes, assunto, parametro.Valor);            
            List<Agente> agentes = new List<Agente>();
            agentes.Add(coletaInsumo.Agente);
            //Agente agente = new Agente();
            //agente.Id = idAgentes.FirstOrDefault();
            //agentes.Add(agente);
            logNotificacaoService.LogarNotificacao(coletaInsumo.SemanaOperativa, agentes,
                coletaInsumo.SemanaOperativa.DataHoraAtualizacao, UserInfo.UserName, LogNotificacaoService.LOG_NOTIFICACAO_REJEICAO);
        }

        #endregion

        #region Validações
        private void VerificarONSBusinessException(IList<string> mensagens)
        {
            if (mensagens.Any())
            {
                throw new ONSBusinessException(mensagens);
            }
        }

        /// <summary>
        /// [MS043]:O sistema deverá mostrar uma mensagem de erro caso o usuário acionar 
        /// a opção para “Enviar” sem selecionar nenhuma coleta. Observação: seleção efetuada 
        /// em um página não é mantida quando se sai dela. 
        /// </summary>
        /// <param name="idsColetaInsumo">Ids das coletas a serem validadas</param>
        /// <param name="mensagens">Lista contendo os erros de negócio</param>
        private void ValidarExisteColetaInsumoNaLista(IList<int> idsColetaInsumo, IList<string> mensagens)
        {
            if (!idsColetaInsumo.Any())
            {
                mensagens.Add(SGIPMOMessages.MS043);
            }
        }

        /// <summary>
        /// [MS023]:O sistema não deverá permitir ao usuário informar ValorDado caso o estudo não
        /// esteja no estado processo “Coleta de ValorDado”
        /// </summary>
        /// <param name="idSemanaOperativa">Id do estudo a ser validado</param>
        /// <param name="mensagens">Lista contendo os erros de negócio</param>
        private void ValidarEstudoSituacaoColetaDados(int idSemanaOperativa, IList<string> mensagens)
        {
            SemanaOperativa semanaOperativa = semanaOperativaRepository.FindByKey(idSemanaOperativa);
            ValidarEstudoSituacaoColetaDados(semanaOperativa, mensagens);
        }

        private void ValidarEstudoSituacaoColetaDados(SemanaOperativa semanaOperativa, IList<string> mensagens)
        {
            if (semanaOperativa.Situacao.Id != (int)SituacaoSemanaOperativaEnum.ColetaDados)
            {
                mensagens.Add(SGIPMOMessages.MS023);
            }
        }

        private void ValidarSituacaoColetaInsumoParaEnvioDados(IList<ColetaInsumo> coletaInsumoList, IList<string> mensagens)
        {
            foreach (ColetaInsumo coletaInsumo in coletaInsumoList)
            {
                ValidarSituacaoColetaInsumoParaEnvioDados(coletaInsumo, mensagens);
            }
        }

        /// <summary>
        /// [MS035]:O sistema só deve permitir o envio de coleta de insumos que estejam nas situações:
        /// não iniciada, em andamento ou rejeitada.
        /// </summary>
        /// <param name="coletaInsumo">ColetaInsumo a ser validada</param>
        /// <param name="mensagens">Lista contendo os erros de negócio</param>
        private void ValidarSituacaoColetaInsumoParaEnvioDados(ColetaInsumo coletaInsumo, IList<string> mensagens)
        {
            SituacaoColetaInsumoEnum situacao = (SituacaoColetaInsumoEnum)coletaInsumo.Situacao.Id;
            if (situacao != SituacaoColetaInsumoEnum.NaoIniciado
                && situacao != SituacaoColetaInsumoEnum.EmAndamento
                && situacao != SituacaoColetaInsumoEnum.Rejeitado)
            {
                string mensagem = string.Format(SGIPMOMessages.MS035, coletaInsumo.Insumo.Nome, coletaInsumo.NomeAgentePerfil);
                mensagens.Add(mensagem);
            }
        }

        private void ValidarMotivoRejeicaoColeta(string motivoRejeicao, IList<string> mensagens)
        {
            if (string.IsNullOrWhiteSpace(motivoRejeicao))
            {
                string mensagem = string.Format(SGIPMOMessages.MS001, "Motivo Rejeição ONS");
                mensagens.Add(mensagem);
            }
        }

        private void ValidarMotivoAlteracaoColeta(string motivoAlteracao)
        {
            if (string.IsNullOrWhiteSpace(motivoAlteracao))
            {
                IList<string> mensagens = new List<string>();
                mensagens.Add(string.Format(SGIPMOMessages.MS001, "Motivo de alteração"));
                VerificarONSBusinessException(mensagens);
            }
        }

        /// <summary>
        /// [MS048]: O sistema não deverá permitir a abertura da coleta de um estudo 
        /// que esteja no estado/processo “Convergência com CCEE”.
        /// </summary>
        /// <param name="semanaOperativa">Estudo a ser validado</param>
        /// <param name="mensagens">Lista contendo os erros de negócio</param>
        private void ValidarEstudoConvergenciaCCEE(SemanaOperativa semanaOperativa, IList<string> mensagens)
        {
            if (semanaOperativa.Situacao.Id == (int)SituacaoSemanaOperativaEnum.ConvergenciaCCEE)
            {
                mensagens.Add(SGIPMOMessages.MS048);
            }
        }

        private void ValidarSituacaoColetaAoEfetuarCaptura(ColetaInsumo coletaInsumo, IList<string> mensagens)
        {
            if (coletaInsumo.Situacao.Id != (int)SituacaoColetaInsumoEnum.EmAndamento
                && coletaInsumo.Situacao.Id != (int)SituacaoColetaInsumoEnum.Rejeitado
                && coletaInsumo.Situacao.Id != (int)SituacaoColetaInsumoEnum.NaoIniciado)
            {
                string mensagem = string.Format(SGIPMOMessages.MS026, coletaInsumo.Insumo.Nome, coletaInsumo.NomeAgentePerfil);
                mensagens.Add(mensagem);
            }
        }

        /// <summary>
        /// O informar dados só pode ser realizada apenas para coletas que estejam nas situações: 
        /// não iniciada, em andamento ou rejeitada.
        /// </summary>
        /// <param name="coletaInsumo">Coleta de insumo a ser validada</param>
        /// <param name="mensagens">Lista contendo os erros de negócio</param>
        private void ValidarSituacaoColetaAoInformarDados(ColetaInsumo coletaInsumo, IList<string> mensagens)
        {
            if (coletaInsumo.Situacao.Id != (int)SituacaoColetaInsumoEnum.NaoIniciado
                && coletaInsumo.Situacao.Id != (int)SituacaoColetaInsumoEnum.EmAndamento
                && coletaInsumo.Situacao.Id != (int)SituacaoColetaInsumoEnum.Rejeitado
                && coletaInsumo.Situacao.Id != (int)SituacaoColetaInsumoEnum.Informado)
            {
                var mensagem = "";
                if (coletaInsumo.Situacao.Id == (int)SituacaoColetaInsumoEnum.Aprovado)
                    mensagem = string.Format(SGIPMOMessages.MS076, coletaInsumo.Insumo.Nome);
                else
                    mensagem = string.Format(SGIPMOMessages.MS061, coletaInsumo.Insumo.Nome, coletaInsumo.NomeAgentePerfil);

                mensagens.Add(mensagem);
            }
        }

        /// <summary>
        /// [MS076]: As alterações não podem ser aplicadas pois o insumo {0} já foi aprovado pela ONS.
        /// </summary>
        /// <param name="coletaInsumo">Coleta de insumo a ser validada</param>
        /// <param name="mensagens">Lista contendo os erros de negócio</param>
        private void ValidarSituacaoColetaJaAprovado(ColetaInsumo coletaInsumo, IList<string> mensagens)
        {
            if (coletaInsumo.Situacao.Id == (int)SituacaoColetaInsumoEnum.Aprovado)
            {
                string mensagem = string.Format(SGIPMOMessages.MS076, coletaInsumo.Insumo.Nome);
                mensagens.Add(mensagem);
            }
        }

        /// <summary>
        /// [MS027]: O sistema não deve permitir que a coleta de dados de um estudo seja encerrada, 
        /// se ainda existir insumos cujos dados que ainda não foram aprovados pelo usuário.
        /// </summary>
        /// <param name="idSemanaOperativa">Id do Estudo</param>
        /// <param name="mensagens">Lista contendo os erros de negócio</param>
        private void ValidarExisteColetaNaoAprovado(int idSemanaOperativa, IList<string> mensagens)
        {
            if (coletaInsumoRepository.AnyNaoAprovado(idSemanaOperativa))
            {
                mensagens.Add(SGIPMOMessages.MS027);
            }
        }

        /// <summary>
        /// [MS024]:O sistema não deverá permitir ao usuário efetuar análise de coleta de insumo
        /// que ainda não tenha sido enviada pelo agente, ou seja, as coletas de insumo que estão nas situações: 
        /// “Não iniciada”, “Em andamento”, “Rejeitada”
        /// </summary>
        /// <param name="coletaInsumo">Coleta insumo a ser validada</param>
        /// <param name="mensagens">Lista contendo os erros de negócio</param>
        private void ValidarSituacaoMonitorarDados(ColetaInsumo coletaInsumo, IList<string> mensagens)
        {
            if (coletaInsumo.Situacao.Id == (int)SituacaoColetaInsumoEnum.EmAndamento
                || coletaInsumo.Situacao.Id == (int)SituacaoColetaInsumoEnum.NaoIniciado
                || coletaInsumo.Situacao.Id == (int)SituacaoColetaInsumoEnum.Rejeitado)
            {
                mensagens.Add(SGIPMOMessages.MS024);
            }
        }

        /// <summary>
        /// Verifica se é permitido alterar a coleta de insumo na situação atual.
        /// Monitorar: Sistema não deve permitir caso a situação da coleta de insumo esteja: Não iniciado, Em andamento ou rejeitado;
        /// Informar: Sistema não deve permitir caso a situação da coleta de insumo não estava: Não iniciado, Em andamento e rejeitado;
        /// </summary>
        /// <param name="coletaInsumo">Coleta de insumo a ser validada</param>
        /// <param name="isMonitorar">Indicador se a alteração será em monitorar ou informar dados</param>
        /// <param name="dadoColeta">Dado coleta que será usado para validar datas de início e término. Se passar "null" a validação não será feita.</param>
        private void ValidarPermiteManutencaoColetaInsumo(ColetaInsumo coletaInsumo,
            bool isMonitorar, DadoColetaManutencao dadoColeta)
        {
            IList<string> mensagens = new List<string>();

            if (isMonitorar)
            {
                ValidarSituacaoMonitorarDados(coletaInsumo, mensagens);
            }
            else
            {
                ValidarSituacaoColetaAoInformarDados(coletaInsumo, mensagens);
            }

            if (dadoColeta != null)
            {
                ValidarDatasManutencao(dadoColeta, mensagens);
            }

            VerificarONSBusinessException(mensagens);
        }


        /// <summary>
        /// Verifica se a grandeza causa foi preenchida corretamente;        
        /// </summary>
        /// <param name="coletaInsumo">Coleta de insumo a ser validada</param>
        /// <param name="dtos">Dados de coleta que seram usados para validar o preenchimento da grandeza Causa.</param>
        private void ValidarPreenchimentoDaGrandezaCausa(ColetaInsumo coletaInsumo, IList<ValorDadoColetaDTO> dtos)
        {
            IList<string> mensagens = new List<string>();
            string[] idsInsumosRegraGrandezaCausa = ConfigurationManager.AppSettings["InsumosRegraGrandezaCausa"].Split(';');

            if (dtos.Any() && idsInsumosRegraGrandezaCausa != null)
            {
                bool isVerificaCausa = idsInsumosRegraGrandezaCausa.Contains(coletaInsumo.InsumoId.ToString());

                if (isVerificaCausa)
                {
                    var usinas = dtos.GroupBy(d => d.OrigemColetaId.Trim()).ToDictionary(kvp => kvp.Key, kvp => kvp.ToList());
                    List<string> IdsOrigemColeta = usinas.Keys.Select(x => x.ToString()).ToList();
                    var dadosColeta = ObterDadosDoGabaritoColetaInsumo(coletaInsumo, IdsOrigemColeta);

                    foreach (var usina in usinas)
                    {
                        int qtdRestricaoEmBrancoTela = usina.Value.Where(x => !x.GrandezaNome.Equals("Causa") && string.IsNullOrEmpty(x.Valor)).Count();
                        //SE APAGOU TODAS AS RESTRIÇÕES, NÃO VALIDA A CAUSA
                        if (qtdRestricaoEmBrancoTela != 0)
                        {
                            int qtdRestricaoBanco = dadosColeta.DadosColetaInsumoPaginado.List.Where(x => !x.GrandezaNome.Equals("Causa") && x.OrigemColetaId == usina.Value.FirstOrDefault().OrigemColetaId).Sum(h => h.ValoresDadoColeta.Where(j => !string.IsNullOrEmpty(j.Valor)).Count());
                            if (qtdRestricaoEmBrancoTela == qtdRestricaoBanco)
                                break;
                        }

                        bool existeCausaTela = usina.Value.Any(x => x.GrandezaNome.Equals("Causa") && !string.IsNullOrEmpty(x.Valor));
                        bool existeCausaEmBrancoTela = usina.Value.Any(x => x.GrandezaNome.Equals("Causa") && string.IsNullOrEmpty(x.Valor));
                        bool existeCausaBanco = dadosColeta.DadosColetaInsumoPaginado.List.Any(x => x.GrandezaNome.Equals("Causa") && x.OrigemColetaId.Trim() == usina.Value.FirstOrDefault().OrigemColetaId.Trim() && x.ValoresDadoColeta.Any(y => !string.IsNullOrEmpty(y.Valor)));
                        bool existeRestricaoTela = usina.Value.Any(x => !x.GrandezaNome.Equals("Causa") && !string.IsNullOrEmpty(x.Valor));
                        bool existeRestricaoBanco = dadosColeta.DadosColetaInsumoPaginado.List.Any(x => !x.GrandezaNome.Equals("Causa") && x.OrigemColetaId.Trim() == usina.Value.FirstOrDefault().OrigemColetaId.Trim() && x.ValoresDadoColeta.Any(y => !string.IsNullOrEmpty(y.Valor)));

                        if ((!existeCausaTela && !existeCausaBanco || existeCausaEmBrancoTela) && (existeRestricaoTela || existeRestricaoBanco))
                            mensagens.Add(string.Format("A grandeza Causa da restrição da {0} deve ser preenchida para salvar/enviar esse insumo.", usina.Value.FirstOrDefault().OrigemColetaNome));
                    }
                }
            }

            VerificarONSBusinessException(mensagens);
        }


        // <summary>
        /// Verifica se o insumo cronograma de manutenção de térmica foi salvo anteriormente e está com status "Informado";        
        /// codigo puxado da branche sprint18_Web-PMO_Bug-76601
        /// </summary>
        /// <param name="coletaInsumoId">Id do Coleta de insumo a ser validada</param>        
        private void ValidarInsumoManutencaoTermica(int idSemanaOperativa, IList<int> idColetaInsumos, IList<string> mensagens)
        {
            //TipoDadoColeta = char.ToString((char)TipoDadoColetaEnum.Manutencao)
            //SituacaoId = (int)SituacaoColetaInsumoEnum.Aprovado

            DadoColetaManutencao dadoColetaManutencao = new DadoColetaManutencao();
            foreach (int idColetaInsumo in idColetaInsumos)
            {
                ColetaInsumo coletaInsumo = coletaInsumoRepository.FindByKey(idColetaInsumo);

                if (true == ValidarInsumoDisponibilidade(coletaInsumo.Insumo.Id))
                {
                    PesquisaMonitorarColetaInsumoFilter filter = new PesquisaMonitorarColetaInsumoFilter();
                    List<int> idsInsumo = new List<int>();
                    idsInsumo.Add(116); // Cronograma de Manutencoes Termicas
                    List<int> idsAgentes = new List<int>();
                    idsAgentes.Add(coletaInsumo.AgenteId);
                    filter.IdsInsumo = idsInsumo;
                    filter.IdsAgentes = idsAgentes;
                    filter.IdSemanaOperativa = idSemanaOperativa;
                    filter.PageIndex = 1;
                    filter.PageSize = 5;

                    var resultadoPaginado = ConsultarColetasInsumoParaInformarDadosPaginado(filter);
                    if (null != resultadoPaginado
                         && null != resultadoPaginado.List
                         && resultadoPaginado.List.Count > 0)
                    {

                        var collll = ObterColetaInsumoInformarDadosPorChave(resultadoPaginado.List[0].Id);
                        if (collll == null
                              || collll.Situacao.Id != (int)SituacaoColetaInsumoEnum.Informado)
                        {
                            mensagens.Add(SGIPMOMessages.MS084);
                            break;
                        }

                    }
                    else
                    {
                        mensagens.Add(SGIPMOMessages.MS084);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Verifica se o insumo é do tipo "Disponibilidade";        
        /// </summary>
        /// <param name="InsumoId">Id do insumo a ser validado</param>        
        private bool ValidarInsumoDisponibilidade(int id)
        {
            foreach (var tipoInsumoDisponibilidade in Enum.GetValues(typeof(TipoInsumoDisponibilidadeEnum)))
            {
                if (id == (int)tipoInsumoDisponibilidade) return true;
            }
            return false;
        }

        /// <summary>
        /// Verifica se a Inflexibilidade está maior do que a Disponibilidade por usina, semana e patamar;        
        /// </summary>
        /// <param name="coletaInsumo">Coleta de insumo a ser validada</param>
        /// <param name="dtos">Dados de coleta que seram usados para validar o preenchimento da Inflexibilidade e Disponibilidade.</param>
        private void ValidarPreenchimentoDaInflexibilidadeDisponibilidade(ColetaInsumo coletaInsumo, IList<ValorDadoColetaDTO> dtos)
        {
            IList<string> mensagens = new List<string>();
            string[] idsInsumosRegraInflexibilidadeDisponibilidade = ConfigurationManager.AppSettings["InsumosRegraInflexibilidadeDisponibilidade"].Split(';');

            if (dtos.Any() && idsInsumosRegraInflexibilidadeDisponibilidade != null)
            {
                bool isVerificaInsumo = idsInsumosRegraInflexibilidadeDisponibilidade.Contains(coletaInsumo.InsumoId.ToString());
                //O insumo precisa ser validado
                if (isVerificaInsumo)
                {
                    string[] idsUsinasForaDaRegraInflexibilidadeDisponibilidade = ConfigurationManager.AppSettings["UsinasForaDaRegraInflexibilidadeDisponibilidade"].Split(';');
                    var usinas = dtos.GroupBy(d => d.OrigemColetaId.Trim()).ToDictionary(kvp => kvp.Key, kvp => kvp.ToList());
                    List<string> IdsOrigemColeta = usinas.Keys.Select(x => x.ToString()).ToList();
                    var dadosColeta = ObterDadosDoGabaritoColetaInsumo(coletaInsumo, IdsOrigemColeta);
                    //Percorre as usinas
                    foreach (var usina in usinas)
                    {
                        var origemColetaId = usina.Value.FirstOrDefault().OrigemColetaId.Trim();
                        //Verifica se a usina faz parte da regra
                        if (!idsUsinasForaDaRegraInflexibilidadeDisponibilidade.Contains(origemColetaId))
                        {
                            List<string> preferences = new List<string>();
                            preferences.AddRange(usina.Value.Select(x => x.Estagio).Where(x => x.Contains("S")).OrderBy(x => x));
                            preferences.AddRange(usina.Value.Select(x => x.Estagio).Where(x => x.Contains("M")).OrderBy(x => x));
                            //Separa as semanas da usina
                            var semanas = usina.Value.GroupBy(d => d.Estagio).ToDictionary(kvp => kvp.Key, kvp => kvp.ToList()).OrderBy(x => preferences.IndexOf(x.Key));

                            foreach (var semana in semanas)
                            {
                                //Separa os patamares da semana Pesada, Média e Leve
                                var patamar = semana.Value.GroupBy(d => d.TipoPatamarId).ToDictionary(kvp => kvp.Key, kvp => kvp.ToList());
                                foreach (var item in patamar)
                                {
                                    double Disponibilidade = 0, Inflexibilidade = 0;
                                    bool valido = true;

                                    //Pega os valores por patamar
                                    var DisponibilidadeTela = item.Value.Where(x => x.GrandezaNome.Contains("Disponibilidade")).FirstOrDefault();
                                    var InflexibilidadeTela = item.Value.Where(x => x.GrandezaNome.Contains("Inflexibilidade")).FirstOrDefault();

                                    //Apagou a inflexibilidade e disponibilidade na tela, não precisa comparar os valores.
                                    if (DisponibilidadeTela != null && string.IsNullOrEmpty(DisponibilidadeTela.Valor) && InflexibilidadeTela != null && string.IsNullOrEmpty(InflexibilidadeTela.Valor))
                                        continue;

                                    //Caso o Valor não seja informado na tela, será necessário buscar na base, tanto a Disponibilidade quanto a Inflexibilidade
                                    if (DisponibilidadeTela != null && !string.IsNullOrEmpty(DisponibilidadeTela.Valor))
                                        Disponibilidade = double.Parse(DisponibilidadeTela.Valor);
                                    else
                                        Disponibilidade = BuscarDisponibilidadeOuInflexibilidade("Disponibilidade", dadosColeta, item.Value.FirstOrDefault().TipoPatamarId, semana.Value.FirstOrDefault().Estagio, origemColetaId);
                                    if (InflexibilidadeTela != null && !string.IsNullOrEmpty(InflexibilidadeTela.Valor))
                                        Inflexibilidade = double.Parse(InflexibilidadeTela.Valor);
                                    else
                                        Inflexibilidade = BuscarDisponibilidadeOuInflexibilidade("Inflexibilidade", dadosColeta, item.Value.FirstOrDefault().TipoPatamarId, semana.Value.FirstOrDefault().Estagio, origemColetaId);

                                    //Informou inflexibilidade e disponibilidade e inflexibilidade é maior que disponibilidade 
                                    if (!Inflexibilidade.Equals(double.NaN) && !Disponibilidade.Equals(double.NaN) && Inflexibilidade > Disponibilidade)
                                        valido = false;

                                    //Informou inflexibilidade e não informou disponibilidade e não possui disponibilidade na base
                                    if (InflexibilidadeTela != null && DisponibilidadeTela == null && Disponibilidade.Equals(double.NaN))
                                        valido = false;

                                    //Apagou a disponibilidade na tela e tem inflexibilidade  
                                    if (DisponibilidadeTela != null && string.IsNullOrEmpty(DisponibilidadeTela.Valor) && (!Inflexibilidade.Equals(double.NaN) || InflexibilidadeTela != null && !string.IsNullOrEmpty(InflexibilidadeTela.Valor)))
                                    {
                                        valido = false;
                                        Disponibilidade = double.NaN;
                                    }

                                    if (!valido)
                                    {
                                        mensagens.Add(string.Format("{0}: No patamar de carga {1} no estágio {2} a inflexibilidade ({3}) está maior do que a disponibilidade ({4}). Não é possível salvar/enviar.",
                                            usina.Value.FirstOrDefault().OrigemColetaNome,
                                            item.Value.FirstOrDefault().TipoPatamarId.Equals(1) ? "Pesada" :
                                            item.Value.FirstOrDefault().TipoPatamarId.Equals(2) ? "Média" : "Leve", item.Value.FirstOrDefault().Estagio, Inflexibilidade.Equals(double.NaN) ? "Vazio" : Inflexibilidade.ToString(), Disponibilidade.Equals(double.NaN) ? "Vazio" : Disponibilidade.ToString()));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            VerificarONSBusinessException(mensagens);
        }

        /// <summary>
        /// Verifica se a Inflexibilidade informada pelo Agente Eletrobras está maior do que a Disponibilidade informada pelos agentes Copel, Tractebel e Cgtee. por usina, semana e patamar;        
        /// </summary>
        /// <param name="coletaInsumo">Coleta de insumo a ser validada</param>
        /// <param name="dtos">Dados de coleta que seram usados para validar o preenchimento da Inflexibilidade e Disponibilidade.</param>
        private void ValidarPreenchimentoDaInflexibilidadeDisponibilidadeEletrobras(ColetaInsumo coletaInsumo, IList<ValorDadoColetaDTO> dtos)
        {
            IList<string> mensagens = new List<string>();
            string[] idsInsumosRegraInflexibilidadeDisponibilidadeEletrobras = ConfigurationManager.AppSettings["InsumosRegraInflexibilidadeDisponibilidadeEletrobras"].Split(';');
            string[] idsAgentesRegraInflexibilidadeDisponibilidadeEletrobras = ConfigurationManager.AppSettings["IdsAgentesRegraInflexibilidadeDisponibilidadeEletrobras"].Split(';');
            int idEletrobrasRegraInflexibilidadeDisponibilidadeEletrobras = int.Parse(ConfigurationManager.AppSettings["IdEletrobrasRegraInflexibilidadeDisponibilidadeEletrobras"].ToString());

            if (dtos.Any() && idsInsumosRegraInflexibilidadeDisponibilidadeEletrobras != null && (coletaInsumo.AgenteId == idEletrobrasRegraInflexibilidadeDisponibilidadeEletrobras || idsAgentesRegraInflexibilidadeDisponibilidadeEletrobras.Contains(coletaInsumo.AgenteId.ToString())))
            {
                bool isVerificaInsumo = idsInsumosRegraInflexibilidadeDisponibilidadeEletrobras.Contains(coletaInsumo.InsumoId.ToString());
                //O insumo precisa ser validado
                if (isVerificaInsumo)
                {
                    string[] idsUsinasForaDaRegraInflexibilidadeDisponibilidade = ConfigurationManager.AppSettings["UsinasForaDaRegraInflexibilidadeDisponibilidade"].Split(';');
                    var usinas = dtos.GroupBy(d => d.OrigemColetaId.Trim()).ToDictionary(kvp => kvp.Key, kvp => kvp.ToList());
                    //Percorre as usinas
                    foreach (var usina in usinas)
                    {
                        var origemColetaId = usina.Value.FirstOrDefault().OrigemColetaId.Trim();
                        //Verifica se a usina faz parte da regra
                        if (idsUsinasForaDaRegraInflexibilidadeDisponibilidade.Contains(origemColetaId))
                        {
                            string Agente = string.Empty;
                            List<string> IdsOrigemColeta = new List<string> { usina.Key };
                            bool isEletrobras = coletaInsumo.AgenteId == idEletrobrasRegraInflexibilidadeDisponibilidadeEletrobras;
                            DadosInformarColetaInsumoDTO dadosColeta = new DadosInformarColetaInsumoDTO();
                            //Se for a eletrobras, os dados da tela são de inflexibilidade, senão de disponibilidade
                            if (isEletrobras)
                            {
                                dadosColeta = BuscarDisponibilidadeOuInflexibilidadeRegraEletrobras(idsAgentesRegraInflexibilidadeDisponibilidadeEletrobras.Select(x => int.Parse(x)).ToList(),
                                                                                                    idsInsumosRegraInflexibilidadeDisponibilidadeEletrobras.Select(x => int.Parse(x)).Where(x => x != coletaInsumo.InsumoId).ToList(),
                                                                                                    coletaInsumo,
                                                                                                    IdsOrigemColeta,
                                                                                                    ref Agente);
                            }
                            else
                            {
                                dadosColeta = BuscarDisponibilidadeOuInflexibilidadeRegraEletrobras(new List<int> { idEletrobrasRegraInflexibilidadeDisponibilidadeEletrobras },
                                                                                                    idsInsumosRegraInflexibilidadeDisponibilidadeEletrobras.Select(x => int.Parse(x)).Where(x => x != coletaInsumo.InsumoId).ToList(),
                                                                                                    coletaInsumo,
                                                                                                    IdsOrigemColeta,
                                                                                                   ref Agente);
                            }
                            if (dadosColeta != null)
                            {
                                List<string> preferences = new List<string>();
                                preferences.AddRange(usina.Value.Select(x => x.Estagio).Where(x => x.Contains("S")).OrderBy(x => x));
                                preferences.AddRange(usina.Value.Select(x => x.Estagio).Where(x => x.Contains("M")).OrderBy(x => x));
                                //Separa as semanas da usina
                                var semanas = usina.Value.GroupBy(d => d.Estagio).ToDictionary(kvp => kvp.Key, kvp => kvp.ToList()).OrderBy(x => preferences.IndexOf(x.Key));

                                foreach (var semana in semanas)
                                {
                                    //Separa os patamares da semana Pesada, Média e Leve
                                    var patamar = semana.Value.GroupBy(d => d.TipoPatamarId).ToDictionary(kvp => kvp.Key, kvp => kvp.ToList());
                                    foreach (var item in patamar)
                                    {
                                        double Disponibilidade = 0, Inflexibilidade = 0;
                                        ValorDadoColetaDTO DisponibilidadeTela = null, InflexibilidadeTela = null;
                                        //Pega os valores por patamar
                                        if (isEletrobras)
                                            InflexibilidadeTela = item.Value.Where(x => x.GrandezaNome.Contains("Inflexibilidade") && !string.IsNullOrEmpty(x.Valor)).FirstOrDefault();
                                        else
                                            DisponibilidadeTela = item.Value.Where(x => x.GrandezaNome.Contains("Disponibilidade") && !string.IsNullOrEmpty(x.Valor)).FirstOrDefault();

                                        if (DisponibilidadeTela != null)
                                            Disponibilidade = double.Parse(DisponibilidadeTela.Valor);
                                        else
                                            Disponibilidade = BuscarDisponibilidadeOuInflexibilidade("Disponibilidade", dadosColeta, item.Value.FirstOrDefault().TipoPatamarId, semana.Value.FirstOrDefault().Estagio, origemColetaId);
                                        if (InflexibilidadeTela != null)
                                            Inflexibilidade = double.Parse(InflexibilidadeTela.Valor);
                                        else
                                            Inflexibilidade = BuscarDisponibilidadeOuInflexibilidade("Inflexibilidade", dadosColeta, item.Value.FirstOrDefault().TipoPatamarId, semana.Value.FirstOrDefault().Estagio, origemColetaId);
                                        if (Inflexibilidade != double.NaN && Disponibilidade != double.NaN && Inflexibilidade > Disponibilidade)
                                        {
                                            mensagens.Add(string.Format("{0}: No patamar de carga {1} no estágio {2} a inflexibilidade ({3}) declarada pela ELETROBRAS está maior do que a disponibilidade ({4}) declarada pela {5}. Não é possível salvar/enviar.",
                                                usina.Value.FirstOrDefault().OrigemColetaNome,
                                                item.Value.FirstOrDefault().TipoPatamarId.Equals(1) ? "Pesada" :
                                                item.Value.FirstOrDefault().TipoPatamarId.Equals(2) ? "Média" : "Leve", item.Value.FirstOrDefault().Estagio, Inflexibilidade, Disponibilidade, isEletrobras ? Agente : coletaInsumo.Agente.Nome));

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            VerificarONSBusinessException(mensagens);
        }

        private DadosInformarColetaInsumoDTO BuscarDisponibilidadeOuInflexibilidadeRegraEletrobras(List<int> idAgente, List<int> idInsumo, ColetaInsumo coletaInsumoAtual, List<string> IdsOrigemColeta, ref string Agente)
        {
            PesquisaMonitorarColetaInsumoFilter filter = new PesquisaMonitorarColetaInsumoFilter
            {
                IdsAgentes = idAgente,
                IdsInsumo = idInsumo,
                IdSemanaOperativa = coletaInsumoAtual.SemanaOperativaId,
                PageIndex = 1,
                PageSize = int.MaxValue
            };


            //Busca os insumos
            var resultadoPaginado = ConsultarColetasInsumoParaInformarDadosPaginado(filter);

            if (resultadoPaginado.List.Count != 0)
            {
                //Se estiver buscando os dados da Eletrobras, sempre vai encontrar apenas um resultado, caso contrário, encontrará os 3 Agentes
                foreach (var item in resultadoPaginado.List)
                {

                    ColetaInsumo coletaInsumo = coletaInsumoRepository.FindByKeyConcurrencyValidate(
                        item.Id, item.Versao);

                    var dadosColeta = ObterDadosDoGabaritoColetaInsumo(coletaInsumo, IdsOrigemColeta);
                    //Sempre vai buscar a usina em questão nos 3 agentes, porém, só deve encontrar em 1 agente.
                    if (dadosColeta.DadosColetaInsumoPaginado.List.Count != 0)
                    {
                        Agente = coletaInsumo.Agente.Nome;
                        return dadosColeta;
                    }
                }
            }
            return null;
        }
        private double BuscarDisponibilidadeOuInflexibilidade(string tipo, DadosInformarColetaInsumoDTO dadosColeta, int TipoPatamarId,
            string Estagio, string origemColetaId)
        {
            var patamar = dadosColeta.DadosColetaInsumoPaginado.List.Where(x => x.OrigemColetaId.Trim().Equals(origemColetaId.Trim()) && x.TipoPatamarId == TipoPatamarId && x.GrandezaNome.Contains(tipo)).GroupBy(d => d.TipoPatamarId).ToDictionary(kvp => kvp.Key, kvp => kvp.ToList()).FirstOrDefault();
            if (patamar.Value != null)
            {
                var semana = patamar.Value.Where(x => x.GrandezaNome.Contains(tipo)).FirstOrDefault().ValoresDadoColeta.Where(x => x.Estagio == Estagio && !string.IsNullOrEmpty(x.Valor)).FirstOrDefault();
                if (semana != null)
                    return double.Parse(semana.Valor);
                else
                    return double.NaN;
            }
            else
                return double.NaN;
        }


        private void ValidarDatasManutencao(DadoColetaManutencao dadoColeta, IList<string> mensagens)
        {
            if (dadoColeta != null)
            {
                if (dadoColeta.DataFim < dadoColeta.DataInicio)
                {
                    mensagens.Add("Data de término não pode ser inferior à data de início.");
                }
            }
        }

        private void ValidarTipoDadoColetado(IList<ValorDadoColetaDTO> dtos)
        {
            IList<string> mensagens = new List<string>();
            var obrigatorio = false;
            foreach (var dto in dtos)
            {
                obrigatorio = false;
                if (IsValorInvalido(dto, out obrigatorio))
                {
                    var campos = new List<string>();

                    if (dto.TipoPatamarId != default(int))
                    {
                        campos.Add(string.Format("patamar de carga \"{0}\"", ((TipoPatamarEnum)dto.TipoPatamarId).ToDescription()));
                    }

                    if (dto.TipoLimiteId != default(int))
                    {
                        campos.Add(string.Format("limite \"{0}\"", ((TipoLimiteEnum)dto.TipoLimiteId).ToDescription()));
                    }

                    campos.Add(string.Format("estágio \"{0}\"", dto.Estagio));
                    string usina = string.Format(" da Usina \"{0}\"", dto.OrigemColetaNome);
                    string campo = string.Format("{0} ({1}) {2}", dto.GrandezaNome, string.Join(", ", campos), usina);
                    string mensagem = string.Format(!obrigatorio ? SGIPMOMessages.MS005 : SGIPMOMessages.MS075, campo);

                    mensagens.Add(mensagem);
                }
            }


            VerificarONSBusinessException(mensagens);
        }

        /// <summary>
        /// Recebe um valor e verifica se existem pontos trocados por vírgula e faz a inversão, caso necessário
        ///     - Por ex: Caso o usuário tenha submetido um valor 3,566,656.66 (Padrão Americano), este valor será trocado para 3.566.656,66;
        ///     - Caso haja mais de uma vírgula no valor, estas já serão substituídas por ponto;
        ///     - O padrão correto deve ser 3 casas a cada ponto;
        /// </summary>
        /// <param name="valorOriginal"></param>
        /// <param name="valorTratado"></param>
        /// <returns></returns>
        private bool TrataPontoVirgulaEmDecimal(string valorOriginal, out decimal valorTratado)
        {
            valorTratado = 0;
            return false;
        }

        private bool IsValorInvalido(ValorDadoColetaDTO dto, out bool obrigatorio)
        {
            bool isValorInvalido = false;
            obrigatorio = false;
            if (dto != null)
            {
                switch (dto.TipoDadoGrandeza)
                {
                    case TipoDadoGrandezaEnum.Numerico:
                        if (!string.IsNullOrWhiteSpace(dto.Valor))
                        {
                            decimal valor;
                            isValorInvalido = !decimal.TryParse(dto.Valor, out valor);

                            if (!isValorInvalido)
                            {
                                // Tratando permissão de valores negativos
                                if (!dto.AceitaValorNegativo && valor < 0)
                                {
                                    isValorInvalido = true;
                                }
                                else
                                {
                                    //Tratando valores com somente duas casas decimais depois do ponto
                                    if (dto.Valor.Contains('.'))
                                    {
                                        //remover o conteúdo após a vírgula
                                        //para o conteúdo restante: 
                                        //  - fazer o split por ponto
                                        //  - cada posição do array resultante deve ter menos que 3 caracteres
                                        //  - todas as posições devem ter 3 caracteres, excluindo-se a primeira
                                        var conteudoAntesVirgula = dto.Valor.Split(',')[0];
                                        var secoes = conteudoAntesVirgula.Split('.');

                                        isValorInvalido = secoes.Any(sec => sec.Length > 3); //o valor é inválido caso tenha alguma seção com mais de 3 caracteres

                                        if (!isValorInvalido)
                                        {
                                            isValorInvalido = secoes.Where((val, index) => index > 0 && val.Length != 3).Any(); //todas as posições devem ter 3 caracteres, excluindo-se a primeira
                                        }
                                    }

                                    if (!isValorInvalido)
                                    {
                                        // Tratando quantidade de casas inteiras e decimais
                                        string parteInteira = string.Empty;
                                        string parteDecimal = string.Empty;

                                        decimal valorAbsoluto = valor < 0 ? valor * -1 : valor;
                                        string valorDecimalFormaUnificada = valorAbsoluto.ToString()
                                            .Replace(",", ".");

                                        string[] valorDecimalRepartido =
                                            valorDecimalFormaUnificada.Split(new string[] { "." },
                                                StringSplitOptions.None);

                                        parteInteira = valorDecimalRepartido[0];
                                        if (valorDecimalRepartido.Length > 1)
                                        {
                                            parteDecimal = valorDecimalRepartido[1];
                                        }

                                        isValorInvalido = parteInteira.Length > dto.QuantidadeCasasInteira || parteDecimal.Length > dto.QuantidadeCasasDecimais;
                                    }
                                }
                            }
                        }
                        else
                        {
                            isValorInvalido = dto.IsObrigatorio;
                            obrigatorio = dto.IsObrigatorio;
                        }

                        break;
                    case TipoDadoGrandezaEnum.Texto:

                        if (!string.IsNullOrWhiteSpace(dto.Valor))
                        {
                            isValorInvalido = dto.Valor.Length > 4000;
                        }
                        else
                        {
                            isValorInvalido = dto.IsObrigatorio;
                            obrigatorio = dto.IsObrigatorio;
                        }

                        break;
                    case TipoDadoGrandezaEnum.Data:
                        if (!string.IsNullOrWhiteSpace(dto.Valor))
                        {
                            DateTime data;
                            isValorInvalido = !DateTime.TryParse(dto.Valor, out data);
                        }
                        else
                        {
                            isValorInvalido = dto.IsObrigatorio;
                            obrigatorio = dto.IsObrigatorio;
                        }
                        break;
                }
            }
            return isValorInvalido;
        }

        #endregion

        #region Dado Coleta Manutenção

        public PagedResult<DadoColetaManutencaoDTO> ConsultarDadoColetaManutencaoPorColetaInsumoPaginado(
            DadoColetaInsumoFilter filter)
        {
            return dadoColetaManutencaoService.ConsultarDadoColetaManutencaoPorColetaInsumo(filter);
        }

        public void IncluirDadoColetaManutencao(InclusaoDadoColetaManutencaoDTO dto)
        {
            var coletaInsumo = coletaInsumoRepository
                .FindByKeyConcurrencyValidate(dto.IdColetaInsumo, dto.VersaoColetaInsumo);

            var dadoColeta = new DadoColetaManutencao();
            dadoColeta.DataInicio = dto.DataInicio;
            dadoColeta.DataFim = dto.DataFim;
            dadoColeta.TempoRetorno = dto.TempoRetorno;
            dadoColeta.Justificativa = dto.Justificativa;
            //dadoColeta.Periodicidade = dto.Periodicidade;
            ValidarPermiteManutencaoColetaInsumo(coletaInsumo, dto.IsMonitorar, dadoColeta);

            coletaInsumo.SemanaOperativa.DataHoraAtualizacao = DateTime.Now;

            Gabarito gabarito = gabaritoRepository.ObterPorColetaInumoOrigemColeta(
                dto.IdColetaInsumo, dto.IdUnidadeGeradora);

            if (coletaInsumo.Situacao.Id == (int)SituacaoColetaInsumoEnum.NaoIniciado ||
                coletaInsumo.Situacao.Id == (int)SituacaoColetaInsumoEnum.Rejeitado)
            {
                coletaInsumo.Situacao = situacaoColetaInsumoRepository
                    .FindByKey((int)SituacaoColetaInsumoEnum.EmAndamento);

                historicoService.CriarSalvarHistoricoColetaInsumo(coletaInsumo);
            }

            dadoColeta.Gabarito = gabarito;
            dadoColeta.UnidadeGeradora = gabarito.OrigemColeta as UnidadeGeradora;
            dadoColeta.ColetaInsumo = coletaInsumo;
            dadoColeta.TipoDadoColeta = char.ToString((char)TipoDadoColetaEnum.Manutencao);

            dadoColetaManutencaoService.IncluirDadoColeta(dadoColeta);
        }

        public void ExcluirDadoColetaManutencao(ExclusaoDadoColetaManutencaoDTO dto, int idColetaInsumo)
        {

            if (0 < idColetaInsumo)
            {
                DadoColetaInsumoFilter filt = new DadoColetaInsumoFilter();
                filt.IdColetaInsumo = idColetaInsumo;
                filt.PageIndex = 1;
                filt.PageSize = int.MaxValue;

                PagedResult<DadoColetaManutencaoDTO> lstParaApgar = dadoColetaManutencaoRepository.ConsultarPorColetaInsumo(filt);

                foreach (DadoColetaManutencaoDTO paraApagar in lstParaApgar.List)
                {
                    DadoColetaManutencao dadoColeta = dadoColetaManutencaoService.ObterPorChave(paraApagar.IdDadoColeta);

                    coletaInsumoRepository.ValidateConcurrency(dadoColeta.ColetaInsumo, dto.VersaoColetaInsumo);
                    ValidarPermiteManutencaoColetaInsumo(dadoColeta.ColetaInsumo, dto.IsMonitorar, null);

                    ColetaInsumo coletaInsumo = coletaInsumoRepository.FindByKeyConcurrencyValidate(
                        dadoColeta.ColetaInsumo.Id, dto.VersaoColetaInsumo);

                    coletaInsumo.SemanaOperativa.DataHoraAtualizacao = DateTime.Now;
                    if (coletaInsumo.Situacao.Id == (int)SituacaoColetaInsumoEnum.Rejeitado)
                    {
                        coletaInsumo.Situacao = situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.EmAndamento);
                    }
                    dadoColetaManutencaoService.Excluir(dadoColeta);
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(dto.ListaIdsDadoColeta) || string.IsNullOrEmpty(dto.ListaIdsDadoColeta))
                {
                    dto.ListaIdsDadoColeta = Convert.ToString(dto.IdDadoColeta);
                }

                string[] idsDadoColeta = dto.ListaIdsDadoColeta.Split(',');
                foreach (string idDadoColeta in idsDadoColeta)
                {
                    DadoColetaManutencao dadoColeta = dadoColetaManutencaoService.ObterPorChave(Convert.ToInt32(idDadoColeta));

                    coletaInsumoRepository.ValidateConcurrency(dadoColeta.ColetaInsumo, dto.VersaoColetaInsumo);
                    ValidarPermiteManutencaoColetaInsumo(dadoColeta.ColetaInsumo, dto.IsMonitorar, null);

                    ColetaInsumo coletaInsumo = coletaInsumoRepository.FindByKeyConcurrencyValidate(
                        dadoColeta.ColetaInsumo.Id, dto.VersaoColetaInsumo);

                    coletaInsumo.SemanaOperativa.DataHoraAtualizacao = DateTime.Now;
                    if (coletaInsumo.Situacao.Id == (int)SituacaoColetaInsumoEnum.Rejeitado)
                    {
                        coletaInsumo.Situacao = situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.EmAndamento);
                    }
                    dadoColetaManutencaoService.Excluir(dadoColeta);
                }

            }
        }

        public void AlterarDadoColetaManutencao(AlteracaoDadoColetaManutencaoDTO dto)
        {
            DadoColetaManutencao dadoColeta = dadoColetaManutencaoService.ObterPorChave(dto.IdDadoColeta);
            dadoColeta.Justificativa = dto.Justificativa;
            dadoColeta.TempoRetorno = dto.TempoRetorno;
            dadoColeta.DataInicio = dto.DataInicio; //.Date + dadoColeta.DataInicio.TimeOfDay;
            dadoColeta.DataFim = dto.DataFim; //.Date + dadoColeta.DataFim.TimeOfDay;

            ColetaInsumo coletaInsumo = coletaInsumoRepository.FindByKeyConcurrencyValidate(
                dadoColeta.ColetaInsumo.Id, dto.VersaoColetaInsumo);

            ValidarPermiteManutencaoColetaInsumo(coletaInsumo, dto.IsMonitorar, dadoColeta);

            coletaInsumo.SemanaOperativa.DataHoraAtualizacao = DateTime.Now;

            if (coletaInsumo.Situacao.Id == (int)SituacaoColetaInsumoEnum.Rejeitado)
            {
                coletaInsumo.Situacao = situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.EmAndamento);
            }

            dadoColetaManutencaoService.AlterarDadoColeta(dadoColeta);
        }

        public void IncluirDadoColetaManutencaoImportacao(IList<InclusaoDadoColetaManutencaoDTO> dtos) //aqui nao tem periodicidade
        {
            if (dtos.Any())
            {
                IList<DadoColetaManutencao> dadoColetaList = new List<DadoColetaManutencao>();

                ColetaInsumo coletaInsumo = coletaInsumoRepository
                    .FindByKeyConcurrencyValidate(
                        dtos.First().IdColetaInsumo,
                        dtos.First().VersaoColetaInsumo);

                ValidarPermiteManutencaoColetaInsumo(coletaInsumo, dtos.First().IsMonitorar, null);

                if (coletaInsumo.Situacao.Id == (int)SituacaoColetaInsumoEnum.NaoIniciado ||
                    coletaInsumo.Situacao.Id == (int)SituacaoColetaInsumoEnum.Rejeitado)
                {
                    coletaInsumo.Situacao =
                        situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.EmAndamento);
                }

                coletaInsumo.SemanaOperativa.DataHoraAtualizacao = DateTime.Now;

                foreach (InclusaoDadoColetaManutencaoDTO dto in dtos)
                {

                    if (dto.EhDiaria)
                    {
                        var dataInicio = dto.DataInicio;
                        while (dataInicio.CompareTo(dto.DataFim) <= 0)
                        {
                            DadoColetaManutencao dadoColeta = new DadoColetaManutencao();
                            dadoColeta.DataInicio = dataInicio;
                            dadoColeta.DataFim = new DateTime(dataInicio.Year, dataInicio.Month, dataInicio.Day, dto.DataFim.Hour, dto.DataFim.Minute, dto.DataFim.Second);
                            dadoColeta.TempoRetorno = dto.TempoRetorno;
                            dadoColeta.Justificativa = string.IsNullOrEmpty(dto.Justificativa) ? " " : dto.Justificativa;
                            dadoColeta.Numero = dto.Numero;
                            dadoColeta.Situacao = dto.Situacao;
                            dadoColeta.ClassificacaoPorTipoEquipamento = dto.ClassificacaoPorTipoEquipamento;
                            dadoColeta.Periodicidade = dto.Periodicidade;
                            Gabarito gabarito = gabaritoRepository.ObterPorColetaInumoOrigemColeta(
                                dto.IdColetaInsumo, dto.IdUnidadeGeradora);

                            dadoColeta.Gabarito = gabarito;
                            dadoColeta.UnidadeGeradora = gabarito.OrigemColeta as UnidadeGeradora;
                            dadoColeta.ColetaInsumo = coletaInsumo;
                            dadoColeta.TipoDadoColeta = char.ToString((char)TipoDadoColetaEnum.Manutencao);

                            dadoColetaList.Add(dadoColeta);

                            dataInicio = dataInicio.AddDays(1);
                        }

                    }
                    else
                    {

                        DadoColetaManutencao dadoColeta = new DadoColetaManutencao();
                        dadoColeta.DataInicio = dto.DataInicio;
                        dadoColeta.DataFim = dto.DataFim;
                        dadoColeta.TempoRetorno = dto.TempoRetorno;
                        dadoColeta.Justificativa = string.IsNullOrEmpty(dto.Justificativa) ? " " : dto.Justificativa;
                        dadoColeta.Numero = dto.Numero;
                        dadoColeta.Situacao = dto.Situacao;
                        dadoColeta.ClassificacaoPorTipoEquipamento = dto.ClassificacaoPorTipoEquipamento;

                        Gabarito gabarito = gabaritoRepository.ObterPorColetaInumoOrigemColeta(
                            dto.IdColetaInsumo, dto.IdUnidadeGeradora);

                        dadoColeta.Gabarito = gabarito;
                        dadoColeta.UnidadeGeradora = gabarito.OrigemColeta as UnidadeGeradora;
                        dadoColeta.ColetaInsumo = coletaInsumo;
                        dadoColeta.TipoDadoColeta = char.ToString((char)TipoDadoColetaEnum.Manutencao);
                        dadoColeta.Periodicidade = dto.Periodicidade;
                        dadoColetaList.Add(dadoColeta);
                    }
                }

                dadoColetaManutencaoService.IncluirDadoColetaSeNaoExiste(dadoColetaList);
            }
        }

        public void SalvarColetaDadosManutencao(ColetaInsumoManutencaoFilter filter)
        {
            ColetaInsumo coletaInsumo = coletaInsumoRepository.FindByKeyConcurrencyValidate(
                filter.IdColetaInsumo, filter.Versao);

            ValidarPermiteManutencaoColetaInsumo(coletaInsumo, true, null);

            coletaInsumo.SemanaOperativa.DataHoraAtualizacao = DateTime.Now;

            coletaInsumo.MotivoAlteracaoONS = filter.MotivoAlteracaoONS;
            coletaInsumo.MotivoRejeicaoONS = filter.MotivoRejeicaoONS;
            coletaInsumo.DataHoraAtualizacao = DateTime.Now;
            coletaInsumo.LoginAgenteAlteracao = UserInfo.UserName;
        }

        public void AprovarColetaDadosManutencao(ColetaInsumoManutencaoFilter filter)
        {
            ColetaInsumo coletaInsumo = coletaInsumoRepository.FindByKeyConcurrencyValidate(
                filter.IdColetaInsumo, filter.Versao);
            coletaInsumo.MotivoAlteracaoONS = filter.MotivoAlteracaoONS;
            coletaInsumo.MotivoRejeicaoONS = filter.MotivoRejeicaoONS;
            coletaInsumo.DataHoraAtualizacao = DateTime.Now;
            coletaInsumo.LoginAgenteAlteracao = UserInfo.UserName;

            coletaInsumo.SemanaOperativa.DataHoraAtualizacao = DateTime.Now;

            coletaInsumo.Situacao = situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.Aprovado);

            historicoService.CriarSalvarHistoricoColetaInsumo(coletaInsumo);
        }

        public void RejeitarColetaDadosManutencao(ColetaInsumoManutencaoFilter filter)
        {
            ColetaInsumo coletaInsumo = coletaInsumoRepository.FindByKeyConcurrencyValidate(
                filter.IdColetaInsumo, filter.Versao);

            IList<string> mensagens = new List<string>();
            ValidarMotivoRejeicaoColeta(filter.MotivoRejeicaoONS, mensagens);
            ValidarSituacaoMonitorarDados(coletaInsumo, mensagens);
            VerificarONSBusinessException(mensagens);

            coletaInsumo.MotivoAlteracaoONS = filter.MotivoAlteracaoONS;
            coletaInsumo.MotivoRejeicaoONS = filter.MotivoRejeicaoONS;
            coletaInsumo.DataHoraAtualizacao = DateTime.Now;
            coletaInsumo.LoginAgenteAlteracao = UserInfo.UserName;

            coletaInsumo.SemanaOperativa.DataHoraAtualizacao = DateTime.Now;

            coletaInsumo.Situacao = situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.Rejeitado);

            historicoService.CriarSalvarHistoricoColetaInsumo(coletaInsumo);

            Parametro parametro = parametroService.ObterParametro(ParametroEnum.MensagemRejeicaoColeta);

            string assunto = string.Format("[ONS-WEBPMO] Rejeição da coleta do insumo {0} do agente {1}",
                coletaInsumo.Insumo.Nome, coletaInsumo.Agente.Nome);

            List<int> idAgentes = new List<int> { coletaInsumo.AgenteId };
            notificacaoService.NotificarUsuariosPorAgente(coletaInsumo.Agente.Id, assunto, parametro.Valor);
            List<Agente> agentes = new List<Agente>();
            agentes.Add(coletaInsumo.Agente);
            logNotificacaoService.LogarNotificacao(coletaInsumo.SemanaOperativa, agentes,
                coletaInsumo.SemanaOperativa.DataHoraAtualizacao, UserInfo.UserName, LogNotificacaoService.LOG_NOTIFICACAO_REJEICAO);
        }

        public void AprovarColetaDadosNaoEstruturados(DadosMonitoramentoColetaInsumoDTO dto, DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dtoDados)
        {
            ColetaInsumo coletaInsumo = null;
            if (dto.VersaoColetaInsumo != null)
            {
                coletaInsumo = coletaInsumoRepository.FindByKeyConcurrencyValidate(dto.IdColetaInsumo, dto.VersaoColetaInsumo);
            }
            else
            {
                coletaInsumo = coletaInsumoRepository.FindByKey(dto.IdColetaInsumo);
            }
            if (coletaInsumo != null)
            {
                IList<string> mensagens = new List<string>();
                ValidarSituacaoMonitorarDados(coletaInsumo, mensagens);
                VerificarONSBusinessException(mensagens);

                coletaInsumo.MotivoAlteracaoONS = dto.MotivoAlteracaoONS;
                coletaInsumo.MotivoRejeicaoONS = dto.MotivoRejeicaoONS;
                coletaInsumo.LoginAgenteAlteracao = UserInfo.UserName;

                dtoDados.PreservarSituacaoDadoColeta = true;
                dtoDados.IsMonitorar = true;
                SalvarDadoColetaNaoEstruturada(dtoDados, null, coletaInsumo);

                SituacaoColetaInsumo situacaoAprovada = situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.Aprovado);
                coletaInsumo.Situacao = situacaoAprovada;

                historicoService.CriarSalvarHistoricoColetaInsumo(coletaInsumo);
            }
        }

        public void RejeitarColetaDadosNaoEstruturados(DadosMonitoramentoColetaInsumoDTO dto,
            DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dtoDados)
        {
            ColetaInsumo coletaInsumo = coletaInsumoRepository.FindByKeyConcurrencyValidate(dto.IdColetaInsumo, dto.VersaoColetaInsumo);
            if (coletaInsumo != null)
            {
                Parametro parametro = parametroService.ObterParametro(ParametroEnum.MensagemRejeicaoColeta);

                IList<string> mensagens = new List<string>();
                ValidarMotivoRejeicaoColeta(dto.MotivoRejeicaoONS, mensagens);
                ValidarSituacaoMonitorarDados(coletaInsumo, mensagens);
                VerificarONSBusinessException(mensagens);

                coletaInsumo.MotivoAlteracaoONS = dto.MotivoAlteracaoONS;
                coletaInsumo.MotivoRejeicaoONS = dto.MotivoRejeicaoONS;
                coletaInsumo.LoginAgenteAlteracao = UserInfo.UserName;

                dtoDados.IsMonitorar = true;
                dtoDados.PreservarSituacaoDadoColeta = true;
                SalvarDadoColetaNaoEstruturada(dtoDados, null, coletaInsumo);

                SituacaoColetaInsumo situacaoRejeitada = situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.Rejeitado);
                coletaInsumo.Situacao = situacaoRejeitada;

                historicoService.CriarSalvarHistoricoColetaInsumo(coletaInsumo);

                string assunto = string.Format("[ONS-WEBPMO] Rejeição da coleta do insumo {0} do agente {1}",
                    coletaInsumo.Insumo.Nome, coletaInsumo.Agente.Nome);

                List<int> idAgentes = new List<int> { coletaInsumo.AgenteId };

                notificacaoService.NotificarUsuariosPorAgentes(idAgentes, assunto, parametro.Valor);

                List<Agente> agentes = new List<Agente>();
                Agente agente = new Agente();
                agente.Id = coletaInsumo.AgenteId;
                agentes.Add(agente);

                logNotificacaoService.LogarNotificacao(coletaInsumo.SemanaOperativa, agentes,
                    coletaInsumo.SemanaOperativa.DataHoraAtualizacao, UserInfo.UserName, LogNotificacaoService.LOG_NOTIFICACAO_REJEICAO);
            }
        }

        public void CapturarColetaDados(DadosMonitoramentoColetaInsumoDTO dto)
        {
            SituacaoColetaInsumo situacaoCapturado = situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.Capturado);
            IList<ColetaInsumo> coletasInsumo = new List<ColetaInsumo>();
            foreach (KeyValuePair<int, string> coletaInsumoCaptura in dto.IdsColetaInsumoCapturaVersaoString)
            {
                byte[] versaoEmByteArray = Convert.FromBase64String(coletaInsumoCaptura.Value);
                ColetaInsumo coletaInsumo = coletaInsumoRepository.FindByKeyConcurrencyValidate(
                    coletaInsumoCaptura.Key, versaoEmByteArray);
                if (coletaInsumo != null)
                {
                    coletasInsumo.Add(coletaInsumo);
                }
            }
            IList<string> mensagens = new List<string>();
            if (coletasInsumo.Any())
            {
                SemanaOperativa semanaOperativa = coletasInsumo.First().SemanaOperativa;
                semanaOperativa.DataHoraAtualizacao = DateTime.Now;
                ValidarEstudoSituacaoColetaDados(semanaOperativa, mensagens);
            }

            foreach (ColetaInsumo coletaInsumo in coletasInsumo)
            {
                ValidarSituacaoColetaAoEfetuarCaptura(coletaInsumo, mensagens);

                coletaInsumo.DataHoraAtualizacao = DateTime.Now;
                coletaInsumo.LoginAgenteAlteracao = UserInfo.UserName;
                coletaInsumo.Situacao = situacaoCapturado;

                historicoService.CriarSalvarHistoricoColetaInsumo(coletaInsumo);
            }

            VerificarONSBusinessException(mensagens);
        }
        #endregion

        #region Abertura e fechamento de coleta de dados

        public bool situacaoBoolSemanaOperativa(DadosSemanaOperativaDTO dadosSemanaOperativaDto)
        {
            SemanaOperativa semanaOperativa = semanaOperativaRepository
                .FindByKeyConcurrencyValidate(
                    dadosSemanaOperativaDto.IdSemanaOperativa,
                    dadosSemanaOperativaDto.VersaoSemanaOperativa);

            if (semanaOperativa != null)
            {
                return semanaOperativa.Situacao.Id == (int)SituacaoSemanaOperativaEnum.Configuracao;
            }
            else
            {
                return false;
            }
        }

        public void AbrirColeta(DadosSemanaOperativaDTO dadosSemanaOperativaDto)
        {
            List<Agente> agentes = new List<Agente>();

            SemanaOperativa semanaOperativa = semanaOperativaRepository
                .FindByKeyConcurrencyValidate(
                    dadosSemanaOperativaDto.IdSemanaOperativa,
                    dadosSemanaOperativaDto.VersaoSemanaOperativa);

            if (semanaOperativa != null)
            {
                if (dadosSemanaOperativaDto.ReenvioDeNotificacao)
                {
                    if (semanaOperativa.Situacao.Id == (int)SituacaoSemanaOperativaEnum.ColetaDados)
                    {
                        GabaritoParticipantesFilter filter = new GabaritoParticipantesFilter() { IdSemanaOperativa = dadosSemanaOperativaDto.IdSemanaOperativa };

                        IList<int> idsAgente = null;
                        if (dadosSemanaOperativaDto.EnviarTodos)
                        {
                            idsAgente = agenteService.ConsultarAgentesParticipamGabarito(filter)
                                        .Select(agente => agente.Id)
                                        .ToList();
                        }
                        else
                        {
                            var filterColeta = new PesquisaMonitorarColetaInsumoFilter();
                            filterColeta.IdSemanaOperativa = semanaOperativa.Id;
                            filterColeta.IdsSituacaoColeta.Add((int)SituacaoColetaInsumoEnum.EmAndamento);
                            filterColeta.IdsSituacaoColeta.Add((int)SituacaoColetaInsumoEnum.NaoIniciado);
                            filterColeta.IdsSituacaoColeta.Add((int)SituacaoColetaInsumoEnum.Rejeitado);
                            filterColeta.PageIndex = 1;
                            filterColeta.PageSize = int.MaxValue;

                            var coletas = coletaInsumoRepository.ConsultarParaInformarDados(filterColeta);
                            idsAgente = coletas.List.Select(c => c.AgenteId).Distinct().ToList();
                        }
                        if (idsAgente.Any())
                        {
                            notificacaoService.NotificarUsuariosPorAgentesList(idsAgente, dadosSemanaOperativaDto.Assunto, dadosSemanaOperativaDto.Mensagem);

                            agentes = agenteService.ObterAgentesPorIds(idsAgente);
                            DateTime dataHoraAcao = DateTime.Now;

                            logNotificacaoService.LogarNotificacao(semanaOperativa, agentes,
                                dataHoraAcao, UserInfo.UserName, LogNotificacaoService.LOG_NOTIFICACAO_REABERTURA);
                        }

                    }
                    else
                    {
                        IList<string> mensagens = new List<string>();
                        mensagens.Add(SGIPMOMessages.MS023);
                        VerificarONSBusinessException(mensagens);
                    }
                }
                else
                {
                    IList<string> mensagens = new List<string>();
                    ValidarEstudoConvergenciaCCEE(semanaOperativa, mensagens);
                    VerificarONSBusinessException(mensagens);

                    if (semanaOperativa.Situacao.Id == (int)SituacaoSemanaOperativaEnum.Configuracao)
                    {
                        Parametro parametro = parametroService.ObterParametro(ParametroEnum.MensagemAberturaColeta);

                        GabaritoParticipantesFilter filter = new GabaritoParticipantesFilter() { IdSemanaOperativa = dadosSemanaOperativaDto.IdSemanaOperativa };

                        IList<int> idsAgente = agenteService.ConsultarAgentesParticipamGabarito(filter)
                            .Select(agente => agente.Id)
                            .ToList();

                        var cultura = CultureInfo.CurrentCulture;
                        string nomeMes = cultura.TextInfo.ToTitleCase(
                                cultura.DateTimeFormat.GetMonthName(semanaOperativa.PMO.MesReferencia));

                        notificacaoService.NotificarUsuariosPorAgentesList(idsAgente, dadosSemanaOperativaDto.Assunto, dadosSemanaOperativaDto.Mensagem);

                        agentes = agenteService.ObterAgentesPorIds(idsAgente);

                        DateTime dataHoraAcao = DateTime.Now;
                        logNotificacaoService.LogarNotificacao(semanaOperativa, agentes,
                              dataHoraAcao, UserInfo.UserName, LogNotificacaoService.LOG_NOTIFICACAO_ABERTURA);


                    }

                    semanaOperativa.Situacao = situacaoSemanaOperativaRepository
                        .FindByKey((int)SituacaoSemanaOperativaEnum.ColetaDados);

                    semanaOperativa.DataHoraAtualizacao = DateTime.Now;

                    historicoService.CriarSalvarHistoricoSemanaOperativa(semanaOperativa);

                    // Excluindo todos os arquivos associados à semana operativa
                    foreach (var arquivoSemana in semanaOperativa.Arquivos)
                    {
                        arquivoRepository.Delete(arquivoSemana.Arquivo);
                    }
                    arquivoSemanaOperativaRepository.Delete((ArquivoSemanaOperativa)semanaOperativa.Arquivos);
                }
            }
        }

        public void FecharColeta(DadosSemanaOperativaDTO dadosSemanaOperativaDto)
        {
            SemanaOperativa semanaOperativa = semanaOperativaRepository
                .FindByKeyConcurrencyValidate(
                    dadosSemanaOperativaDto.IdSemanaOperativa,
                    dadosSemanaOperativaDto.VersaoSemanaOperativa);

            if (semanaOperativa != null)
            {
                IList<string> mensagens = new List<string>();
                ValidarEstudoSituacaoColetaDados(semanaOperativa, mensagens);
                VerificarONSBusinessException(mensagens);
                ValidarExisteColetaNaoAprovado(dadosSemanaOperativaDto.IdSemanaOperativa, mensagens);
                VerificarONSBusinessException(mensagens);

                semanaOperativa.Situacao =
                    situacaoSemanaOperativaRepository.FindByKey((int)SituacaoSemanaOperativaEnum.GeracaoBlocos);
                semanaOperativa.DataHoraAtualizacao = DateTime.Now;

                // Excluindo todos os arquivos associados à semana operativa
                foreach (var arquivoSemana in semanaOperativa.Arquivos)
                {
                    arquivoRepository.Delete(arquivoSemana.Arquivo);
                }
                arquivoSemanaOperativaRepository.Delete((ArquivoSemanaOperativa)semanaOperativa.Arquivos);

                historicoService.CriarSalvarHistoricoSemanaOperativa(semanaOperativa);
            }
        }

        public Parametro MensagemAberturaColetaEditavel(DadosSemanaOperativaDTO dadosSemanaOperativaDto)
        {
            SemanaOperativa semanaOperativa = semanaOperativaRepository
                .FindByKeyConcurrencyValidate(
                    dadosSemanaOperativaDto.IdSemanaOperativa,
                    dadosSemanaOperativaDto.VersaoSemanaOperativa);

            Parametro parametro = null;

            if (semanaOperativa != null)
            {
                if (semanaOperativa.Situacao.Id == (int)SituacaoSemanaOperativaEnum.Configuracao)
                {
                    parametro = parametroService.ObterParametro(ParametroEnum.MensagemAberturaColeta);
                }
                else if (semanaOperativa.Situacao.Id == (int)SituacaoSemanaOperativaEnum.ColetaDados)
                {
                    parametro = parametroService.ObterParametro(ParametroEnum.MensagemAberturaColeta);
                }
            }

            return parametro;
        }

        public bool VerificarPermissaoIncluirManutencao()
        {
            bool escopoONS = VerificaSeUsuarioLogadoTemEscopoONS();

            if (escopoONS)
            {
                return true;
            }
            else if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["VisualizarBotaoIncluirManutencao"]))
            {
                if (ConfigurationManager.AppSettings["VisualizarBotaoIncluirManutencao"].ToLower() == "true")
                    return true;
            }

            return false;
        }

        private bool VerificaSeUsuarioLogadoTemEscopoONS()
        {
            Parametro parametroAgenteONS = parametroService.ObterParametro(ParametroEnum.CodigoAgenteONS);

            var agentesUsuarioLogado = UserInfo.ConsultarIdsAgentesUsuarioLogado();

            int idAgenteONS = int.Parse(parametroAgenteONS.Valor);

            return agentesUsuarioLogado.Any(id => id == idAgenteONS);
        }

        #endregion

        #region Coleta de Insumo Não-Estruturado

        /// <summary>
        /// Método utilizado para consultar as informações de um DadoColeta que seja do tipo não estruturado. Serão considerados os arquivos no retorno do mesmo.
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public DadoColetaNaoEstruturadoDTO ObterDadoColetaNaoEstruturado(
            DadoColetaInsumoNaoEstruturadoFilter filtros)
        {
            DadoColetaNaoEstruturado dadoColeta = dadoColetaNaoEstruturadoRepository.ObterDadoColetaNaoEstruturado(filtros);
            if (dadoColeta != null)
            {
                DadoColetaNaoEstruturadoDTO retorno = new DadoColetaNaoEstruturadoDTO();
                retorno.NomeAgente = dadoColeta.ColetaInsumo.NomeAgentePerfil;
                retorno.NomeInsumo = dadoColeta.ColetaInsumo.Insumo.Nome;
                retorno.NomeSemanaOperativa = dadoColeta.ColetaInsumo.SemanaOperativa.Nome;
                retorno.DescricaoSituacaoSemanaOperativa = dadoColeta.ColetaInsumo.SemanaOperativa.Situacao.Descricao;
                retorno.IdAgente = dadoColeta.ColetaInsumo.Agente.Id;
                retorno.IdColetaInsumo = dadoColeta.ColetaInsumo.Id;
                retorno.IdDadoColetaInsumo = dadoColeta.Id;
                retorno.IdGabarito = dadoColeta.Gabarito.Id;
                retorno.IdInsumo = dadoColeta.ColetaInsumo.Insumo.Id;
                retorno.IdSemanaOperativa = dadoColeta.ColetaInsumo.SemanaOperativa.Id;
                retorno.IdSituacaoColetaInsumo = dadoColeta.ColetaInsumo.Situacao.Id;
                retorno.IdSituacaoSemanaOperativa = dadoColeta.ColetaInsumo.SemanaOperativa.Situacao.Id;
                retorno.MotivoAlteracaoONS = dadoColeta.ColetaInsumo.MotivoAlteracaoONS;
                retorno.MotivoRejeicaoONS = dadoColeta.ColetaInsumo.MotivoRejeicaoONS;
                retorno.Observacao = dadoColeta.Observacao;
                retorno.VersaoColetaInsumo = dadoColeta.ColetaInsumo.Versao;
                retorno.IsInsumoParaDECOMP = ((InsumoNaoEstruturado)dadoColeta.ColetaInsumo.Insumo).IsUtilizadoDECOMP;
                foreach (var arquivo in dadoColeta.Arquivos)
                {
                    retorno.Arquivos.Add(CastArquivoParaDTO(arquivo));
                }
                return retorno;
            }
            return null;
        }


        public void SalvarDadoColetaNaoEstruturada(DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dto,
            DadosMonitoramentoColetaInsumoDTO dtoDadosAnalise, ColetaInsumo coletaInsumoParametro = null)
        {
            bool ocorreuAlteracao = false;

            ColetaInsumo coletaInsumo = coletaInsumoParametro;
            if (coletaInsumo == null)
            {
                if (dto.VersaoColetaInsumo != null)
                {
                    coletaInsumo = coletaInsumoRepository.FindByKeyConcurrencyValidate(dto.IdColetaInsumo, dto.VersaoColetaInsumo);
                }
                else
                {
                    coletaInsumo = coletaInsumoRepository.FindByKey(dto.IdColetaInsumo);
                }
            }

            if (coletaInsumo != null)
            {
                ValidarPermiteManutencaoColetaInsumo(coletaInsumo, dto.IsMonitorar, null);

                coletaInsumo.LoginAgenteAlteracao = "ons\\fabio.sander"; // Context.GetUserName();
                coletaInsumo.DataHoraAtualizacao = DateTime.Now;

                SemanaOperativa semanaOperativa = coletaInsumo.SemanaOperativa;
                semanaOperativa.DataHoraAtualizacao = DateTime.Now;

                #region Consultando o gabarito associado a esta "Coleta Insumo"

                int? idSemanaOperativa = null;
                if (semanaOperativa != null) idSemanaOperativa = semanaOperativa.Id;
                IList<Gabarito> gabaritosResultadoConsulta = gabaritoRepository.ConsultarPorGabaritoFilter(
                    new GabaritoConfiguracaoFilter()
                    {
                        CodigoPerfilONS = coletaInsumo.CodigoPerfilONS,
                        IsNullCodigoPerfilONS = string.IsNullOrEmpty(coletaInsumo.CodigoPerfilONS),
                        IdAgente = coletaInsumo.Agente.Id,
                        IdInsumo = coletaInsumo.Insumo.Id,
                        IdSemanaOperativa = idSemanaOperativa,
                        IsPadrao = !idSemanaOperativa.HasValue,
                        IsOrigemColetaNull = true
                    });
                Gabarito gabarito = gabaritosResultadoConsulta.First();

                #endregion

                // Tratando os campos de motivo de alteração e rejeição caso os estejam sendo mencionados
                if (dtoDadosAnalise != null)
                {
                    coletaInsumo.MotivoAlteracaoONS = dtoDadosAnalise.MotivoAlteracaoONS;
                    coletaInsumo.MotivoRejeicaoONS = dtoDadosAnalise.MotivoRejeicaoONS;
                }

                // Tratando devidamente a situação da coleta
                if (dto.EnviarDadosAoSalvar)
                {
                    SituacaoColetaInsumo situacaoColetaInsumoInformado =
                        situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.Informado);
                    SituacaoColetaInsumo situacaoColetaInsumoAprovado =
                        situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.Aprovado);

                    coletaInsumo.Situacao = coletaInsumo.Insumo.PreAprovado
                        ? situacaoColetaInsumoAprovado
                        : situacaoColetaInsumoInformado;

                    historicoService.CriarSalvarHistoricoColetaInsumo(coletaInsumo);
                }
                else
                {
                    if (coletaInsumo.Situacao.Id == (int)SituacaoColetaInsumoEnum.NaoIniciado ||
                        coletaInsumo.Situacao.Id == (int)SituacaoColetaInsumoEnum.Rejeitado)
                    {
                        SituacaoColetaInsumo situacaoColetaInsumoEmAndamento =
                            situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.EmAndamento);

                        coletaInsumo.Situacao = situacaoColetaInsumoEmAndamento;

                        historicoService.CriarSalvarHistoricoColetaInsumo(coletaInsumo);
                    }
                    else
                    {
                        if (!dto.PreservarSituacaoDadoColeta)
                        {
                            SituacaoColetaInsumo situacaoColetaInsumoEmAndamento = situacaoColetaInsumoRepository
                                .FindByKey((int)SituacaoColetaInsumoEnum.EmAndamento);
                            coletaInsumo.Situacao = situacaoColetaInsumoEmAndamento;

                            historicoService.CriarSalvarHistoricoColetaInsumo(coletaInsumo);
                        }
                    }
                }

                if (string.IsNullOrEmpty(dto.Observacao)) dto.Observacao = string.Empty;

                // Caso o dado já exista, então apenas o atualiza, considerando os novos arquivos inseridos
                if (coletaInsumo.DadosColeta != null && coletaInsumo.DadosColeta.Any())
                {
                    //DadoColetaNaoEstruturado dadoColetaExistente = this.dadoColetaNaoEstruturadoRepository.FindByKey(coletaInsumo.DadosColeta.First().Id);
                    DadoColetaNaoEstruturado dadoColetaExistente =
                        (DadoColetaNaoEstruturado)coletaInsumo.DadosColeta.First();

                    if (string.IsNullOrEmpty(dadoColetaExistente.Observacao)) dadoColetaExistente.Observacao = string.Empty;
                    if (!dadoColetaExistente.Observacao.Equals(dto.Observacao)) ocorreuAlteracao = true;

                    dadoColetaExistente.Observacao = dto.Observacao;
                    dadoColetaExistente.ColetaInsumo = coletaInsumo;

                    #region Tratando exclusão de arquivos

                    List<Arquivo> arquivosParaExclusao = new List<Arquivo>();
                    foreach (var arquivoJaArmazenado in dadoColetaExistente.Arquivos)
                    {
                        if (!dto.Arquivos.Select(row => row.Id).ToList().Contains(arquivoJaArmazenado.Id))
                        {
                            arquivosParaExclusao.Add(arquivoJaArmazenado);
                            ocorreuAlteracao = true;
                        }
                    }

                    arquivoRepository.Delete(arquivosParaExclusao);

                    foreach (Arquivo arquivo in arquivosParaExclusao)
                    {
                        dadoColetaExistente.Arquivos.Remove(arquivo);
                    }

                    #endregion

                    foreach (var arquivo in dto.Arquivos)
                    {
                        if (arquivo.Id == Guid.Empty)
                        {
                            Arquivo arquivoAindaNaoSalvo = ObterArquivoUpload(arquivo);
                            Arquivo arquivoRecemGravadoBancoDados = arquivoRepository.SalvarArquivoContentFile(arquivoAindaNaoSalvo);
                            dadoColetaExistente.Arquivos.Add(arquivoRecemGravadoBancoDados);
                            ocorreuAlteracao = true;
                        }
                    }
                }
                else
                {
                    // Instanciando objeto que armazena o dado não-estruturado e em seguida o armazenando
                    var novoDadoColeta = new DadoColetaNaoEstruturado();
                    foreach (var arquivo in dto.Arquivos)
                    {
                        if (arquivo.Id == Guid.Empty)
                        {
                            Arquivo arquivoAindaNaoSalvo = ObterArquivoUpload(arquivo);
                            Arquivo arquivoRecemGravadoBancoDados = arquivoRepository.SalvarArquivoContentFile(arquivoAindaNaoSalvo);
                            novoDadoColeta.Arquivos.Add(arquivoRecemGravadoBancoDados);
                            ocorreuAlteracao = true;
                        }
                    }

                    if (!string.IsNullOrEmpty(dto.Observacao)) ocorreuAlteracao = true;

                    novoDadoColeta.Observacao = dto.Observacao;
                    novoDadoColeta.TipoDadoColeta = "L";
                    novoDadoColeta.ColetaInsumo = coletaInsumo;
                    novoDadoColeta.Gabarito = gabarito;
                    dadoColetaNaoEstruturadoRepository.Add(novoDadoColeta);
                }

                if (ocorreuAlteracao && dto.IsMonitorar)
                {
                    ValidarMotivoAlteracaoColeta(coletaInsumo.MotivoAlteracaoONS);
                }
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

        public ISet<Arquivo> ObterArquivosUpload(ISet<ArquivoDadoNaoEstruturadoDTO> arquivos, bool desconsiderarJaGravadosBancoDados = false)
        {
            ISet<Arquivo> arquivosRetorno = new HashSet<Arquivo>();

            foreach (ArquivoDadoNaoEstruturadoDTO arquivo in arquivos)
            {
                if (arquivo.Id == Guid.Empty)
                {
                    arquivosRetorno.Add(ObterArquivoUpload(arquivo));
                }
                else
                {
                    if (!desconsiderarJaGravadosBancoDados)
                    {
                        arquivosRetorno.Add(ObterArquivoUpload(arquivo));
                    }
                }
            }
            return arquivosRetorno;
        }

        private Arquivo ObterArquivoUpload(ArquivoDadoNaoEstruturadoDTO arquivoDTO)
        {
            BinaryData fileContent = new BinaryData
            {
                Data = FileTemp.GetFileTempByFullpath(arquivoDTO.CaminhoFisicoCompleto)
            };

            var arquivoEnviado = new Arquivo()
            {
                Id = arquivoDTO.Id,
                Content = fileContent,
                Nome = arquivoDTO.Nome,
                MimeType = arquivoDTO.MimeType,
                HashVerificacao = FileUtil.GetMD5Hash(fileContent.Data),
                Tamanho = fileContent.Data.Length
            };
            return arquivoEnviado;
        }

        public void DeletarArquivos(DadosSemanaOperativaDTO dadosSemanaOperativaDto)
        {
            SemanaOperativa semanaOperativa = semanaOperativaRepository
                .FindByKeyConcurrencyValidate(
                    dadosSemanaOperativaDto.IdSemanaOperativa,
                    dadosSemanaOperativaDto.VersaoSemanaOperativa);

            if (semanaOperativa != null)
            {
                // Excluindo todos os arquivos associados à semana operativa
                foreach (var arquivoSemana in semanaOperativa.Arquivos)
                {
                    arquivoRepository.Delete(arquivoSemana.Arquivo);
                }
                arquivoSemanaOperativaRepository.Delete((ArquivoSemanaOperativa)semanaOperativa.Arquivos);
            }
        }


        #endregion

        #region Geração de bloco
        public IList<DadoColetaBloco> ConsultarDadosColetaParaGeracaoBloco(int idSemanaOperativa)
        {
            IList<ColetaInsumo> coletasInsumoBloco = coletaInsumoRepository.ConsultarColetaParticipaBloco(idSemanaOperativa);

            IList<Gabarito> gabaritosBloco = gabaritoRepository.ConsultarGabaritoParticipaBloco(idSemanaOperativa);

            IList<TipoPatamar> tiposPatamar = tipoPatamarRepository.GetAll();
            IList<TipoLimite> tiposLimites = tipoLimiteRepository.GetAll();

            IList<DadoColetaBloco> dadosBloco = new List<DadoColetaBloco>();

            foreach (ColetaInsumo coletaInsumo in coletasInsumoBloco)
            {
                IList<DadoColetaBloco> dadosColetaBloco = new List<DadoColetaBloco>();

                IList<Gabarito> gabaritos = gabaritosBloco
                    .Where(g => g.AgenteId == coletaInsumo.AgenteId
                        && g.InsumoId == coletaInsumo.InsumoId
                        && g.CodigoPerfilONS == coletaInsumo.CodigoPerfilONS)
                    .ToList();

                foreach (var gabarito in gabaritos)
                {
                    InsumoEstruturado insumoGabarito = gabarito.Insumo as InsumoEstruturado;

                    var grandezas = insumoGabarito.Grandezas.Where(g => g.Ativo && g.ParticipaBlocoMontador);

                    foreach (var grandeza in grandezas)
                    {
                        DadoColetaBloco dadoColetaGrandeza = new DadoColetaBloco
                        {
                            ColetaInsumo = coletaInsumo,
                            Gabarito = gabarito,
                            Grandeza = grandeza,
                            Insumo = insumoGabarito
                        };

                        dadosColetaBloco.Add(dadoColetaGrandeza);
                    }
                }

                IList<DadoColetaBloco> dadosColetaBlocoPatamar = dadosColetaBloco.ToList();

                foreach (var dadoColetaBloco in dadosColetaBlocoPatamar)
                {
                    if (dadoColetaBloco.Grandeza.IsColetaPorPatamar)
                    {
                        dadosColetaBloco.Remove(dadoColetaBloco);
                        foreach (var tipoPatamar in tiposPatamar)
                        {
                            DadoColetaBloco dadosColetaPatamar = new DadoColetaBloco(dadoColetaBloco);
                            dadosColetaPatamar.TipoPatamar = (TipoPatamarEnum)tipoPatamar.Id;
                            dadosColetaBloco.Add(dadosColetaPatamar);
                        }
                    }
                }

                IList<DadoColetaBloco> dadosColetaGrandezaLimite = dadosColetaBloco.ToList();

                foreach (var dadoColetaBloco in dadosColetaGrandezaLimite)
                {
                    if (dadoColetaBloco.Grandeza.IsColetaPorLimite)
                    {
                        dadosColetaBloco.Remove(dadoColetaBloco);
                        foreach (var tipoLimite in tiposLimites)
                        {
                            DadoColetaBloco dadosColetaLimite = new DadoColetaBloco(dadoColetaBloco);
                            dadosColetaLimite.TipoLimite = (TipoLimiteEnum)tipoLimite.Id;
                            dadosColetaBloco.Add(dadosColetaLimite);
                        }
                    }
                }

                int numeroRevisao = coletaInsumo.SemanaOperativa.Revisao;

                InsumoEstruturado insumoColeta = coletaInsumo.Insumo as InsumoEstruturado;
                int qtdSemanas = coletaInsumo.SemanaOperativa.PMO.SemanasOperativas.Count;
                int qtdMesesAdiante = insumoColeta.QuantidadeMesesAdiante
                    ?? coletaInsumo.SemanaOperativa.PMO.QuantidadeMesesAdiante
                    ?? default(int);
                int qtdRevisao = qtdSemanas + qtdMesesAdiante;

                IList<DadoColetaBloco> dadosColetaGrandezaEstagio = dadosColetaBloco.ToList();

                foreach (var dadoColetaBloco in dadosColetaGrandezaEstagio)
                {
                    if (dadoColetaBloco.Grandeza.IsColetaPorEstagio)
                    {
                        dadosColetaBloco.Remove(dadoColetaBloco);
                        for (int i = numeroRevisao; i < qtdRevisao; i++)
                        {
                            DadoColetaBloco dadosColetaEstagio = new DadoColetaBloco(dadoColetaBloco);
                            dadosColetaEstagio.Estagio = i >= qtdSemanas
                                ? string.Format("M{0}", i - qtdSemanas + 1)
                                : string.Format("S{0}", i + 1);
                            dadosColetaBloco.Add(dadosColetaEstagio);
                        }
                    }
                    else
                    {
                        dadoColetaBloco.Estagio = string.Format("S{0}", coletaInsumo.SemanaOperativa.Revisao + 1);
                    }
                }

                dadosBloco.Add((DadoColetaBloco)dadosColetaBloco);
            }

            return dadosBloco;
        }

        #endregion



        #region Validações para Insumo Volume Inicial

        /// <summary>
        /// Checa se volume passado por usina é igual ao da revisão anterior
        /// </summary>
        /// <param name="dadosColeta"></param>
        /// <param name="idColetaInsumo"></param>
        /// <returns></returns>
        public string ChecarSeVolumeInicialIgualAoDaSemanaAnterior(IList<ValorDadoColetaDTO> dadosColeta, int idColetaInsumo)
        {

            var coletaInsumo = coletaInsumoRepository.FindByKey(idColetaInsumo);
            var semana = semanaOperativaRepository.FindByKey(coletaInsumo.SemanaOperativaId);

            var usinasComVolumesIguais = new List<string>();
            ColetaInsumo coletaInsumoAnterior = null;
            var resposta = string.Empty;

            if (semana.Revisao > 0)
            {
                coletaInsumoAnterior = coletaInsumoRepository.ObterColetaInsumoAnterior(coletaInsumo);
            }
            else
            {
                coletaInsumoAnterior = coletaInsumoRepository.ObterColetaInsumoSemanaOperativaAnterior(coletaInsumo);
            }

            if (coletaInsumoAnterior != null)
            {
                var dadosColetaInsumoAnterior = coletaInsumoAnterior.DadosColeta;

                if (dadosColetaInsumoAnterior.Any())
                {
                    foreach (var itemDadoColeta in dadosColeta)
                    {
                        var coletasPorGrandeza = dadosColetaInsumoAnterior.Where(d => d.GrandezaId == itemDadoColeta.GrandezaId & d.Gabarito.OrigemColetaId == itemDadoColeta.OrigemColetaId);

                        if (coletasPorGrandeza.Any())
                        {
                            var dadoColetaAnterior = (DadoColetaEstruturado)coletasPorGrandeza.FirstOrDefault();

                            if (dadoColetaAnterior.Valor.Equals(itemDadoColeta.Valor))
                            {
                                usinasComVolumesIguais.Add(itemDadoColeta.OrigemColetaNome);
                            }
                        }

                    }

                    if (usinasComVolumesIguais.Any())
                    {
                        if (usinasComVolumesIguais.Count() == 1)
                        {
                            resposta = string.Format(SGIPMOMessages.MS080, usinasComVolumesIguais.First());
                        }
                        else
                        {
                            resposta = string.Format(SGIPMOMessages.MS081, string.Join(",", usinasComVolumesIguais));
                        }
                    }
                }
            }
            return resposta;
        }




        /// <summary>
        /// Exemplo: Para id_origemcoleta = 'N,277,H,AMBA' extrair o TEXTO AMBA
        /// </summary>
        /// <param name="origemColetaId"></param>
        /// <returns></returns>
        private static string ExtrairUsinaIdDaOrigem(string origemColetaId)
        {
            var idsOrigem = origemColetaId.Split(',');

            //id da usina está na última posição da string
            return idsOrigem.Last();
        }

        #endregion


    }
}
