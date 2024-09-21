using AutoMapper;

namespace ONS.WEBPMO.Api.Controllers
{
    [WebPermission("ConvergirPLD")]
    public class PLDController : ControllerBase
    {
        private readonly ISemanaOperativaService semanaOperativaService;
        private readonly IColetaInsumoPresentation coletaInsumoPresentation;
        private readonly IArquivoService arquivoService;

        public PLDController(
            ISemanaOperativaService semanaOperativaService,
            IColetaInsumoPresentation coletaInsumoPresentation,
            IArquivoService arquivoService)
        {
            this.semanaOperativaService = semanaOperativaService;
            this.coletaInsumoPresentation = coletaInsumoPresentation;
            this.arquivoService = arquivoService;

        }

        /// <summary>
        /// Ação padrão deste controlador, que é chamado também quando omite-se a action na URL /sgipmo/PLD/
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ConsultaPLDModel model = new ConsultaPLDModel();
            return View(model);

        }

        #region Pesquisas

        public ActionResult CarregarPesquisa(ConsultaPLDModel model)
        {
            PesquisaColetaInsumoModel modelPesquisa = new PesquisaColetaInsumoModel() { IdSemanaOperativa = model.IdSemanaOperativa };
            CarregarDadosPesquisa(modelPesquisa);
            Mapper.DynamicMap(modelPesquisa, model);
            return View("Index", model);
        }

        private void CarregarDadosPesquisa(PesquisaColetaInsumoModel model)
        {
            DadosPesquisaColetaInsumoDTO dadosFiltro = coletaInsumoPresentation
                .ObterDadosPesquisaColetaInsumo(model.IdSemanaOperativa, true);
            Mapper.DynamicMap(dadosFiltro, model);
        }

        /// <summary>
        /// Action responsável pela pesquisa de PLD de acordo com estudo selecionado na listagem.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Pesquisar(ConsultaPLDModel model)
        {
            SetViewError("Index", model);

            if (ModelState.IsValid)
            {
                if (model.IdSemanaOperativa.HasValue)
                {
                    ConvergirPLDModel modelView = new ConvergirPLDModel()
                    {
                        IdSemanaOperativa = model.IdSemanaOperativa.Value,
                        SemanasOperativas = model.SemanasOperativas
                    };

                    CarregarConvergenciaPLD(modelView);
                    return View("ConvergirPLD", modelView);
                }
            }
            return View("Index", model);
        }

        /// <summary>
        /// Método utilizado para carregar as informações a serem visualizadas pelo usuário.
        /// </summary>
        /// <param name="model"></param>
        private void CarregarConvergenciaPLD(ConvergirPLDModel model)
        {
            ArquivosSemanaOperativaFilter filtro = new ArquivosSemanaOperativaFilter()
            {
                IdSemanaOperativa = model.IdSemanaOperativa.Value,
                IsConsiderarInsumosConvergenciaCCEE = true
            };
            ArquivosSemanaOperativaConvergirPldDTO consultaConvergenciaPld =
                semanaOperativaService.ConsultarArquivosSemanaOperativaConvergenciaPLD(filtro);

            Mapper.Map(consultaConvergenciaPld, model);
        }

        /// <summary>
        /// Action utilizada pelo componente AutoComplete existente na tela de pesquisa de PLD.
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public ActionResult ConsultarEstudos(string term)
        {
            var semanasOperativas = semanaOperativaService.ConsultarEstudoPorNome(term);
            return Json(semanasOperativas.Select(s => new { Descricao = s.Nome, Chave = s.Id }), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Convergir PLD e Não Convergir PLD

        [HttpPost]
        [ActionSource("ConvergirPLD")]
        [Auditoria("Convergir PLD", "Convergir PLD", typeof(SemanaOperativa))]
        public ActionResult ConvergirPLD(ConvergirPLDModel model)
        {
            return ConvergirNaoConvergirPLD(model);
        }

        [HttpPost]
        [ActionSource("NaoConvergirPLD")]
        [Auditoria("Convergir PLD", "Não Convergir PLD", typeof(SemanaOperativa))]
        public ActionResult NaoConvergirPLD(ConvergirPLDModel model)
        {
            return ConvergirNaoConvergirPLD(model);
        }

        private ActionResult ConvergirNaoConvergirPLD(ConvergirPLDModel model)
        {
            ConvergirPLDModel modelView = new ConvergirPLDModel() { IdSemanaOperativa = model.IdSemanaOperativa };
            SetViewError("ConvergirPLD", modelView, CarregarConvergenciaPLD);

            if (ModelStateHandleValid)
            {
                ConvergirPLDDTO dto = new ConvergirPLDDTO()
                {
                    Convergir = model.IsConvergirPLD,
                    IdSemanaOperativa = model.IdSemanaOperativa.Value,
                    VersaoSemanaOperativa = Convert.FromBase64String(model.VersaoStringSemanaOperativa),
                    ObservacoesConvergenciaPld = model.ObservacoesConvergenciaPld
                };

                semanaOperativaService.ConvergirPLD(dto);

                DisplaySuccessMessage(SGIPMOMessages.MS013);
            }

            return RedirectToAction("CarregarPesquisa", new { IdSemanaOperativa = model.IdSemanaOperativa });
        }

        #endregion

        #region Download Arquivos

        protected override byte[] DownloadFileDatabase(string id)
        {
            Guid idConvertido = Guid.Parse(id);
            return arquivoService.ObterArquivoDadoNaoEstruturadoEmBytes(idConvertido);
        }

        protected override ResponseDownload DownloadFilesDatabase(RequestDownload request)
        {
            return arquivoService.ObterArquivosCompactados(request);
        }

        #endregion

    }
}
