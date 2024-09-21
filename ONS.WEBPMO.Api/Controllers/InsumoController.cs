using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using ONS.Common.Exceptions;
using ONS.Common.Seguranca;

using ONS.SGIPMO.Domain.Entities;
using ONS.SGIPMO.Domain.Entities.DTO;
using ONS.SGIPMO.Domain.Entities.Filters;
using ONS.SGIPMO.Domain.Presentations;
using ONS.SGIPMO.Domain.Services;
using ONS.SGIPMO.WebSite.Models;
using ONS.Common.Util;

namespace ONS.WEBPMO.Api.Controllers
{
    using Common.Util.Pagination;

    using ONS.Common.Entities;

    [WebPermission("ConfigurarInsumo")]
    public class InsumoController : ControllerBase
    {
        private readonly IInsumoService insumoService;
        private readonly IInsumoPresentation insumoPresentation;

        public InsumoController(
            IInsumoService insumoService,
            IInsumoPresentation insumoPresentation)
        {
            this.insumoService = insumoService;
            this.insumoPresentation = insumoPresentation;
        }

        #region Index

        public ActionResult Index()
        {
            InsumoConsultaModel model = new InsumoConsultaModel();
            CarregarInsumoConsulta(model);
            model.IsPrimeiroCarregamentoGrid = true;

            return View(model);
        }

        public ActionResult CarregarPesquisaInsumo(InsumoConsultaModel model)
        {
            CarregarInsumoConsulta(model);
            return View("Index", model);
        }

        #endregion

        #region Pesquisa Insumo

        public ActionResult PesquisaInsumo(InsumoConsultaModel model)
        {
            return PartialView("_PesquisaInsumo", model);
        }

        #endregion

        #region Carregamento dos Grids Ajax (JqGrid)
        public ActionResult CarregarGridInsumos(GridSettings gridSettings, InsumoConsultaModel model)
        {
            PagedResult<InsumoListagemModel> resultadoPaginado = null;
            if (ModelStateHandleValid)
            {
                InsumoFiltro filtro = Mapper.DynamicMap<InsumoFiltro>(model);
                Mapper.DynamicMap(gridSettings, filtro);

                PagedResult<Insumo> result = insumoService.ConsultarInsumosPorFiltro(filtro);
                resultadoPaginado = Mapper.DynamicMap<PagedResult<InsumoListagemModel>>(result);
            }

            return JsonToPagedGrid(resultadoPaginado, gridSettings.PageIndex);
        }

        #endregion

        #region Alterar Insumo

        public ActionResult AlterarInsumo(int idInsumo, string tipoInsumoAlterar, InsumoConsultaModel model)
        {
            SetViewError("Index", model, CarregarInsumoConsulta);

            insumoService.VerificarInsumoReservado(idInsumo);

            string actionName;
            if (tipoInsumoAlterar == TipoInsumoEnum.Estruturado.ToChar())
            {
                actionName = "CarregarAlterarInsumoEstruturado";
            }
            else
            {
                actionName = "CarregarAlterarInsumoNaoEstruturado";
            }
            return RedirectToAction(actionName, new { id = idInsumo });
        }

        public ActionResult CarregarAlterarInsumoNaoEstruturado(int id)
        {
            InsumoNaoEstruturado insumo = insumoService.ObterInsumoNaoEstruturadoPorChave(id);
            ConfiguracaoInsumoNaoEstruturado model = Mapper.DynamicMap<ConfiguracaoInsumoNaoEstruturado>(insumo);
            return View("AlterarInsumoNaoEstruturado", model);
        }

        public ActionResult CarregarAlterarInsumoEstruturado(int id)
        {
            DadosManutencaoInsumoEstruturado dto = insumoPresentation.ObterDadosManutencaoInsumoEstruturado(id);
            ManutencaoInsumoEstruturadoModel model = Mapper.DynamicMap<ManutencaoInsumoEstruturadoModel>(dto);

            return View("AlterarInsumoEstruturado", model);
        }

        [HttpPost]
        [Auditoria("Configurar Insumo", "Alterar Insumo", typeof(InsumoNaoEstruturado))]
        public ActionResult AlterarInsumoNaoEstruturado(ConfiguracaoInsumoNaoEstruturado model)
        {
            SetViewError("AlterarInsumoNaoEstruturado", model);
            if (ModelStateHandleValid)
            {
                InsumoNaoEstruturado insumo = Mapper.Map<InsumoNaoEstruturado>(model);
                insumoService.AlterarInsumoNaoEstruturado(insumo, Convert.FromBase64String(model.VersaoInsumoString));
                DisplaySuccessMessage(SGIPMOMessages.MS013);
                return RedirectToAction("CarregarAlterarInsumoNaoEstruturado", new { id = insumo.Id });
            }
            throw new ONSBusinessException();
        }

