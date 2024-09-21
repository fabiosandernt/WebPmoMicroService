using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta;

namespace ONS.WEBPMO.Domain.Presentations.Impl
{
    public class GabaritoPresentation : IGabaritoPresentation
    {
        private readonly IGabaritoRepository gabaritoRepository;
        private readonly ISemanaOperativaRepository semanaOperativaRepository;
        private readonly IInsumoService insumoService;
        private readonly IOrigemColetaService origemColetaService;
        private readonly IAgenteService agenteService;
        private readonly IInsumoRepository insumoRepository;
        private readonly IAgenteRepository agenteRepository;

        public GabaritoPresentation(
            IGabaritoRepository gabaritoRepository,
            ISemanaOperativaRepository semanaOperativaRepository,
            IInsumoService insumoService,
            IOrigemColetaService origemColetaService,
            IAgenteService agenteService,
            IInsumoRepository insumoRepository,
            IAgenteRepository agenteRepository)
        {
            this.gabaritoRepository = gabaritoRepository;
            this.semanaOperativaRepository = semanaOperativaRepository;
            this.insumoService = insumoService;
            this.origemColetaService = origemColetaService;
            this.agenteService = agenteService;
            this.insumoRepository = insumoRepository;
            this.agenteRepository = agenteRepository;
        }

        public DadosFiltroPesquisaGabaritoDTO ObterDadosFiltroPesquisaGabarito(int? idInsumo, int? idAgente,
            int? idSemanaOperativa)
        {
            DadosFiltroPesquisaGabaritoDTO retorno = new DadosFiltroPesquisaGabaritoDTO();
            if (idInsumo.HasValue)
            {
                Insumo insumo = insumoRepository.FindByKey(idInsumo);
                retorno.Insumos.Add(new ChaveDescricaoDTO<int>
                {
                    Chave = insumo.Id,
                    Descricao = insumo.Nome
                });
            }
            if (idAgente.HasValue)
            {
                Agente agente = agenteRepository.FindByKey(idAgente);
                retorno.Agentes.Add(new ChaveDescricaoDTO<int>
                {
                    Chave = agente.Id,
                    Descricao = agente.Nome
                });
            }
            if (idSemanaOperativa.HasValue)
            {
                SemanaOperativa semanaOperativa = semanaOperativaRepository.FindByKey(idSemanaOperativa);
                retorno.SemanasOperativas.Add(new ChaveDescricaoDTO<int>
                {
                    Chave = semanaOperativa.Id,
                    Descricao = semanaOperativa.Nome
                });
            }
            return retorno;
        }

        #region Configuração Gabarito

