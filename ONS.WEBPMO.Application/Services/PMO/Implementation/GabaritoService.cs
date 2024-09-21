using System;
using ONS.Common.Seguranca;
using ONS.Common.Util;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    using Common.Util.Pagination;
    using Entities;
    using Entities.Filters;
    using Repositories;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Exceptions;
    using Common.Services.Impl;
    using ONS.WEBPMO.Application.Services.PMO.Interfaces;
    using ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta;

    public class GabaritoService : Service, IGabaritoService
    {
        private readonly IGabaritoRepository gabaritoRepository;
        private readonly ISemanaOperativaRepository semanaOperativaRepository;
        private readonly IInsumoRepository insumoRepository;
        private readonly IColetaInsumoRepository coletaInsumoRepository;
        private readonly ISituacaoColetaInsumoRepository situacaoColetaInsumoRepository;
        private readonly IDadoColetaEstruturadoRepository dadoColetaEstruturadoRepository;
        private readonly IDadoColetaManutencaoRepository dadoColetaManutencaoRepository;
        private readonly IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository;
        private readonly IDadoColetaRepository dadoColetaRepository;
        private readonly IArquivoRepository arquivoRepository;
        private readonly IAgenteService agenteService;
        private readonly IOrigemColetaService origemColetaService;
        private readonly IHistoricoService historicoService;

        public GabaritoService(
            IGabaritoRepository gabaritoRepository,
            ISemanaOperativaRepository semanaOperativaRepository,
            IInsumoRepository insumoRepository,
            IColetaInsumoRepository coletaInsumoRepository,
            ISituacaoColetaInsumoRepository situacaoColetaInsumoRepository,
            IAgenteService agenteService,
            IOrigemColetaService origemColetaService,
            IDadoColetaEstruturadoRepository dadoColetaEstruturadoRepository,
            IDadoColetaManutencaoRepository dadoColetaManutencaoRepository,
            IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository,
            IDadoColetaRepository dadoColetaRepository,
            IArquivoRepository arquivoRepository,
            IHistoricoService historicoService)
        {
            this.gabaritoRepository = gabaritoRepository;
            this.semanaOperativaRepository = semanaOperativaRepository;
            this.insumoRepository = insumoRepository;
            this.coletaInsumoRepository = coletaInsumoRepository;
            this.situacaoColetaInsumoRepository = situacaoColetaInsumoRepository;
            this.agenteService = agenteService;
            this.origemColetaService = origemColetaService;
            this.dadoColetaEstruturadoRepository = dadoColetaEstruturadoRepository;
            this.dadoColetaManutencaoRepository = dadoColetaManutencaoRepository;
            this.dadoColetaNaoEstruturadoRepository = dadoColetaNaoEstruturadoRepository;
            this.dadoColetaRepository = dadoColetaRepository;
            this.arquivoRepository = arquivoRepository;
            this.historicoService = historicoService;
        }

        public Gabarito ObterPorChave(int chave)
        {
            return gabaritoRepository.FindByKey(chave);
        }

        public PagedResult<GabaritoAgrupadoAgenteOrigemColetaDTO> ConsultarGabaritosAgrupadoPorAgenteTipoOrigemPaginado(
            GabaritoOrigemColetaFilter filter)
        {
            return gabaritoRepository.ConsultarAgrupadoPorAgenteTipoOrigemPaginado(filter);
        }

        private void VerificarONSBusinessException(IList<string> mensagens)
        {
            if (mensagens.Any())
            {
                throw new ONSBusinessException(mensagens);
            }
        }

        public Gabarito ObterPorColetaInsumoNaoEstruturado(GabaritoDadoColetaNaoEstruturadoFilter filtro)
        {
            IList<Gabarito> gabaritosResultadoConsulta = gabaritoRepository.ConsultarPorGabaritoFilter(
                new GabaritoConfiguracaoFilter
                {
                    CodigoPerfilONS = filtro.PerfilONS,
                    IsNullCodigoPerfilONS = string.IsNullOrWhiteSpace(filtro.PerfilONS),
                    IdAgente = filtro.IdAgente,
                    IdInsumo = filtro.IdInsumo,
                    IdSemanaOperativa = filtro.IdSemanaOperativa,
                    IsPadrao = filtro.IsPadrao,
                    IsOrigemColetaNull = true
                });
            return gabaritosResultadoConsulta.First();
        }

        #region Salvar Gabaritos

        public void SalvarGabarito(GabaritoConfiguracaoDTO dto)
        {
            IList<string> mensagens = new List<string>();
            ValidarInclusaoGabarito(dto, mensagens);
            ValidarSituacaoSemanaOperativa(dto, mensagens);
            ValidarCodigoPerfilONS(dto, mensagens);
            VerificarONSBusinessException(mensagens);

            SalvarGabaritos(dto);
        }

        private void SalvarGabaritos(GabaritoConfiguracaoDTO dto)
        {
            if (dto.TipoInsumo == TipoInsumoEnum.Estruturado)
            {
                switch (dto.TipoOrigemColeta)
                {
                    case TipoOrigemColetaEnum.Reservatorio:
                    case TipoOrigemColetaEnum.Subsistema:
                    case TipoOrigemColetaEnum.Usina:
                    case TipoOrigemColetaEnum.GeracaoComplementar:
                        SalvarGabaritoReservatorioUsinaSubsistemaGeracao(dto);
                        break;
                    case TipoOrigemColetaEnum.UnidadeGeradora:
                        SalvarGabaritoUnidadeGeradora(dto);
                        break;
                }
            }
            else
            {
                SalvarGabaritosNaoEstruturado(dto);
            }
        }

        private void SalvarGabaritosNaoEstruturado(GabaritoConfiguracaoDTO dto)
        {
            Agente agente = agenteService.ObterOuCriarAgentePorChave(dto.IdAgente);

            SemanaOperativa semanaOperativa = semanaOperativaRepository.FindByKey(dto.IdSemanaOperativa);

            SituacaoColetaInsumo situacaoColetaInsumo =
                situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.NaoIniciado);

            IList<Insumo> insumos = insumoRepository.ConsultaInsumoPorIds(dto.IdsInsumo.ToArray());

            foreach (Insumo insumo in insumos)
            {
                Gabarito gabarito = CriarGabarito(dto.IsPadrao, agente, insumo, semanaOperativa, null,
                    dto.CodigoPerfilONS);

                gabaritoRepository.Add(gabarito, false);

                if (!dto.IsPadrao)
                {
                    SalvarColetaInsumo(agente, semanaOperativa, insumo, situacaoColetaInsumo, dto.CodigoPerfilONS);
                }
            }
        }

        private void SalvarGabaritoUnidadeGeradora(GabaritoConfiguracaoDTO dto, IList<Gabarito> gabaritosExistente = null)
        {
            Agente agente = agenteService.ObterOuCriarAgentePorChave(dto.IdAgente);

            SemanaOperativa semanaOperativa = null;
            if (dto.IdSemanaOperativa.HasValue)
            {
                semanaOperativa = semanaOperativaRepository.FindByKey(dto.IdSemanaOperativa);
            }

            IList<OrigemColeta> origensColeta =
                origemColetaService.ConsultarOuCriarOrigemColetaPorIds(dto.IdsOrigemColeta, dto.TipoOrigemColeta);

            SituacaoColetaInsumo situacaoColetaInsumo =
                situacaoColetaInsumoRepository.FindByKey((int)SituacaoColetaInsumoEnum.NaoIniciado);

            IList<Insumo> insumos = insumoRepository.ConsultaInsumoPorIds(dto.IdsInsumo.ToArray());

            foreach (OrigemColeta origemColeta in origensColeta)
            {
                foreach (Insumo insumo in insumos)
                {
                    if (gabaritosExistente == null
                        || !gabaritosExistente.Any(g => g.OrigemColeta.Id == origemColeta.Id && g.Insumo.Id == insumo.Id))
                    {
                        Gabarito gabarito = CriarGabarito(dto.IsPadrao, agente, insumo, semanaOperativa,
                            origemColeta, dto.CodigoPerfilONS);

                        gabaritoRepository.Add(gabarito, false);
                    }
                }
            }

            if (!dto.IsPadrao)
            {
                foreach (Insumo insumo in insumos)
                {
                    SalvarColetaInsumo(agente, semanaOperativa, insumo, situacaoColetaInsumo, dto.CodigoPerfilONS);
                }
            }
        }

        private void SalvarGabaritoReservatorioUsinaSubsistemaGeracao(GabaritoConfiguracaoDTO dto)
        {
            Agente agente = agenteService.ObterOuCriarAgentePorChave(dto.IdAgente);

            SemanaOperativa semanaOperativa = semanaOperativaRepository.FindByKey(dto.IdSemanaOperativa);

            OrigemColeta origemColeta = null;
            if (dto.IdsOrigemColeta.Any())
            {
                origemColeta = origemColetaService.ObterOuCriarOrigemColetaPorId(dto.IdsOrigemColeta.Single(),
                    dto.TipoOrigemColeta);
            }

            SituacaoColetaInsumo situacaoColetaInsumo = situacaoColetaInsumoRepository
                .FindByKey((int)SituacaoColetaInsumoEnum.NaoIniciado);

            IList<Insumo> insumos = insumoRepository.ConsultaInsumoPorIds(dto.IdsInsumo.ToArray());

            foreach (Insumo insumo in insumos)
            {
                Gabarito gabarito = CriarGabarito(dto.IsPadrao, agente, insumo, semanaOperativa,
                    origemColeta, dto.CodigoPerfilONS);

                gabaritoRepository.Add(gabarito, false);

                if (!dto.IsPadrao)
                {
                    SalvarColetaInsumo(agente, semanaOperativa, insumo, situacaoColetaInsumo, dto.CodigoPerfilONS);
                }
            }
        }

        private void SalvarColetaInsumo(Agente agente, SemanaOperativa semanaOperativa, Insumo insumo,
            SituacaoColetaInsumo situacaoColetaInsumo, string codigoPerfilONS)
        {
            bool existeColetaInsumo = coletaInsumoRepository.Any(agente.Id, insumo.Id, semanaOperativa.Id, codigoPerfilONS);
            if (!existeColetaInsumo)
            {
                ColetaInsumo coletaInsumo = new ColetaInsumo();
                coletaInsumo.Agente = agente;
                coletaInsumo.SemanaOperativa = semanaOperativa;
                coletaInsumo.Insumo = insumo;
                coletaInsumo.Situacao = situacaoColetaInsumo;
                coletaInsumo.CodigoPerfilONS = codigoPerfilONS;
                coletaInsumo.DataHoraAtualizacao = DateTime.Now;
                coletaInsumo.LoginAgenteAlteracao = UserInfo.UserName;
                coletaInsumoRepository.Add(coletaInsumo, false);
            }
        }

        #endregion

        #region Alterar Gabaritos
        public void AlterarGabarito(GabaritoConfiguracaoDTO dto)
        {
            IList<string> mensagens = new List<string>();
            ValidarAlteracaoGabarito(dto, mensagens);
            ValidarSituacaoSemanaOperativa(dto, mensagens);
            VerificarONSBusinessException(mensagens);

            AlterarGabaritos(dto);
        }

        private void AlterarGabaritos(GabaritoConfiguracaoDTO dto)
        {
            if (dto.TipoInsumo == TipoInsumoEnum.Estruturado)
            {
                switch (dto.TipoOrigemColeta)
                {
                    case TipoOrigemColetaEnum.Reservatorio:
                    case TipoOrigemColetaEnum.Subsistema:
                    case TipoOrigemColetaEnum.Usina:
                    case TipoOrigemColetaEnum.GeracaoComplementar:
                        AlterarGabaritoReservatorioUsinaSubsistemaGeracao(dto);
                        break;
                    case TipoOrigemColetaEnum.UnidadeGeradora:
                        AlterarGabaritoUnidadeGeradora(dto);
                        break;
                }
            }
            else
            {
                AlterarGabaritoNaoEstruturado(dto);
            }

            if (!dto.IsPadrao && dto.IdSemanaOperativa.HasValue)
            {
                IList<ColetaInsumo> coletasInsumo = coletaInsumoRepository.ConsultarColetasInsumoOrfaos(dto.IdAgente,
                    dto.IdSemanaOperativa.Value, dto.CodigoPerfilONS);
                foreach (ColetaInsumo coletaInsumo in coletasInsumo)
                {
                    historicoService.ExcluirHistoricoColetaInsumo(coletaInsumo.Id);
                    coletaInsumoRepository.Delete(coletaInsumo);
                }
            }
        }

        private void RemoverGabaritos(IList<int> idsGabarito)
        {
            if (idsGabarito.Any())
            {
                var gabaritosExclusao = gabaritoRepository.ConsultarParaRemover(idsGabarito).ToList();
                arquivoRepository.DeletarPorIdGabarito(idsGabarito);
                dadoColetaRepository.Delete(gabaritosExclusao.SelectMany(g => g.DadosColeta));
                gabaritoRepository.Delete(gabaritosExclusao);
            }
        }

        private void AlterarGabaritoNaoEstruturado(GabaritoConfiguracaoDTO dto)
        {
            GabaritoConfiguracaoFilter filter = new GabaritoConfiguracaoFilter();
            filter.IsPadrao = dto.IsPadrao;
            filter.IdAgente = dto.IdAgente;
            filter.IdSemanaOperativa = dto.IdSemanaOperativa;
            filter.TipoInsumo = TipoInsumoEnum.NaoEstruturado.ToChar();
            filter.CodigoPerfilONS = dto.CodigoPerfilONS;
            filter.IsNullCodigoPerfilONS = string.IsNullOrWhiteSpace(dto.CodigoPerfilONS);

            IList<Gabarito> gabaritos = gabaritoRepository.ConsultarPorGabaritoFilter(filter);

            GabaritoConfiguracaoDTO dtoInclusao = new GabaritoConfiguracaoDTO(dto);
            dtoInclusao.IdsInsumo = dto.IdsInsumo
                .Where(idInsumo => gabaritos.All(gabarito => gabarito.Insumo.Id != idInsumo))
                .ToList();

            SalvarGabaritosNaoEstruturado(dtoInclusao);

            IList<int> idsRemovidos = gabaritos
                .Where(g => dto.IdsInsumo.All(i => i != g.Insumo.Id))
                .Select(g => g.Id)
                .ToList();

            RemoverGabaritos(idsRemovidos);
        }

        private void AlterarGabaritoUnidadeGeradora(GabaritoConfiguracaoDTO dto)
        {
            GabaritoConfiguracaoFilter filter = new GabaritoConfiguracaoFilter();
            filter.IsPadrao = dto.IsPadrao;
            filter.IdAgente = dto.IdAgente;
            filter.IdSemanaOperativa = dto.IdSemanaOperativa;
            filter.IdOrigemColetaPai = dto.IdOrigemColetaPai;
            filter.CodigoPerfilONS = dto.CodigoPerfilONS;
            filter.IsNullCodigoPerfilONS = string.IsNullOrWhiteSpace(dto.CodigoPerfilONS);

            IList<Gabarito> gabaritos = gabaritoRepository.ConsultarPorGabaritoFilter(filter);

            SalvarGabaritoUnidadeGeradora(dto, gabaritos);

            IList<int> idsRemovidos = gabaritos
                .Where(g => dto.IdsInsumo.All(id => id != g.Insumo.Id)
                    || dto.IdsOrigemColeta.All(idOrigem => idOrigem != g.OrigemColeta.Id))
                .Select(g => g.Id)
                .ToList();

            RemoverGabaritos(idsRemovidos);
        }

        private void AlterarGabaritoReservatorioUsinaSubsistemaGeracao(GabaritoConfiguracaoDTO dto)
        {
            GabaritoConfiguracaoFilter filter = new GabaritoConfiguracaoFilter();
            filter.IsPadrao = dto.IsPadrao;
            filter.IdAgente = dto.IdAgente;
            filter.IdSemanaOperativa = dto.IdSemanaOperativa;
            filter.IdsOrigemColeta = dto.IdsOrigemColeta;
            filter.CodigoPerfilONS = dto.CodigoPerfilONS;
            filter.TipoInsumo = TipoInsumoEnum.Estruturado.ToChar();
            filter.IsNullCodigoPerfilONS = string.IsNullOrWhiteSpace(dto.CodigoPerfilONS);

            if (!dto.IdsOrigemColeta.Any())
            {
                filter.IsOrigemColetaNull = true;
            }

            IList<Gabarito> gabaritos = gabaritoRepository.ConsultarPorGabaritoFilter(filter);

            GabaritoConfiguracaoDTO dtoInclusao = new GabaritoConfiguracaoDTO(dto);
            dtoInclusao.IdsInsumo = dto.IdsInsumo
                .Where(idInsumo => gabaritos.All(gabarito => gabarito.Insumo.Id != idInsumo))
                .ToList();

            SalvarGabaritoReservatorioUsinaSubsistemaGeracao(dtoInclusao);

            IList<int> idsRemovidos = gabaritos
                .Where(g => dto.IdsInsumo.All(i => i != g.Insumo.Id))
                .Select(g => g.Id)
                .ToList();

            RemoverGabaritos(idsRemovidos);
        }

        #endregion

        private Gabarito CriarGabarito(bool isPadrao, Agente agente, Insumo insumo,
            SemanaOperativa semanaOperativa, OrigemColeta origemColeta, string codigoPerfilONS)
        {
            Gabarito gabarito = new Gabarito();
            gabarito.Agente = agente;
            gabarito.Insumo = insumo;
            gabarito.IsPadrao = isPadrao;
            gabarito.SemanaOperativa = semanaOperativa;
            gabarito.OrigemColeta = origemColeta;
            gabarito.CodigoPerfilONS = codigoPerfilONS;

            return gabarito;
        }

        #region Validações
        #region Validações de Inclusão de Gabarito
        private void ValidarInclusaoGabarito(GabaritoConfiguracaoDTO dto, IList<string> mensagens)
        {
            GabaritoConfiguracaoFilter filter = new GabaritoConfiguracaoFilter();
            filter.IsPadrao = dto.IsPadrao;
            filter.IdSemanaOperativa = dto.IdSemanaOperativa;
            filter.IdsOrigemColeta = dto.IdsOrigemColeta;
            filter.IdsInsumo = dto.IdsInsumo;
            filter.TipoInsumo = char.ToString((char)dto.TipoInsumo);
            filter.IsOrigemColetaNull = dto.TipoOrigemColeta == TipoOrigemColetaEnum.GeracaoComplementar;

            if (dto.TipoInsumo == TipoInsumoEnum.NaoEstruturado ||
                    dto.TipoOrigemColeta == TipoOrigemColetaEnum.GeracaoComplementar)
            {
                filter.IdAgente = dto.IdAgente;
            }

            if (!dto.IdsInsumo.Any())
            {
                mensagens.Add(string.Format(SGIPMOMessages.MS001, "Insumo(s)"));
            }

            IList<Gabarito> gabaritos = gabaritoRepository.ConsultarPorGabaritoFilter(filter);
            foreach (Gabarito gabarito in gabaritos)
            {
                switch (dto.TipoOrigemColeta)
                {
                    case TipoOrigemColetaEnum.Reservatorio:
                        mensagens.Add(gabarito.Agente.Id == dto.IdAgente
                            ? string.Format(SGIPMOMessages.MS015, gabarito.Insumo.Nome, gabarito.OrigemColeta.Nome,
                                gabarito.NomeAgentePerfil)
                            : string.Format(SGIPMOMessages.MS016, gabarito.Insumo.Nome, gabarito.NomeAgentePerfil,
                                gabarito.OrigemColeta.Nome));
                        break;
                    case TipoOrigemColetaEnum.Usina:
                        mensagens.Add(gabarito.Agente.Id == dto.IdAgente
                            ? string.Format(SGIPMOMessages.MS018, gabarito.Insumo.Nome, gabarito.OrigemColeta.Nome,
                                gabarito.NomeAgentePerfil)
                            : string.Format(SGIPMOMessages.MS019, gabarito.Insumo.Nome, gabarito.NomeAgentePerfil,
                                gabarito.OrigemColeta.Nome));
                        break;
                    case TipoOrigemColetaEnum.UnidadeGeradora:
                        mensagens.Add(string.Format(SGIPMOMessages.MS060, gabarito.Insumo.Nome,
                            gabarito.NomeAgentePerfil, gabarito.OrigemColeta.Nome));
                        break;
                    case TipoOrigemColetaEnum.Subsistema:
                        mensagens.Add(string.Format(SGIPMOMessages.MS020, gabarito.Insumo.Nome,
                            gabarito.OrigemColeta.Nome, gabarito.NomeAgentePerfil));
                        break;
                    //Geração complementar e Não estruturado
                    default:
                        mensagens.Add(string.Format(SGIPMOMessages.MS022, gabarito.Insumo.Nome,
                            gabarito.NomeAgentePerfil));
                        break;
                }
            }
        }

        /// <summary>
        /// [MS001] O campo do gabarito deve ser informado para pesquisa.
        /// Valida se caso o agente for ONS o perfil está preenchido
        /// </summary>
        /// <param name="dto">Objeto contendo id do agente e o codico do peril ONS</param>
        /// <param name="mensagens">Lista contendo os erros de negócio</param>
        private void ValidarCodigoPerfilONS(GabaritoConfiguracaoDTO dto, IList<string> mensagens)
        {
            bool isAgenteOns = agenteService.IsAgenteONS(dto.IdAgente);
            if (isAgenteOns && string.IsNullOrWhiteSpace(dto.CodigoPerfilONS))
            {
                mensagens.Add(string.Format(SGIPMOMessages.MS001, "Perfil"));
            }
        }
        #endregion

        #region Validações de Alteração de Gabarito
        private void ValidarAlteracaoGabarito(GabaritoConfiguracaoDTO dto, IList<string> mensagens)
        {
            if (dto.IdsInsumo.Any())
            {
                GabaritoConfiguracaoFilter filter = new GabaritoConfiguracaoFilter();
                filter.IsPadrao = dto.IsPadrao;
                filter.IdSemanaOperativa = dto.IdSemanaOperativa;
                filter.IdsOrigemColeta = dto.IdsOrigemColeta;
                filter.IdsInsumo = dto.IdsInsumo;
                filter.TipoInsumo = char.ToString((char)dto.TipoInsumo);

                if (dto.TipoInsumo == TipoInsumoEnum.NaoEstruturado ||
                    dto.TipoOrigemColeta == TipoOrigemColetaEnum.GeracaoComplementar)
                {
                    filter.IdAgente = dto.IdAgente;
                }

                IList<Gabarito> gabaritos = gabaritoRepository.ConsultarPorGabaritoFilter(filter);
                foreach (Gabarito gabarito in gabaritos)
                {
                    if (gabarito.Agente.Id != dto.IdAgente || gabarito.CodigoPerfilONS != dto.CodigoPerfilONS)
                    {
                        switch (dto.TipoOrigemColeta)
                        {
                            case TipoOrigemColetaEnum.Reservatorio:
                                mensagens.Add(string.Format(SGIPMOMessages.MS016, gabarito.Insumo.Nome,
                                    gabarito.NomeAgentePerfil, gabarito.OrigemColeta.Nome));
                                break;
                            case TipoOrigemColetaEnum.Usina:
                                mensagens.Add(string.Format(SGIPMOMessages.MS019, gabarito.Insumo.Nome,
                                    gabarito.NomeAgentePerfil, gabarito.OrigemColeta.Nome));
                                break;
                            case TipoOrigemColetaEnum.UnidadeGeradora:
                                mensagens.Add(string.Format(SGIPMOMessages.MS060, gabarito.Insumo.Nome,
                                    gabarito.NomeAgentePerfil, gabarito.OrigemColeta.Nome));
                                break;
                            case TipoOrigemColetaEnum.Subsistema:
                                mensagens.Add(string.Format(SGIPMOMessages.MS020, gabarito.Insumo.Nome,
                                    gabarito.OrigemColeta.Nome, gabarito.NomeAgentePerfil));
                                break;
                            //Geração complementar e Não estruturado
                            default:
                                mensagens.Add(string.Format(SGIPMOMessages.MS022, gabarito.Insumo.Nome,
                                    gabarito.NomeAgentePerfil));
                                break;
                        }
                    }
                }
            }
        }

        private void ValidarSituacaoSemanaOperativa(GabaritoConfiguracaoDTO dto, IList<string> mensagens)
        {
            if (dto.IdSemanaOperativa.HasValue)
            {
                SemanaOperativa semanaOperativa = semanaOperativaRepository.FindByKey(dto.IdSemanaOperativa);
                if (semanaOperativa.Situacao.Id != (int)SituacaoSemanaOperativaEnum.Configuracao
                    && semanaOperativa.Situacao.Id != (int)SituacaoSemanaOperativaEnum.ColetaDados)
                {
                    mensagens.Add(SGIPMOMessages.MS059);
                }
            }
        }

        #endregion
        #endregion

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoUsinaDTO>> ConsultarUsinaPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            return gabaritoRepository.ConsultarUsinaPorGabarito(isPadrao, nomeRevisao);
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoUGEDTO>> ConsultarUGEPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            return gabaritoRepository.ConsultarUGEPorGabarito(isPadrao, nomeRevisao);
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoReservatorioDTO>> ConsultarReservatorioPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            return gabaritoRepository.ConsultarReservatorioPorGabarito(isPadrao, nomeRevisao);
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoSubsistemaDTO>> ConsultarSubsistemaPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            return gabaritoRepository.ConsultarSubsistemaPorGabarito(isPadrao, nomeRevisao);
        }
        public IList<GabaritoParticipantesBaseDTO<ReservatorioParticipanteGabaritoDTO>> ConsultarReservatoriosParticipantesGabarito(bool isPadrao, string nomeRevisao = "")
        {
            return gabaritoRepository.ConsultarReservatoriosParticipantesGabarito(isPadrao, nomeRevisao);
        }

        public IList<GabaritoParticipantesBaseDTO<UsinaParticipanteGabaritoDTO>> ConsultarUsinasParticipantesGabarito(bool isPadrao, string nomeRevisao = "")
        {
            return gabaritoRepository.ConsultarUsinasParticipantesGabarito(isPadrao, nomeRevisao);
        }

        public IList<GabaritoParticipantesBaseDTO<AgenteParticipanteGabaritoDTO>> ConsultarAgentesParticipantesGabarito(bool isPadrao, string nomeRevisao = "")
        {
            return gabaritoRepository.ConsultarAgentesParticipantesGabarito(isPadrao, nomeRevisao);
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoInsumoNaoEstruturadoDTO>> ConsultarAgentesPodemEnviarDadosNaoEstruturadosPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            return gabaritoRepository.ConsultarAgentesPodemEnviarDadosNaoEstruturadosPorGabarito(isPadrao, nomeRevisao);
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoGeracaoComplementar>> ConsultarAgentesComGeracaoComplementar(bool isPadrao, string nomeRevisao = "")
        {
            return gabaritoRepository.ConsultarAgentesComGeracaoComplementar(isPadrao, nomeRevisao);
        }
    }
}
