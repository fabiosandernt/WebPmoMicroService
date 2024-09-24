using AutoMapper;

namespace ONS.WEBPMO.Api.Controllers
{
    using Domain.Entities.Filters;
    using Domain.Presentations;
    using Microsoft.AspNetCore.Mvc;
    using ONS.WEBPMO.Application.Services.PMO.Interfaces;
    using ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta;
    using ONS.WEBPMO.Domain.Entities.Base;
    using ONS.WEBPMO.Domain.Entities.PMO;
    using ONS.WEBPMO.Domain.Enumerations;
    using System.Collections.Generic;
    using System.Linq;

    //[WebPermission("ConfigurarGabarito")]
    public class GabaritoController : ControllerBase
    {
        private readonly IGabaritoService gabaritoService;
        private readonly IGabaritoPresentation gabaritoPresentation;
        private readonly IInsumoService insumoService;
        private readonly ISemanaOperativaService semanaOperativaService;
        private readonly IAgenteService agenteService;
        private readonly IOrigemColetaService origemColetaService;

        public GabaritoController(
            IGabaritoService gabaritoService,
            IGabaritoPresentation gabaritoPresentation,
            IInsumoService insumoService,
            ISemanaOperativaService semanaOperativaService,
            IAgenteService agenteService,
            IOrigemColetaService origemColetaService)
        {
            this.gabaritoService = gabaritoService;
            this.gabaritoPresentation = gabaritoPresentation;
            this.insumoService = insumoService;
            this.semanaOperativaService = semanaOperativaService;
            this.agenteService = agenteService;
            this.origemColetaService = origemColetaService;
        }

        public ActionResult Index(GabaritoConsultaModel gabaritoModel)
        {
            DadosFiltroPesquisaGabaritoDTO dadosFiltro = gabaritoPresentation.ObterDadosFiltroPesquisaGabarito(
                gabaritoModel.IdInsumo, gabaritoModel.IdAgente, gabaritoModel.IdSemanaOperativa);
            GabaritoConsultaModel model = Mapper.Map<GabaritoConsultaModel>(dadosFiltro);

            if (gabaritoModel.IsPadrao || gabaritoModel.IdSemanaOperativa.HasValue)
            {
                gabaritoModel.Agentes = model.Agentes;
                gabaritoModel.Insumos = model.Insumos;
                gabaritoModel.SemanasOperativas = model.SemanasOperativas;
                Mapper.DynamicMap(gabaritoModel, model);
            }
            else
            {
                model.IsPadrao = true;
            }

            return View(model);
        }

        #region Pesquisa Gabarito

        [HttpPost]
        public ActionResult PesquisaGabarito(GabaritoConsultaModel model)
        {
            if (model.IsPadrao)
            {
                model.NomeGabarito = "Gabarito - Padrão";
            }
            else
            {
                if (model.IdSemanaOperativa.HasValue)
                {
                    SemanaOperativa semanaOperativa =
                        semanaOperativaService.ObterSemanaOperativaPorChave(model.IdSemanaOperativa.Value);
                    model.SituacaoSemanaOperativa = (SituacaoSemanaOperativaEnum)semanaOperativa.Situacao.Id;
                    model.NomeGabarito = string.Format("Gabarito - {0} - {1}", semanaOperativa.Nome,
                        model.SituacaoSemanaOperativa.ToDescription());
                }
                else
                {
                    throw new ONSBusinessException(string.Format(SGIPMOMessages.MS001, "Estudo"));
                }
            }

            model.TipoOrigemColeta = TipoOrigemColetaEnum.Usina;

            return PartialView("_PesquisaGabarito", model);
        }