        public DadosConfiguracaoGabaritoDTO ObterDadosConfiguracaoGabarito(GabaritoDadosFilter filter)
        {
            DadosConfiguracaoGabaritoDTO dto = new DadosConfiguracaoGabaritoDTO();

            IList<Insumo> insumos = new List<Insumo>();
            IList<OrigemColeta> origensColeta = new List<OrigemColeta>();

            if (filter.TipoInsumo == TipoInsumoEnum.Estruturado)
            {
                switch (filter.TipoOrigemColeta)
                {
                    case TipoOrigemColetaEnum.Subsistema:
                        origensColeta = origemColetaService.ConsultarOrigemColetaPorTipoNomeOnline(
                            filter.TipoOrigemColeta.Value, string.Empty);
                        insumos = insumoService
                            .ConsultarInsumoPorTipoOrigemColetaCategoria(filter.TipoOrigemColeta.Value)
                            .ToList<Insumo>();
                        break;
                    case TipoOrigemColetaEnum.GeracaoComplementar:
                        insumos = insumoService
                            .ConsultarInsumoPorTipoOrigemColetaCategoria(filter.TipoOrigemColeta.Value)
                            .ToList<Insumo>();
                        break;
                    case TipoOrigemColetaEnum.Reservatorio:
                        origensColeta = origemColetaService.ConsultarOrigemColetaPorTipoNomeOnline(
                            filter.TipoOrigemColeta.Value, string.Empty);
                        insumos = insumoService
                            .ConsultarInsumoPorTipoOrigemColetaCategoria(filter.TipoOrigemColeta.Value,
                                CategoriaInsumoEnum.Hidreletrico)
                            .ToList<Insumo>();
                        break;
                    case TipoOrigemColetaEnum.Usina:
                        origensColeta = origemColetaService.ConsultarOrigemColetaPorTipoNomeOnline(
                            filter.TipoOrigemColeta.Value, string.Empty);
                        insumos = string.IsNullOrEmpty(filter.IdOrigemColeta)
                            ? new List<Insumo>()
                            : insumoService.ConsultarInsumoEstruturadosPorUsina(filter.IdOrigemColeta).ToList<Insumo>();
                        break;
                    case TipoOrigemColetaEnum.UnidadeGeradora:
                        origensColeta = origemColetaService.ConsultarOrigemColetaPorTipoNomeOnline(
                            TipoOrigemColetaEnum.Usina, string.Empty);
                        break;
                }

                dto.OrigensColeta.Add(origensColeta.Select(oc => new ChaveDescricaoDTO<string>(oc.Id, oc.Nome)).ToList());
            }
            else
            {
                insumos = insumoService.ConsultarInsumoNaoEstruturado().ToList<Insumo>();
            }

            dto.Insumos = insumos
                .Select(e => new ChaveDescricaoDTO<int>(e.Id, e.Nome))
                .ToList();

            if (filter.IdAgente.HasValue)
            {
                Agente agente = agenteService.ObterAgentePorChaveOnline(filter.IdAgente.Value);
                if (agente != null)
                {
                    dto.Agentes.Add(new ChaveDescricaoDTO<int>(agente.Id, agente.Nome));
                }
            }

            if (filter.IdSemanaOperativa.HasValue)
            {
                SemanaOperativa semanaOperativa = semanaOperativaRepository.FindByKey(filter.IdSemanaOperativa.Value);
                if (semanaOperativa != null)
                {
                    dto.SemanaOperativa = new ChaveDescricaoDTO<int>(semanaOperativa.Id, semanaOperativa.Nome);
                }
            }

            return dto;
        }

        public DadosConfiguracaoGabaritoUnidadeGeradoraDTO ObterDadosConfiguracaoGabaritoUnidadeGeradora(
            GabaritoDadosFilter filter)
        {
            DadosConfiguracaoGabaritoUnidadeGeradoraDTO dadosManutencaoGabarito =
                new DadosConfiguracaoGabaritoUnidadeGeradoraDTO();

            if (filter.IdSemanaOperativa.HasValue)
            {
                SemanaOperativa semanaOperativa = semanaOperativaRepository.FindByKey(filter.IdSemanaOperativa.Value);
                dadosManutencaoGabarito.SemanaOperativa = new ChaveDescricaoDTO<int>(semanaOperativa.Id, semanaOperativa.Nome);
            }

            if (filter.IdAgente.HasValue)
            {
                Agente agente = agenteService.ObterAgentePorChaveOnline(filter.IdAgente.Value);
                dadosManutencaoGabarito.Agentes.Add(new ChaveDescricaoDTO<int>(agente.Id, agente.Nome));
            }

            if (!string.IsNullOrEmpty(filter.IdOrigemColeta))
            {
                IList<OrigemColeta> usinas = origemColetaService
                    .ConsultarOrigemColetaPorTipoNomeOnline(TipoOrigemColetaEnum.Usina, string.Empty);

                dadosManutencaoGabarito.OrigensColeta = usinas
                    .Select(usina => new ChaveDescricaoDTO<string>(usina.Id, usina.Nome))
                    .ToList();

                IList<Insumo> insumos =
                    ConsultarInsumosEstruturados(TipoOrigemColetaEnum.UnidadeGeradora, filter.IdOrigemColeta)
                        .ToList<Insumo>();

                dadosManutencaoGabarito.Insumos = insumos
                    .Select(insumo => new ChaveDescricaoDTO<int>(insumo.Id, insumo.Nome))
                    .ToList();

                IList<OrigemColeta> unidadesGeradoras = origemColetaService
                    .ConsultarUnidadeGeradoraPorUsinaOnline(filter.IdOrigemColeta)
                    .ToList<OrigemColeta>();

                dadosManutencaoGabarito.UnidadesGeradoras = unidadesGeradoras
                    .Select(unidadeGeradora => new ChaveDescricaoDTO<string>(unidadeGeradora.Id, unidadeGeradora.Nome))
                    .ToList();
            }

            return dadosManutencaoGabarito;
        }

