using AutoMapper;


namespace ONS.WEBPMO.Api.Controllers
{
    [WebPermission("LogNotificacao")]
    public class LogNotificacaoController : ControllerBase
    {

        private readonly ILogNotificacaoService logNotificacaoService;
        private readonly ILogNotificacaoPresentation logNotificacaoPresentation;
        private readonly IColetaInsumoPresentation coletaInsumoPresentation;
        private readonly ISemanaOperativaService semanaOperativaService;
        private readonly IColetaInsumoService coletaInsumoService;

        public LogNotificacaoController(IColetaInsumoPresentation coletaInsumoPresentation, ISemanaOperativaService semanaOperativaService,
            IColetaInsumoService coletaInsumoService, ILogNotificacaoService logNotificacaoService, ILogNotificacaoPresentation logNotificacaoPresentation)
        {
            this.coletaInsumoPresentation = coletaInsumoPresentation;
            this.semanaOperativaService = semanaOperativaService;
            this.coletaInsumoService = coletaInsumoService;
            this.logNotificacaoService = logNotificacaoService;
            this.logNotificacaoPresentation = logNotificacaoPresentation;
        }

        public ActionResult Index()
        {
            LogNotificacaoDTO dadosFiltro = this.logNotificacaoPresentation.ObterDadosPesquisaLogNotificacao();
            var model = Mapper.Map<PesquisaLogNotificacaoModel>(dadosFiltro);
            return View(model);
        }

        public ActionResult CarregarDadosEstudo(int? idSemanaOperativa)
        {
            DadosPesquisaColetaInsumoDTO dto = coletaInsumoPresentation.ObterDadosPesquisaColetaInsumo(idSemanaOperativa, true);
            return Json(new { dto.Agentes, dto.Insumos }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Pesquisar(PesquisaLogNotificacaoModel model)
        {
            if (ModelStateHandleValid)
            {
                SemanaOperativa semanaOperativa = semanaOperativaService.ObterSemanaOperativaPorChave(model.IdSemanaOperativa.Value);

                model.NomeSemanaOperativaSituacao = semanaOperativa.Situacao == null
                    ? semanaOperativa.Nome
                    : string.Format("{0} - {1}", semanaOperativa.Nome, semanaOperativa.Situacao.Descricao);

                model.IdSituacaoSemanaOperativa = semanaOperativa.Situacao.Id;

                if (semanaOperativa.Situacao == null)
                {
                    ViewBag.Titulo = model.NomeSemanaOperativaSituacao;
                    ViewBag.Mensagem = SGIPMOMessages.MS074;

                    return PartialView("_LogNotificacaoVazio");
                }

                LogNotificacaoFilter filter = Mapper.DynamicMap<LogNotificacaoFilter>(model);
            }

            return PartialView("_PesquisaLogNotificacao", model);
        }

        [HttpPost]
        public ActionResult Excluir(PesquisaLogNotificacaoModel model)
        {
            if (ModelStateHandleValid)
            {
                bool success = logNotificacaoService.Apagar(model.IdsLogNotificacao.ToList());

                if (success)
                {
                    model.IdsLogNotificacao = null;
                }

                return Pesquisar(model);
            }

            return PartialView("_PesquisaLogNotificacao", model);
        }

        public ActionResult CarregarPesquisaLogNotificacao(PesquisaLogNotificacaoModel pesquisaLogNotificacaoModel)
        {
            LogNotificacaoDTO dadosFiltro = logNotificacaoPresentation.ObterDadosPesquisaLogNotificacao(pesquisaLogNotificacaoModel.IdSemanaOperativa);
            PesquisaLogNotificacaoModel model = Mapper.Map<PesquisaLogNotificacaoModel>(dadosFiltro);

            if (pesquisaLogNotificacaoModel.IdSemanaOperativa > 0)
            {
                model.IdSemanaOperativa = pesquisaLogNotificacaoModel.IdSemanaOperativa;
                model.IdsAgentes = pesquisaLogNotificacaoModel.IdsAgentes;
                model.IdSituacaoSemanaOperativa = pesquisaLogNotificacaoModel.IdSituacaoSemanaOperativa;
            }

            return View("Index", model);
        }

        public ActionResult CarregarGridLogNotificacao(GridSettings gridSettings, PesquisaLogNotificacaoModel model)
        {
            PagedResult<LogNotificacao> resultadoPaginado = null;

            if (ModelStateHandleValid)
            {
                LogNotificacaoFilter filter = Mapper.DynamicMap<LogNotificacaoFilter>(model);

                Mapper.DynamicMap(gridSettings, filter);

                resultadoPaginado = logNotificacaoService.ObterLogNotificacao(filter);
            }

            return JsonToPagedGrid(resultadoPaginado, gridSettings.PageIndex);
        }
    }
}