        public ActionResult PesquisarAgentesPorNome(string term)
        {
            var agentes = agenteService.ConsultarAgentesPorNome(term);
            return Json(agentes.Select(s => new { Descricao = s.Nome, Chave = s.Id }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PesquisarAgentesPorNomeOnline(string term)
        {
            var agentes = agenteService.ConsultarAgentesPorNomeOnline(term);
            return Json(agentes.Select(s => new { Descricao = s.Nome, Chave = s.Id }), JsonRequestBehavior.AllowGet);
        }


        public ActionResult PesquisarReservatoriosPorNome(string term)
        {
            var origensColeta = origemColetaService.ConsultarOrigemColetaPorTipoNomeOnline(
                TipoOrigemColetaEnum.Reservatorio, term);
            return Json(origensColeta.Select(s => new { Descricao = s.Nome, Chave = s.Id }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PesquisarSubsistemaPorNome(string term)
        {
            var origensColeta = origemColetaService.ConsultarOrigemColetaPorTipoNomeOnline(
                TipoOrigemColetaEnum.Subsistema, term);
            return Json(origensColeta.Select(s => new { Descricao = s.Nome, Chave = s.Id }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PesquisarUsinaPorNome(string term)
        {
            var origensColeta = origemColetaService.ConsultarOrigemColetaPorTipoNomeOnline(
                TipoOrigemColetaEnum.Usina, term);
            return Json(origensColeta.Select(s => new { Descricao = s.Nome, Chave = s.Id }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PesquisarInsumosPorNome(string term)
        {
            var insumos = insumoService.ConsultarInsumosPorNome(term);
            return Json(insumos.Select(s => new { Descricao = s.Nome, Chave = s.Id }), JsonRequestBehavior.AllowGet);
        }

        #region Pesquisa Configuração

        public ActionResult PesquisaGabaritoConfiguracao(GabaritoConsultaModel model)
        {
            return PartialView("_PesquisaGabaritoConfiguracao", model);
        }

        public ActionResult PesquisaGabaritoPorOrigemColeta(GabaritoConsultaModel model)
        {
            return PartialView("_ListaConfiguracaoOrigemColeta", model);
        }

        public ActionResult PesquisaGabaritoNaoEstruturado(GabaritoConsultaModel model)
        {
            return PartialView("_ListaConfiguracaoNaoEstruturado", model);
        }




        #endregion

        #region Carregamento dos Grids Ajax (JqGrid)
        public ActionResult CarregarGridGabaritosPorAgenteOrigemColeta(GridSettings gridSettings, GabaritoConsultaModel model)
        {
            PagedResult<GabaritoAgrupadoAgenteOrigemColetaDTO> result = null;
            if (ModelStateHandleValid)
            {
                if (!model.IsPadrao && !model.IdSemanaOperativa.HasValue)
                {
                    throw new ONSBusinessException("Escolha um estudo para realizar a pesquisa.");
                }
                GabaritoOrigemColetaFilter filter = Mapper.DynamicMap<GabaritoOrigemColetaFilter>(model);

                Mapper.DynamicMap(gridSettings, filter);

                result = gabaritoService.ConsultarGabaritosAgrupadoPorAgenteTipoOrigemPaginado(filter);
            }

            return JsonToPagedGrid(result, gridSettings.PageIndex);
        }

        #endregion


        #endregion

        #region Salvar Gabarito

        public ActionResult CarregarSalvarGabarito(GabaritoConsultaModel gabaritoConsultaModel)
        {
            BaseGabaritoModel model = null;
            string nomeView;

            if (gabaritoConsultaModel.TipoInsumo == TipoInsumoEnum.Estruturado)
            {
                TipoOrigemColetaEnum tipoOrigemColeta = gabaritoConsultaModel.TipoOrigemColeta.Value;
                switch (tipoOrigemColeta)
                {
                    case TipoOrigemColetaEnum.Usina:
                        model = new ConfiguracaoGabaritoUsinaModel();
                        break;
                    case TipoOrigemColetaEnum.Subsistema:
                        model = new ConfiguracaoGabaritoSubsistemaModel();
                        break;
                    case TipoOrigemColetaEnum.UnidadeGeradora:
                        model = new ConfiguracaoGabaritoUnidadeGeradoraModel();
                        break;
                    case TipoOrigemColetaEnum.Reservatorio:
                        model = new ConfiguracaoGabaritoReservatorioModel();
                        break;
                    case TipoOrigemColetaEnum.GeracaoComplementar:
                        model = new ConfiguracaoGabaritoGeracaoComplementarModel();
                        break;
                    default:
                        model = new BaseGabaritoModel();
                        break;
                }

                nomeView = string.Format("SalvarGabarito{0}", tipoOrigemColeta);
            }
            else
            {
                model = new ConfiguracaoGabaritoNaoEstruturadoModel();
                nomeView = "SalvarGabaritoNaoEstruturado";
            }

            Mapper.DynamicMap(gabaritoConsultaModel, model);
            CarregarConfiguracaoGabaritoModel(model);

            return View(nomeView, model);
        }

        [HttpPost]
        [Auditoria("Configurar Gabarito", "Salvar Gabarito", typeof(Gabarito))]
        public ActionResult SalvarGabaritoReservatorio(ConfiguracaoGabaritoReservatorioModel model)
        {
            SetViewError("SalvarGabaritoReservatorio", model, CarregarConfiguracaoGabaritoModel);
            return ConfigurarGabarito(model);
        }

        [HttpPost]
        [Auditoria("Configurar Gabarito", "Salvar Gabarito", typeof(Gabarito))]
        public ActionResult SalvarGabaritoUsina(ConfiguracaoGabaritoUsinaModel model)
        {
            SetViewError("SalvarGabaritoUsina", model, CarregarConfiguracaoGabaritoModel);
            return ConfigurarGabarito(model);
        }

        [HttpPost]
        [Auditoria("Configurar Gabarito", "Salvar Gabarito", typeof(Gabarito))]
        public ActionResult SalvarGabaritoSubsistema(ConfiguracaoGabaritoSubsistemaModel model)
        {
            SetViewError("SalvarGabaritoSubsistema", model, CarregarConfiguracaoGabaritoModel);
            return ConfigurarGabarito(model);
        }

        [HttpPost]
        [Auditoria("Configurar Gabarito", "Salvar Gabarito", typeof(Gabarito))]
        public ActionResult SalvarGabaritoGeracaoComplementar(ConfiguracaoGabaritoGeracaoComplementarModel model)
        {
            SetViewError("SalvarGabaritoGeracaoComplementar", model, CarregarConfiguracaoGabaritoModel);
            return ConfigurarGabarito(model);
        }

        [HttpPost]
        [Auditoria("Configurar Gabarito", "Salvar Gabarito", typeof(Gabarito))]
        public ActionResult SalvarGabaritoNaoEstruturado(ConfiguracaoGabaritoNaoEstruturadoModel model)
        {
            SetViewError("SalvarGabaritoNaoEstruturado", model, CarregarConfiguracaoGabaritoModel);
            return ConfigurarGabarito(model);
        }

        [HttpPost]
        [Auditoria("Configurar Gabarito", "Salvar Gabarito", typeof(Gabarito))]
        public ActionResult SalvarGabaritoUnidadeGeradora(ConfiguracaoGabaritoUnidadeGeradoraModel model)
        {
            SetViewError("SalvarGabaritoUnidadeGeradora", model, CarregarConfiguracaoGabaritoUnidadeGeradoraModel);
            return ConfigurarGabarito(model);
        }

        private ActionResult ConfigurarGabarito(BaseGabaritoModel model)
        {
            if (ModelStateHandleValid)
            {
                if (model.IdsInsumo.Any())
                {
                    GabaritoConfiguracaoDTO dto = Mapper.DynamicMap<GabaritoConfiguracaoDTO>(model);
                    gabaritoService.SalvarGabarito(dto);
                    DisplaySuccessMessage(SGIPMOMessages.MS013);

                    return RedirectToAction("CarregarAlterarGabarito", new GabaritoConsultaModel
                    {
                        IdAgente = dto.IdAgente,
                        IdSemanaOperativa = dto.IdSemanaOperativa,
                        TipoOrigemColeta = dto.TipoOrigemColeta,
                        TipoInsumo = dto.TipoInsumo,
                        CodigoPerfilONS = dto.CodigoPerfilONS,
                        IdOrigemColeta = dto.TipoOrigemColeta == TipoOrigemColetaEnum.UnidadeGeradora
                            ? dto.IdOrigemColetaPai
                            : dto.IdsOrigemColeta.FirstOrDefault()
                    });
                }
            }

            throw new ONSBusinessException();
        }

        #region Carregar Configuração Gabarito
        private void CarregarConfiguracaoGabaritoUnidadeGeradoraModel(ConfiguracaoGabaritoUnidadeGeradoraModel model)
        {
            GabaritoDadosFilter filter = Mapper.DynamicMap<GabaritoDadosFilter>(model);

            DadosConfiguracaoGabaritoUnidadeGeradoraDTO dados = gabaritoPresentation
                .ObterDadosConfiguracaoGabaritoUnidadeGeradora(filter);

            Mapper.DynamicMap(dados, model);

            model.InsumosGabarito = model.Insumos
                .Where(insumo => model.IdsInsumo.Any(idInsumo => idInsumo == int.Parse(insumo.Value)))
                .ToList();
            model.Insumos = model.Insumos
                .Where(insumo => model.IdsInsumo.All(idInsumo => idInsumo != int.Parse(insumo.Value)))
                .ToList();

            model.OrigensColetaGabarito = model.OrigensColeta
                .Where(origemColeta => model.IdsOrigemColeta.Any(idOrigem => idOrigem == origemColeta.Value))
                .ToList();
            model.OrigensColeta = model.OrigensColeta
                .Where(origemColeta => model.IdsOrigemColeta.All(idOrigem => idOrigem != origemColeta.Value))
                .ToList();
        }

        private void CarregarConfiguracaoGabaritoModel(BaseGabaritoModel model)
        {
            GabaritoDadosFilter filter = Mapper.DynamicMap<GabaritoDadosFilter>(model);

            DadosConfiguracaoGabaritoDTO dados = gabaritoPresentation.ObterDadosConfiguracaoGabarito(filter);

            Mapper.DynamicMap(dados, model);

            model.InsumosGabarito = model.Insumos
                .Where(insumo => model.IdsInsumo.Any(idInsumo => idInsumo == int.Parse(insumo.Value)))
                .ToList();

            model.Insumos = model.Insumos
                .Where(insumo => model.IdsInsumo.All(idInsumo => idInsumo != int.Parse(insumo.Value)))
                .ToList();
        }
        #endregion

        #endregion

        #region Alterar Gabarito
        public ActionResult CarregarAlterarGabarito(GabaritoConsultaModel gabaritoConsultaModel)
        {
            ManutencaoGabaritoModel model = null;

            string nomeView;

            if (gabaritoConsultaModel.TipoInsumo == TipoInsumoEnum.Estruturado)
            {
                switch (gabaritoConsultaModel.TipoOrigemColeta)
                {
                    case TipoOrigemColetaEnum.Usina:
                        model = new ManutencaoGabaritoUsinaModel();
                        break;
                    case TipoOrigemColetaEnum.Subsistema:
                        model = new ManutencaoGabaritoSubsistemaModel();
                        break;
                    case TipoOrigemColetaEnum.UnidadeGeradora:
                        model = new ManutencaoGabaritoUnidadeGeradoraModel();
                        break;
                    case TipoOrigemColetaEnum.Reservatorio:
                        model = new ManutencaoGabaritoReservatorioModel();
                        break;
                    case TipoOrigemColetaEnum.GeracaoComplementar:
                        model = new ManutencaoGabaritoGeracaoComplementarModel();
                        break;
                    default:
                        model = new ManutencaoGabaritoModel();
                        break;
                }

                model.TipoOrigemColeta = gabaritoConsultaModel.TipoOrigemColeta
                    ?? TipoOrigemColetaEnum.GeracaoComplementar;
                nomeView = string.Format("AlterarGabarito{0}", model.TipoOrigemColeta);
            }
            else
            {
                model = new ManutencaoGabaritoNaoEstruturadoModel();
                nomeView = "AlterarGabaritoNaoEstruturado";
            }

            model.TipoInsumo = gabaritoConsultaModel.TipoInsumo;
            model.IsPadrao = (gabaritoConsultaModel.IdSemanaOperativa == null);
            model.IdSemanaOperativa = gabaritoConsultaModel.IdSemanaOperativa;
            model.IdOrigemColeta = gabaritoConsultaModel.IdOrigemColeta;
            model.IdAgente = gabaritoConsultaModel.IdAgente;
            model.CodigoPerfilONS = gabaritoConsultaModel.CodigoPerfilONS;

            CarregarManutencaoGabaritoModel(model);
            return View(nomeView, model);
        }

        [HttpPost]
        [Auditoria("Configurar Gabarito", "Alterar Gabarito", typeof(Gabarito))]
        public ActionResult AlterarGabaritoReservatorio(ManutencaoGabaritoReservatorioModel model)
        {
            SetViewError("AlterarGabaritoReservatorio", model, CarregarManutencaoGabaritoModel);
            return ManterGabarito(model);
        }

        [HttpPost]
        [Auditoria("Configurar Gabarito", "Alterar Gabarito", typeof(Gabarito))]
        public ActionResult AlterarGabaritoUsina(ManutencaoGabaritoUsinaModel model)
        {
            SetViewError("AlterarGabaritoUsina", model, CarregarManutencaoGabaritoModel);
            return ManterGabarito(model);
        }

        [HttpPost]
        [Auditoria("Configurar Gabarito", "Alterar Gabarito", typeof(Gabarito))]
        public ActionResult AlterarGabaritoUnidadeGeradora(ManutencaoGabaritoUnidadeGeradoraModel model)
        {
            SetViewError("AlterarGabaritoUnidadeGeradora", model, CarregarManutencaoGabaritoUnidadeGeradoraModel);
            return ManterGabarito(model);
        }

        [HttpPost]
        [Auditoria("Configurar Gabarito", "Alterar Gabarito", typeof(Gabarito))]
        public ActionResult AlterarGabaritoSubsistema(ManutencaoGabaritoSubsistemaModel model)
        {
            SetViewError("AlterarGabaritoSubsistema", model, CarregarManutencaoGabaritoModel);
            return ManterGabarito(model);
        }

        [HttpPost]
        [Auditoria("Configurar Gabarito", "Alterar Gabarito", typeof(Gabarito))]
        public ActionResult AlterarGabaritoGeracaoComplementar(ManutencaoGabaritoGeracaoComplementarModel model)
        {
            SetViewError("AlterarGabaritoGeracaoComplementar", model, CarregarManutencaoGabaritoModel);
            return ManterGabarito(model);
        }

        [HttpPost]
        [Auditoria("Configurar Gabarito", "Alterar Gabarito", typeof(Gabarito))]
        public ActionResult AlterarGabaritoNaoEstruturado(ManutencaoGabaritoNaoEstruturadoModel model)
        {
            SetViewError("AlterarGabaritoNaoEstruturado", model, CarregarManutencaoGabaritoModel);
            return ManterGabarito(model);
        }

        private ActionResult ManterGabarito(ManutencaoGabaritoModel model)
        {
            if (ModelStateHandleValid)
            {
                GabaritoConfiguracaoDTO dto = Mapper.DynamicMap<GabaritoConfiguracaoDTO>(model);

                gabaritoService.AlterarGabarito(dto);

                DisplaySuccessMessage(SGIPMOMessages.MS013);

                if (model.IdsInsumo.Any())
                {
                    return RedirectToAction("CarregarAlterarGabarito", new GabaritoConsultaModel
                    {
                        IdAgente = model.IdAgente,
                        IdOrigemColeta = model.IdOrigemColeta,
                        IdSemanaOperativa = model.IdSemanaOperativa,
                        TipoOrigemColeta = model.TipoOrigemColeta,
                        IsPadrao = model.IsPadrao,
                        TipoInsumo = model.TipoInsumo,
                        CodigoPerfilONS = model.CodigoPerfilONS
                    });
                }
            }

            return Redirect(ReturnUrl());
        }

        #region Carregar Manutenção de Gabarito
        private void CarregarManutencaoGabaritoModel(ManutencaoGabaritoModel model)
        {
            GabaritoOrigemColetaFilter filter = Mapper.DynamicMap<GabaritoOrigemColetaFilter>(model);
            DadosManutencaoGabaritoDTO dados = gabaritoPresentation.ObterDadosManutencaoGabarito(filter);
            Mapper.DynamicMap(dados, model);

            IList<SelectListItem> insumos = model.Insumos.ToList();
            foreach (var insumo in insumos)
            {
                if (model.IdsInsumo.Any(idInsumo => idInsumo == int.Parse(insumo.Value)))
                {
                    model.InsumosGabarito.Add(insumo);
                    model.Insumos.Remove(insumo);
                }
            }
        }

        private void CarregarManutencaoGabaritoUnidadeGeradoraModel(ManutencaoGabaritoUnidadeGeradoraModel model)
        {
            GabaritoOrigemColetaFilter filter = Mapper.DynamicMap<GabaritoOrigemColetaFilter>(model);
            DadosManutencaoGabaritoDTO dados = gabaritoPresentation.ObterDadosManutencaoGabarito(filter);
            Mapper.DynamicMap(dados, model);

            IList<SelectListItem> insumos = model.Insumos.ToList();
            foreach (var insumo in insumos)
            {
                if (model.IdsInsumo.Any(idInsumo => idInsumo == int.Parse(insumo.Value)))
                {
                    model.InsumosGabarito.Add(insumo);
                    model.Insumos.Remove(insumo);
                }
            }

            IList<SelectListItem> origensColeta = model.OrigensColeta.ToList();
            foreach (var origemColeta in origensColeta)
            {
                if (model.IdsOrigemColeta.Any(idOrigem => idOrigem == origemColeta.Value))
                {
                    model.OrigensColetaGabarito.Add(origemColeta);
                    model.OrigensColeta.Remove(origemColeta);
                }
            }
        }

        #endregion

        public ActionResult VisualizarGabarito(GabaritoConsultaModel consultaModel)
        {
            GabaritoOrigemColetaFilter filtro = Mapper.DynamicMap<GabaritoOrigemColetaFilter>(consultaModel);
            DadosManutencaoGabaritoDTO dados = gabaritoPresentation.ObterDadosManutencaoGabarito(filtro);

            VisualizarGabaritoModel model = Mapper.DynamicMap<VisualizarGabaritoModel>(dados);
            model.TipoOrigemColeta = filtro.TipoOrigemColeta;

            return View(model);
        }
        #endregion

        public ActionResult ConsultarUnidadesGeradorasInsumos(string idUsina)
        {
            DadosConfiguracaoGabaritoDTO dto =
                gabaritoPresentation.ObterDadosConfiguracaoGabaritoUnidadeGeradoraPorUsina(idUsina);

            return Json(new { UnidadesGeradoras = dto.OrigensColeta, dto.Insumos }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConsultarInsumosPorUsina(string idUsina)
        {
            IList<InsumoEstruturado> insumos = insumoService.ConsultarInsumoEstruturadosPorUsina(idUsina);
            IList<ChaveDescricaoDTO<int>> insumosDTO = insumos
                .Select(insumo => new ChaveDescricaoDTO<int>(insumo.Id, insumo.Nome))
                .ToList();

            return Json(new { Insumos = insumosDTO }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsAgenteOns(int idAgente)
        {
            bool isAgenteONS = agenteService.IsAgenteONS(idAgente);
            return Json(isAgenteONS, JsonRequestBehavior.AllowGet);
        }
    }
}