        public DadosConfiguracaoGabaritoDTO ObterDadosConfiguracaoGabaritoUnidadeGeradoraPorUsina(string idUsina)
        {
            IList<InsumoEstruturado> insumos = ConsultarInsumosEstruturados(TipoOrigemColetaEnum.UnidadeGeradora, idUsina);
            IList<UnidadeGeradora> unidadesGeradoras = origemColetaService.ConsultarUnidadeGeradoraPorUsinaOnline(idUsina);

            DadosConfiguracaoGabaritoDTO dto = new DadosConfiguracaoGabaritoDTO();

            dto.OrigensColeta = unidadesGeradoras
                .Select(unidadeGeradora => new ChaveDescricaoDTO<string>(unidadeGeradora.Id, unidadeGeradora.Nome))
                .ToList();

            dto.Insumos = insumos.Select(insumo => new ChaveDescricaoDTO<int>(insumo.Id, insumo.Nome)).ToList();

            return dto;
        }

        #endregion

        #region Manutencção Gabarito
        public DadosManutencaoGabaritoDTO ObterDadosManutencaoGabarito(GabaritoOrigemColetaFilter filtro)
        {
            DadosManutencaoGabaritoDTO dto = new DadosManutencaoGabaritoDTO();

            if (filtro.TipoInsumo == TipoInsumoEnum.Estruturado)
            {
                switch (filtro.TipoOrigemColeta)
                {
                    case TipoOrigemColetaEnum.Usina:
                    case TipoOrigemColetaEnum.Subsistema:
                    case TipoOrigemColetaEnum.Reservatorio:
                        dto = ObterDadosManutencaoGabaritoReservatorioUsinaSubsistema(filtro.IdAgente.Value,
                            filtro.IdOrigemColeta, filtro.IdSemanaOperativa, filtro.CodigoPerfilONS);
                        break;
                    case TipoOrigemColetaEnum.UnidadeGeradora:
                        dto = ObterDadosManutencaoGabaritoUnidadeGeradora(filtro.IdAgente.Value,
                            filtro.IdOrigemColeta, filtro.IdSemanaOperativa, filtro.CodigoPerfilONS);
                        break;
                    case TipoOrigemColetaEnum.GeracaoComplementar:
                        dto = ObterDadosManutencaoGabaritoGeracaoComplementar(filtro.IdAgente.Value,
                            filtro.IdSemanaOperativa, filtro.CodigoPerfilONS);
                        break;
                }
            }
            else
            {
                dto = ObterDadosManutencaoGabaritosNaoEstruturado(filtro.IdAgente.Value,
                    filtro.IdSemanaOperativa, filtro.CodigoPerfilONS);
            }

            return dto;
        }

        private DadosManutencaoGabaritoDTO ObterDadosManutencaoGabaritoUnidadeGeradora(int idAgente,
            string idUsina, int? idSemanaOperativa, string codigoPerfilONS)
        {
            GabaritoConfiguracaoFilter filter = new GabaritoConfiguracaoFilter
            {
                IdAgente = idAgente,
                IdSemanaOperativa = idSemanaOperativa,
                IsPadrao = !idSemanaOperativa.HasValue,
                IdOrigemColetaPai = idUsina,
                TipoInsumo = char.ToString((char)TipoInsumoEnum.Estruturado),
                CodigoPerfilONS = codigoPerfilONS,
                IsNullCodigoPerfilONS = string.IsNullOrWhiteSpace(codigoPerfilONS)
            };

            DadosManutencaoGabaritoDTO dadosManutencaoGabarito = new DadosManutencaoGabaritoDTO();

            IList<Gabarito> gabaritos = gabaritoRepository.ConsultarPorGabaritoFilter(filter);
            if (gabaritos.Any())
            {
                Usina usina = origemColetaService.ObterOrigemColetaPorChave<Usina>(idUsina);

                IList<Insumo> insumos = ConsultarInsumosEstruturados(TipoOrigemColetaEnum.UnidadeGeradora, idUsina)
                    .ToList<Insumo>();

                PreencherDadosManutencaoGabarito(dadosManutencaoGabarito, gabaritos, insumos);

                IList<OrigemColeta> origensColeta = origemColetaService
                    .ConsultarUnidadeGeradoraPorUsinaOnline(idUsina)
                    .ToList<OrigemColeta>();

                IList<OrigemColeta> origensColetaGabarito = gabaritos
                    .Select(gabarito => gabarito.OrigemColeta)
                    .Distinct()
                    .ToList();

                IList<OrigemColeta> origensColetaExcept = origensColeta.Except(origensColetaGabarito, new OrigemColetaComparer()).ToList();

                dadosManutencaoGabarito.OrigemColeta = new ChaveDescricaoDTO<string>(usina.Id, usina.Nome);
                dadosManutencaoGabarito.OrigensColetaGabarito = origensColetaGabarito
                    .Select(origemColeta => new ChaveDescricaoDTO<string>(origemColeta.Id, origemColeta.Nome))
                    .ToList();
                dadosManutencaoGabarito.OrigensColeta = origensColetaExcept
                    .Select(origemColeta => new ChaveDescricaoDTO<string>(origemColeta.Id, origemColeta.Nome))
                    .ToList();
            }

            return dadosManutencaoGabarito;
        }

