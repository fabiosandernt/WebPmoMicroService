using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ONS.WEBPMO.Application.Models.PMO;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Api.Controllers
{
    //[WebPermission("ConfigurarPMO")]
    public class PMOController : ControllerBase
    {
        private readonly IPMOService pmoService;
        private readonly ISemanaOperativaService semanaOperativaService;

        public PMOController(IPMOService pmoService,
            ISemanaOperativaService semanaOperativaService)
        {
            this.pmoService = pmoService;
            this.semanaOperativaService = semanaOperativaService;
        }

        #region Index

        //public ActionResult Index()
        //{
        //    PMOConsultaModel model = new PMOConsultaModel();
        //    return View(model);
        //}

        #endregion

        #region Manter PMO

        //public ActionResult ManterPMO(int id)
        //{
        //    PMO pmo = pmoService.ObterPMOPorChave(id);
        //    PMOManterModel model = RecarregarPMOModel(pmo);
        //    return View("ManterPMO", model);
        //}

        //[HttpPost]
        //[ActionSource("Incluir")]
        //[Auditoria("Configurar PMO", "Incluir PMO", typeof(PMO))]
        //public ActionResult ManterPMO(PMOConsultaModel model)
        //{
        //    SetViewError("Index", model);
        //    if (ModelState.IsValid)
        //    {
        //        PMO pmo = pmoService.GerarPMO(model.Ano.Value, model.Mes.Value);
        //        DisplaySuccessMessage(SGIPMOMessages.MS013);
        //        return RedirectToAction("ManterPMO", new { id = pmo.Id });
        //    }
        //    return View("Index", model);
        //}

        //[HttpPost]
        //[ActionSource("Salvar")]
        //[Auditoria("Configurar PMO", "Alterar PMO", typeof(PMO))]
        //public ActionResult SalvarPMO(PMOManterModel model)
        //{
        //    SetViewError("ManterPMO", model, RecarregarModel);
        //    if (ModelState.IsValid)
        //    {
        //        pmoService.AtualizarMesesAdiantePMO(model.Id, model.QuantidadeMesesAdiante, model.Versao);
        //        DisplaySuccessMessage(SGIPMOMessages.MS013);
        //        return RedirectToAction("ManterPMO", new { id = model.Id });
        //    }
        //    model = RecarregarPMOModel(model.Id);
        //    return View("ManterPMO", model);
        //}

        //[HttpPost]
        //[ActionSource("Excluir")]
        //[Auditoria("Configurar PMO", "Excluir PMO", typeof(PMO))]
        //public ActionResult ExcluirPMO(PMOManterModel model)
        //{
        //    SetViewError("ManterPMO", model, RecarregarModel);
        //    if (ModelState.IsValid)
        //    {
        //        pmoService.ExcluirPMO(new DadosPMODTO() { IdPMO = model.Id, VersaoPMO = Convert.FromBase64String(model.VersaoPmoString) });
        //        DisplaySuccessMessage(SGIPMOMessages.MS013);
        //    }
        //    return RedirectToAction("Index");
        //}

        //private void RecarregarModel(PMOManterModel model)
        //{
        //    Mapper.DynamicMap(RecarregarPMOModel(model.Id), model);
        //}

        //private PMOManterModel RecarregarPMOModel(int idPMO)
        //{
        //    PMO pmo = pmoService.ObterPMOPorChave(idPMO);
        //    return RecarregarPMOModel(pmo);
        //}

        //private PMOManterModel RecarregarPMOModel(PMO pmo)
        //{
        //    PMOManterModel manterModel = new PMOManterModel();
        //    if (pmo != null)
        //    {
        //        manterModel = Mapper.Map<PMOManterModel>(pmo);
        //        manterModel.VersaoPmoString = Convert.ToBase64String(pmo.Versao);
        //    }
        //    return manterModel;
        //}

        //#endregion

        //#region Pesquisa PMO

        //[HttpPost]
        //[ActionSource("Pesquisar")]
        //public ActionResult PesquisaPMO(PMOConsultaModel model)
        //{
        //    SetViewError("Index", model);
        //    if (ModelState.IsValid)
        //    {
        //        if (model.Ano != null && model.Mes != null)
        //        {
        //            var filtro = new PMOFilter() { Ano = model.Ano.Value, Mes = model.Mes.Value };
        //            PMO pmo = pmoService.ObterPMOPorFiltro(filtro);
        //            if (pmo != null)
        //            {
        //                return RedirectToAction("ManterPMO", new { id = pmo.Id });
        //            }
        //            DisplayErrorMessage(SGIPMOMessages.MS004);
        //        }
        //    }
        //    return View("Index", model);
        //}

        //#endregion

        //#region Abertura Estudo
        //public ActionResult CarregarAbrirEstudo(AberturaEstudoModel model)
        //{
        //    ValidarSelecaoSemanaOperativa(model.IdSemanaOperativa);

        //    DadosSemanaOperativaDTO dto = new DadosSemanaOperativaDTO()
        //    {
        //        IdSemanaOperativa = model.IdSemanaOperativa.Value,
        //        VersaoPMO = Convert.FromBase64String(model.VersaoPmoString)
        //    };
        //    SemanaOperativa semanaOperativa = semanaOperativaService.ObterSemanaOperativaValidaParaAbrirEstudo(dto);
        //    EscolhaGabaritoModel modelModalAberturaEstudo = new EscolhaGabaritoModel
        //    {
        //        IdSemanaOperativa = semanaOperativa.Id,
        //        VersaoSemanaOperativa = semanaOperativa.Versao,
        //        VersaoPMO = semanaOperativa.PMO.Versao
        //    };
        //    return PartialView("_AbrirEstudo", modelModalAberturaEstudo);
        //}

        //private void ValidarSelecaoSemanaOperativa(int? idSemanaOperativa)
        //{
        //    if (!idSemanaOperativa.HasValue)
        //    {
        //        throw new ONSBusinessException(SGIPMOMessages.MS025);
        //    }
        //}

        //[HttpPost]
        //[Auditoria("Configurar PMO", "Abrir Estudo", typeof(SemanaOperativa))]
        //public ActionResult AbrirEstudo(EscolhaGabaritoModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        semanaOperativaService.AbrirEstudo(
        //            new AberturaEstudoDTO()
        //            {
        //                IdSemanaOperativa = model.IdSemanaOperativa,
        //                IdEstudo = model.IdEstudo,
        //                IsPadrao = model.IsPadrao,
        //                VersaoPMO = model.VersaoPMO,
        //                VersaoSemanaOperativa = model.VersaoSemanaOperativa
        //            });
        //    }
        //    return AjaxSuccessResult(SGIPMOMessages.MS013, reloadCurrentView: true);
        //}

        //#endregion

        //#region Incluir Semana Operativa

        //public ActionResult CarregarIncluirSemanaOperativa(PMOManterModel modelParam)
        //{
        //    InclusaoSemanaOperativaModel modelInclusao = new InclusaoSemanaOperativaModel()
        //    {
        //        VersaoPMO = Convert.FromBase64String(modelParam.VersaoPmoString),
        //        IdPMO = modelParam.Id
        //    };
        //    return PartialView("_IncluirSemanaOperativa", modelInclusao);
        //}

        //[HttpPost]
        //[Auditoria("Configurar PMO", "Incluir Semana", typeof(SemanaOperativa))]
        //public ActionResult IncluirSemanaOperativa(InclusaoSemanaOperativaModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        pmoService.IncluirSemanaOperativa(Mapper.Map<InclusaoSemanaOperativaDTO>(model));
        //    }
        //    IList<string> mensagens = new List<string>();
        //    mensagens.Add(SGIPMOMessages.MS013);
        //    string mensagemComSpan =
        //        string.Format("<img src='{0}' style='float:left; margin-right:5px'></img> {1} ",
        //            Url.Content("~/Images/alert2.png"), SGIPMOMessages.MS028);
        //    mensagens.Add(mensagemComSpan);
        //    return AjaxSuccessResult(mensagens, reloadCurrentView: true);
        //}

        //#endregion

        //#region Excluir Semana Operativa

        //[HttpPost]
        //[Auditoria("Configurar PMO", "Excluir Semana", typeof(SemanaOperativa))]
        //public ActionResult ExcluirUltimaSemanaOperativaPMO(ManutencaoSemanaOperativaModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        pmoService.ExcluirUltimaSemanaOperativa(model.IdPMO, Convert.FromBase64String(model.VersaoPmoString));
        //    }
        //    return AjaxSuccessResult(SGIPMOMessages.MS013, reloadCurrentView: true);
        //}

        //#endregion

        //#region Alterar Semana Operativa

        //public ActionResult CarregarAlterarSemanaOperativa(int? idSemanaOperativaSelecionada)
        //{
        //    ValidarSelecaoSemanaOperativa(idSemanaOperativaSelecionada);
        //    SemanaOperativa semanaOperativa = semanaOperativaService.ObterSemanaOperativaPorChave(idSemanaOperativaSelecionada.Value);
        //    AlteracaoSemanaOperativaModel model = Mapper.Map<AlteracaoSemanaOperativaModel>(semanaOperativa);

        //    return PartialView("_AlterarSemanaOperativa", model);
        //}

        //[HttpPost]
        //[Auditoria("Configurar PMO", "Alterar Semana", typeof(SemanaOperativa))]
        //public ActionResult AlterarSemanaOperativa(AlteracaoSemanaOperativaModel model)
        //{
        //    if (ModelStateHandleValid)
        //    {
        //        DadosAlteracaoSemanaOperativaDTO dadosAlteracao =
        //            Mapper.Map<DadosAlteracaoSemanaOperativaDTO>(model);

        //        semanaOperativaService.AlterarSemanaOperativa(dadosAlteracao);
        //    }
        //    return AjaxSuccessResult(SGIPMOMessages.MS013, reloadCurrentView: true);
        //}

        //#endregion

        //#region Resetar Gabarito

        ///// <summary>
        ///// Action que é utilizada para carregar uma view parcial para que o usuário preencha o fomurlário e confirme ao reset do gabarito
        ///// </summary>
        ///// <param name="modelReset"></param>
        ///// <returns></returns>
        //public ActionResult CarregarResetarGabarito(ResetGabaritoSemanaOperativaModel modelReset)
        //{
        //    ValidarSelecaoSemanaOperativa(modelReset.IdSemanaOperativa);
        //    SemanaOperativa semanaOperativa =
        //        semanaOperativaService.ObterSemanaOperativaValidaParaResetarGabarito(modelReset.IdSemanaOperativa.Value);

        //    List<SelectListItem> estudosDesconsiderados = new List<SelectListItem>();
        //    estudosDesconsiderados.Add(new SelectListItem() { Value = semanaOperativa.Id.ToString() });

        //    EscolhaGabaritoModel model = new EscolhaGabaritoModel
        //    {
        //        IdSemanaOperativa = semanaOperativa.Id,
        //        VersaoPMO = Convert.FromBase64String(modelReset.VersaoPmoString),
        //        VersaoSemanaOperativa = semanaOperativa.Versao,
        //        EstudosParaDesconsiderar = estudosDesconsiderados
        //    };
        //    return PartialView("_ResetarGabarito", model);
        //}

        ///// <summary>
        ///// Action utilizada para consumir o serviço, efetuando a operação de reset do gabarito da semana operativa.
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Auditoria("Configurar PMO", "Resetar Gabarito", typeof(PMO))]
        //public ActionResult ResetarGabarito(EscolhaGabaritoModel model)
        //{
        //    if (ModelStateHandleValid)
        //    {
        //        ResetGabaritoDTO dto = Mapper.Map<ResetGabaritoDTO>(model);
        //        semanaOperativaService.ResetarGabarito(dto);
        //    }
        //    return AjaxSuccessResult(SGIPMOMessages.MS013, reloadCurrentView: true);
        //}

        //#endregion
    }
}