        [HttpPost]
        [Auditoria("Configurar Insumo", "Alterar Insumo", typeof(InsumoEstruturado))]
        public ActionResult AlterarInsumoEstruturado(ManutencaoInsumoEstruturadoModel model)
        {
            if (ModelStateHandleValid)
            {
                DadosInclusaoInsumoEstruturadoDTO insumo = Mapper.DynamicMap<DadosInclusaoInsumoEstruturadoDTO>(model);
                insumoService.AlterarInsumoEstruturado(insumo);
            }

            return AjaxSuccessResult(SGIPMOMessages.MS013, true);
        }

        #endregion

        #region Incluir Insumo

        public ActionResult CarregarIncluirInsumo()
        {
            InclusaoInsumoModelBase modelBase = new InclusaoInsumoModelBase();
            return PartialView("_IncluirInsumo", modelBase);
        }

        [HttpPost]
        public ActionResult IncluirInsumo(InclusaoInsumoModelBase modelBase)
        {
            if (modelBase.TipoInsumoNome.Equals(TipoInsumoEnum.Estruturado.ToDescription()))
            {
                return RedirectToAction("CarregarIncluirInsumoEstruturado");
            }
            return RedirectToAction("CarregarIncluirInsumoNaoEstruturado");
        }

        public ActionResult CarregarIncluirInsumoEstruturado()
        {
            DadosManutencaoInsumoEstruturado dto = insumoPresentation.ObterDadosManutencaoInsumoEstruturado(null);

            ManutencaoInsumoEstruturadoModel model = Mapper.DynamicMap<ManutencaoInsumoEstruturadoModel>(dto);

            return View("IncluirInsumoEstruturado", model);
        }

        public ActionResult CarregarIncluirInsumoNaoEstruturado()
        {
            ConfiguracaoInsumoNaoEstruturado model = new ConfiguracaoInsumoNaoEstruturado();

            return View("IncluirInsumoNaoEstruturado", model);
        }

        [HttpPost]
        [Auditoria("Configurar Insumo", "Incluir Insumo", typeof(InsumoEstruturado))]
        public ActionResult InserirInsumoEstruturado(ManutencaoInsumoEstruturadoModel model)
        {
            int idInsumo = 0;

            if (ModelStateHandleValid)
            {
                DadosInclusaoInsumoEstruturadoDTO insumo = Mapper.DynamicMap<DadosInclusaoInsumoEstruturadoDTO>(model);
                idInsumo = insumoService.InserirInsumoEstruturado(insumo);
            }

            return AjaxSuccessResult(SGIPMOMessages.MS013,
                Url.Action("CarregarAlterarInsumoEstruturado", new { id = idInsumo }));
        }

        [HttpPost]
        [Auditoria("Configurar Insumo", "Incluir Insumo", typeof(InsumoNaoEstruturado))]
        public ActionResult InserirInsumoNaoEstruturado(ConfiguracaoInsumoNaoEstruturado model)
        {
            SetViewError("IncluirInsumoNaoEstruturado", model);

            if (ModelState.IsValid)
            {
                InsumoNaoEstruturado insumo = Mapper.Map<InsumoNaoEstruturado>(model);

                int idInsumo = insumoService.InserirInsumoNaoEstruturado(insumo);
                DisplaySuccessMessage(SGIPMOMessages.MS013);

                return RedirectToAction("CarregarAlterarInsumoNaoEstruturado", new { id = idInsumo });
            }
            throw new ONSBusinessException();
        }

        #endregion

        #region Excluir Insumo

        [HttpPost]
        [Auditoria("Configurar Insumo", "Excluir Insumo", typeof(Insumo))]
        public ActionResult ExcluirInsumo(ExclusaoInsumoModel model)
        {
            insumoService.ExcluirInsumoPorChave(model.IdInsumo, Convert.FromBase64String(model.VersaoStringInsumo));
            return AjaxSuccessResult(SGIPMOMessages.MS013, ReturnUrl());
        }

        #endregion

        #region Visualizar Insumo