        private DadosManutencaoGabaritoDTO ObterDadosManutencaoGabaritoGeracaoComplementar(int idAgente,
            int? idSemanaOperativa, string codigoPerfilONS)
        {
            GabaritoConfiguracaoFilter filter = new GabaritoConfiguracaoFilter();
            filter.IdAgente = idAgente;
            filter.IdSemanaOperativa = idSemanaOperativa;
            filter.IsPadrao = !idSemanaOperativa.HasValue;
            filter.IsOrigemColetaNull = true;
            filter.TipoInsumo = char.ToString((char)TipoInsumoEnum.Estruturado);
            filter.CodigoPerfilONS = codigoPerfilONS;
            filter.IsNullCodigoPerfilONS = string.IsNullOrWhiteSpace(codigoPerfilONS);

            DadosManutencaoGabaritoDTO dadosManutencaoGabarito = new DadosManutencaoGabaritoDTO();

            IList<Gabarito> gabaritos = gabaritoRepository.ConsultarPorGabaritoFilter(filter);
            if (gabaritos.Any())
            {
                IList<Insumo> insumos =
                    ConsultarInsumosEstruturados(TipoOrigemColetaEnum.GeracaoComplementar).ToList<Insumo>();
                PreencherDadosManutencaoGabarito(dadosManutencaoGabarito, gabaritos, insumos);
            }

            return dadosManutencaoGabarito;
        }

        private DadosManutencaoGabaritoDTO ObterDadosManutencaoGabaritosNaoEstruturado(int idAgente,
           int? idSemanaOperativa, string codigoPerfilONS)
        {
            GabaritoConfiguracaoFilter filter = new GabaritoConfiguracaoFilter();
            filter.IdAgente = idAgente;
            filter.IdSemanaOperativa = idSemanaOperativa;
            filter.IsPadrao = !idSemanaOperativa.HasValue;
            filter.TipoInsumo = char.ToString((char)TipoInsumoEnum.NaoEstruturado);
            filter.CodigoPerfilONS = codigoPerfilONS;
            filter.IsNullCodigoPerfilONS = string.IsNullOrWhiteSpace(codigoPerfilONS);

            DadosManutencaoGabaritoDTO dadosManutencaoGabarito = new DadosManutencaoGabaritoDTO();

            IList<Gabarito> gabaritos = gabaritoRepository.ConsultarPorGabaritoFilter(filter);
            if (gabaritos.Any())
            {
                IList<Insumo> insumos = insumoService.ConsultarInsumoNaoEstruturado().ToList<Insumo>();
                PreencherDadosManutencaoGabarito(dadosManutencaoGabarito, gabaritos, insumos);
            }

            return dadosManutencaoGabarito;
        }

        private DadosManutencaoGabaritoDTO ObterDadosManutencaoGabaritoReservatorioUsinaSubsistema(int idAgente,
            string idOrigemColeta, int? idSemanaOperativa, string codigoPerfilONS)
        {
            GabaritoConfiguracaoFilter filter = new GabaritoConfiguracaoFilter();
            filter.IdAgente = idAgente;
            filter.IdSemanaOperativa = idSemanaOperativa;
            filter.IsPadrao = !idSemanaOperativa.HasValue;
            filter.IdsOrigemColeta = new List<string> { idOrigemColeta };
            filter.TipoInsumo = char.ToString((char)TipoInsumoEnum.Estruturado);
            filter.CodigoPerfilONS = codigoPerfilONS;
            filter.IsNullCodigoPerfilONS = string.IsNullOrWhiteSpace(codigoPerfilONS);

            DadosManutencaoGabaritoDTO dadosManutencaoGabarito = new DadosManutencaoGabaritoDTO();

            IList<Gabarito> gabaritos = gabaritoRepository.ConsultarPorGabaritoFilter(filter);
            if (gabaritos.Any())
            {
                OrigemColeta origemColeta = gabaritos.First().OrigemColeta;
                TipoOrigemColetaEnum tipoOrigemColeta = origemColeta.TipoOrigemColeta;

                IList<Insumo> insumos = ConsultarInsumosEstruturados(tipoOrigemColeta, idOrigemColeta).ToList<Insumo>();
                PreencherDadosManutencaoGabarito(dadosManutencaoGabarito, gabaritos, insumos);

                dadosManutencaoGabarito.OrigemColeta = new ChaveDescricaoDTO<string>(origemColeta.Id, origemColeta.Nome);
            }

            return dadosManutencaoGabarito;
        }

        private void PreencherDadosManutencaoGabarito(DadosManutencaoGabaritoDTO dadosManutencaoGabarito,
            IList<Gabarito> gabaritos, IList<Insumo> insumos)
        {
            dadosManutencaoGabarito.CodigoPerfilONS = gabaritos.First().CodigoPerfilONS;

            Agente agente = gabaritos.First().Agente;
            SemanaOperativa semanaOperativa = gabaritos.First().SemanaOperativa;

            IList<Insumo> insumosGabarito = gabaritos.Select(gabarito => gabarito.Insumo).Distinct().ToList();
            IList<Insumo> insumosExcept = insumos.Except(insumosGabarito).ToList();

            if (semanaOperativa != null)
            {
                dadosManutencaoGabarito.SemanaOperativa = new ChaveDescricaoDTO<int>(
                    semanaOperativa.Id, semanaOperativa.Nome);
            }

            dadosManutencaoGabarito.Agente = new ChaveDescricaoDTO<int>(agente.Id, agente.Nome);
            dadosManutencaoGabarito.InsumosGabarito =
                insumosGabarito.Select(insumo => new ChaveDescricaoDTO<int>(insumo.Id, insumo.Nome))
                    .ToList();
            dadosManutencaoGabarito.Insumos = insumosExcept
                .Select(insumo => new ChaveDescricaoDTO<int>(insumo.Id, insumo.Nome))
                .ToList();
        }
        #endregion

        private IList<InsumoEstruturado> ConsultarInsumosEstruturados(TipoOrigemColetaEnum tipoOrigemColeta,
            string idUsina = null)
        {
            CategoriaInsumoEnum? categoria = null;

            if (tipoOrigemColeta == TipoOrigemColetaEnum.Reservatorio)
            {
                categoria = CategoriaInsumoEnum.Hidreletrico;
            }
            else if (tipoOrigemColeta == TipoOrigemColetaEnum.Usina ||
                     tipoOrigemColeta == TipoOrigemColetaEnum.UnidadeGeradora)
            {
                if (!string.IsNullOrEmpty(idUsina))
                {
                    Usina usina = origemColetaService.ObterOrigemColetaPorChaveOnline<Usina>(idUsina);
                    if (usina != null)
                    {
                        categoria = usina.TipoUsina == TipoUsinaEnum.Hidraulica.ToDescription()
                            ? CategoriaInsumoEnum.Hidreletrico
                            : CategoriaInsumoEnum.Termico;
                    }
                }
                else
                {
                    return new List<InsumoEstruturado>();
                }
            }

            return insumoService.ConsultarInsumoPorTipoOrigemColetaCategoria(tipoOrigemColeta, categoria);
        }


    }
}