        public ActionResult VisualizarInsumo(int idInsumo, string tipoInsumoVisualizar)
        {
            string viewName = "";
            Insumo insumo = null;
            VisualizarInsumoModel model = null;

            if (tipoInsumoVisualizar == TipoInsumoEnum.NaoEstruturado.ToChar())
            {
                insumo = insumoService.ObterInsumoNaoEstruturadoPorChave(idInsumo);
                model = Mapper.DynamicMap<VisualizarInsumoNaoEstruturadoModel>(insumo);
                viewName = "VisualizarInsumoNaoEstruturado";
            }
            else
            {
                insumo = insumoService.ObterInsumoEstruturadoPorChave(idInsumo);
                model = Mapper.DynamicMap<VisualizarInsumoEstruturadoModel>(insumo);
                viewName = "VisualizarInsumoEstruturado";
            }
            return View(viewName, model);
        }

        #endregion

        #region Carregamento Insumo (SetViewError)

        private void CarregarInsumoConsulta(InsumoConsultaModel model)
        {
            DadosInsumoConsultaDTO dto = insumoPresentation.ObterDadosInsumoConsulta();
            Mapper.DynamicMap(dto, model);
        }

        #endregion

        #region Grandeza

        public ActionResult CarregarIncluirGrandeza(int idInsumo)
        {
            var tiposDado = insumoService.ObterTiposDadoGrandeza()
                .Select(t => new SelectListItem { Text = t.Descricao, Value = t.Id.ToString() });
            ManutencaoGrandezaModel model = new ManutencaoGrandezaModel
            {
                InsumoId = idInsumo,
                TiposDado = tiposDado.ToList(),
                Ativo = true
            };
            return PartialView("_IncluirGrandeza", model);
        }

        public ActionResult CarregarAlterarGrandeza(ManutencaoGrandezaModel model)
        {
            TipoDadoGrandeza tipoDado = insumoService.ObterTiposDadoGrandeza()
                .FirstOrDefault(tipo => tipo.Id == model.TipoDadoGrandezaId);

            var tiposDado = insumoService.ObterTiposDadoGrandeza()
                .Select(t => new SelectListItem { Text = t.Descricao, Value = t.Id.ToString() });
            model.TiposDado = tiposDado.ToList();

            model.TipoDadoGrandezaDescricao = tipoDado == null ? string.Empty : tipoDado.Descricao;
            model.PermiteAlteracao = insumoService.PermitirAlteracaoGrandeza(model.Id);

            return PartialView("_AlterarGrandeza", model);
        }

        [HttpPost]
        public ActionResult ValidarInclusaoGrandeza(ManutencaoGrandezaModel model)
        {
            model.TipoDadoGrandezaDescricao = ((TipoDadoGrandezaEnum)model.TipoDadoGrandezaId).ToDescription();

            if (ModelStateHandleValid)
            {
                Grandeza grandeza = Mapper.DynamicMap<Grandeza>(model);
                insumoService.ValidarIncluirAlterarGrandeza(grandeza);
            }
            return Json(model);
        }

        [HttpPost]
        public ActionResult ValidarAlteracaoGrandeza(ManutencaoGrandezaModel model)
        {
            if (ModelStateHandleValid)
            {
                Grandeza grandeza = Mapper.DynamicMap<Grandeza>(model);
                insumoService.ValidarIncluirAlterarGrandeza(grandeza);
            }
            return Json(model);
        }

        [HttpPost]
        public ActionResult ValidarExclusaoGrandeza(ManutencaoGrandezaModel model)
        {
            Grandeza grandeza = Mapper.DynamicMap<Grandeza>(model);
            if (model.Id > 0)
            {
                insumoService.ValidarExclusaoGrandeza(grandeza.Id);
            }
            return Json(model);
        }

        public ActionResult CarregarVisualizarGrandeza(ManutencaoGrandezaModel model)
        {
            return PartialView("_VisualizarGrandeza", model);
        }

        public ActionResult CarregarGridGrandezas(GridSettings gridSettings, int idInsumo)
        {
            PagedResult<ManutencaoGrandezaModel> resultadoPaginado =
                new PagedResult<ManutencaoGrandezaModel>(new List<ManutencaoGrandezaModel>(), 0, 0, 0);

            if (ModelStateHandleValid && idInsumo > 0)
            {
                IList<Grandeza> grandezas = insumoService.ObterGrandezasPorInsumo(idInsumo);
                IList<ManutencaoGrandezaModel> grandezasModel = grandezas.Select(
                    Mapper.DynamicMap<ManutencaoGrandezaModel>).ToList();
                resultadoPaginado = new PagedResult<ManutencaoGrandezaModel>(
                    grandezasModel, grandezasModel.Count, gridSettings.PageIndex, gridSettings.PageSize);
            }

            return JsonToPagedGrid(resultadoPaginado, gridSettings.PageIndex);
        }

        #endregion
    }
}
