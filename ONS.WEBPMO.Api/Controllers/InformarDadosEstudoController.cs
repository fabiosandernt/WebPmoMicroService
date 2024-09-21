using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta;
using ONS.WEBPMO.Domain.Presentations;
using System.Data;
using System.Drawing;

namespace ONS.WEBPMO.Api.Controllers
{
    //[WebPermission("VisualizarDadosEstudo", "InformarDadosEstudo")]
    public class InformarDadosEstudoController : ControllerBase
    {
        private readonly IColetaInsumoService coletaInsumoService;
        private readonly IColetaInsumoPresentation coletaInsumoPresentation;
        private readonly ISemanaOperativaService semanaOperativaService;
        private readonly IOrigemColetaService origemColetaService;
        private readonly IArquivoService arquivoService;
        private readonly IInsumoService insumoService;
        private readonly IAgenteService agenteService;

        public InformarDadosEstudoController(
            IColetaInsumoService coletaInsumoService,
            IColetaInsumoPresentation coletaInsumoPresentation,
            ISemanaOperativaService semanaOperativaService,
            IOrigemColetaService origemColetaService,
            IArquivoService arquivoService,
            IInsumoService insumoService,
            IAgenteService agenteService)
        {
            this.coletaInsumoService = coletaInsumoService;
            this.coletaInsumoPresentation = coletaInsumoPresentation;
            this.semanaOperativaService = semanaOperativaService;
            this.origemColetaService = origemColetaService;
            this.arquivoService = arquivoService;
            this.insumoService = insumoService;
            this.agenteService = agenteService;
        }

        public ActionResult Index()
        {
            DadosPesquisaColetaInsumoDTO dadosFiltro =
                coletaInsumoPresentation.ObterDadosPesquisaColetaInsumo();
            PesquisaColetaInsumoModel model = Mapper.Map<PesquisaColetaInsumoModel>(dadosFiltro);

            return View(model);
        }

        public ActionResult CarregarPesquisaColetaInsumo(FiltroPesquisaColetaInsumoModel pesquisaColetaInsumoModel)
        {
            DadosPesquisaColetaInsumoDTO dadosFiltro =
                coletaInsumoPresentation.ObterDadosPesquisaColetaInsumo(pesquisaColetaInsumoModel.IdSemanaOperativa);
            PesquisaColetaInsumoModel model = Mapper.Map<PesquisaColetaInsumoModel>(dadosFiltro);

            if (pesquisaColetaInsumoModel.IdSemanaOperativa > 0)
            {
                model.IdSemanaOperativa = pesquisaColetaInsumoModel.IdSemanaOperativa;
                model.IdsAgentes = pesquisaColetaInsumoModel.IdsAgentes;
                model.IdsInsumo = pesquisaColetaInsumoModel.IdsInsumo;
                model.IdsSituacaoColeta = pesquisaColetaInsumoModel.IdsSituacaoColeta;
                model.IdSituacaoSemanaOperativa = pesquisaColetaInsumoModel.IdSituacaoSemanaOperativa;
            }

            return View("Index", model);
        }

        public ActionResult CarregarDadosEstudo(int? idSemanaOperativa)
        {
            DadosPesquisaColetaInsumoDTO dto = coletaInsumoPresentation.ObterDadosPesquisaColetaInsumo(idSemanaOperativa);
            return Json(new { dto.Agentes, dto.Insumos }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Pesquisar(PesquisaColetaInsumoModel model)
        {
            if (ModelStateHandleValid)
            {
                SemanaOperativa semanaOperativa =
                    semanaOperativaService.ObterSemanaOperativaPorChave(model.IdSemanaOperativa.Value);

                model.NomeSemanaOperativaSituacao = semanaOperativa.Situacao == null
                    ? semanaOperativa.Nome
                    : string.Format("{0} - {1}", semanaOperativa.Nome, semanaOperativa.Situacao.Descricao);

                ////Regra comentada para o funcionamento da exportação em excel na página _PesquisaColetaInsumo.
                ////Antes entrava nesse if quem era diferente da situação "Coleta de Dados" e retornava apenas uma mesagem na tela para o usuário.
                ////Agora a tabela abre com todos os registros e se for situação diferente de "Coleta de Dados", entra na função java script DisplayButtons() na página _PesquisaColetaInsumo.
                ////O usuário não poderá fazer nada, a não ser visualizar os registros e exportar os mesmos para o Excel.
                model.IdSituacaoSemanaOperativa = semanaOperativa.Situacao.Id;
                if (semanaOperativa.Situacao == null)
                {
                    ViewBag.Titulo = model.NomeSemanaOperativaSituacao;
                    ViewBag.Mensagem = SGIPMOMessages.MS074;

                    return PartialView("_ColetaInsumoVazio");
                }
                //if (semanaOperativa.Situacao == null
                //    || semanaOperativa.Situacao.Id != (int)SituacaoSemanaOperativaEnum.ColetaDados)
                //{
                //    ViewBag.Titulo = model.NomeSemanaOperativaSituacao;
                //    ViewBag.Mensagem = semanaOperativa.Situacao != null
                //        && semanaOperativa.Situacao.Id == (int)SituacaoSemanaOperativaEnum.Configuracao
                //            ? SGIPMOMessages.MS031
                //            : SGIPMOMessages.MS023;

                //    return PartialView("_ColetaInsumoVazio");
                //}
            }

            return PartialView("_PesquisaColetaInsumo", model);
        }

        private string GetReturnUrlDecodedPesquisa(int idColetaInsumo)
        {
            ColetaInsumoModel newModel = new ColetaInsumoModel { IdColetaInsumo = idColetaInsumo };
            CarregarColetaInsumoModel(newModel);
            newModel.MotivoAlteracaoONS = string.Empty;
            newModel.MotivoRejeicaoONS = string.Empty;
            return Server.UrlDecode(Url.Action("CarregarInformarDados", "InformarDadosEstudo") + "?" + newModel.AsQueryString());
        }

        #region Carregamento dos Grids Ajax (JqGrid)
        public ActionResult CarregarGridColetasInsumo(GridSettings gridSettings, PesquisaColetaInsumoModel model)
        {
            PagedResult<ColetaInsumo> resultadoPaginado = null;

            if (ModelStateHandleValid)
            {
                PesquisaMonitorarColetaInsumoFilter filter =
                    Mapper.DynamicMap<PesquisaMonitorarColetaInsumoFilter>(model);

                Mapper.DynamicMap(gridSettings, filter);

                resultadoPaginado = coletaInsumoService.ConsultarColetasInsumoParaInformarDadosPaginado(filter);
            }

            return JsonToPagedGrid(resultadoPaginado, gridSettings.PageIndex);
        }
        #endregion

        [HttpPost]
        [WebPermission("InformarDadosEstudo", IgnorePermissionsInClass = true)]
        [Auditoria("Informar Dados Estudo", "Enviar", typeof(SemanaOperativa))]
        public ActionResult EnviarDados(EnviarDadosColetaInsumoModel model)
        {
            PesquisaColetaInsumoModel modelPesquisa = CastPesquisaColetaInsumoModel(model);
            SetViewError("Index", modelPesquisa, CarregarDadosPesquisa);

            EnviarDadosColetaInsumoFilter filter = Mapper.DynamicMap<EnviarDadosColetaInsumoFilter>(model);
            coletaInsumoService.EnviarDadosColetaInsumo(filter);

            return AjaxSuccessResult(SGIPMOMessages.MS013, ReturnUrl());
        }

        private PesquisaColetaInsumoModel CastPesquisaColetaInsumoModel(FiltroPesquisaColetaInsumoModel model)
        {
            PesquisaColetaInsumoModel pesquisaModel = new PesquisaColetaInsumoModel();
            Mapper.DynamicMap(model, pesquisaModel);
            return pesquisaModel;
        }

        public ActionResult VisualizarColetaInsumo(int idColetaInsumo)
        {
            ColetaInsumo coletaInsumo = coletaInsumoService.ObterColetaInsumoInformarDadosPorChave(idColetaInsumo);
            ColetaInsumoModel model = Mapper.DynamicMap<ColetaInsumoModel>(coletaInsumo);

            // Coleta insumo que já pode ser visualizada porque os dados foram informados
            string viewName = "VisualizarColetaInsumo";
            if (coletaInsumo.Insumo.Reservado)
            {
                viewName = "VisualizarColetaInsumoManutencao";
            }
            else if (coletaInsumo.Insumo.TipoInsumo.Equals("L"))
            {
                viewName = "VisualizarColetaInsumoNaoEstruturado";
                model = ObterColetaInsumoNaoEstruturadoModel(idColetaInsumo);
            }
            return View(viewName, model);
        }

        [WebPermission("InformarDadosEstudo", IgnorePermissionsInClass = true)]
        [Auditoria("Informar Dados Estudo", "Informar", typeof(SemanaOperativa))]
        public ActionResult CarregarInformarDados(InformaDadosColetaInsumoModel model)
        {
            SetViewError("Index", CastPesquisaColetaInsumoModel(model), CarregarDadosPesquisa);

            byte[] versao = null;
            if (!string.IsNullOrEmpty(model.VersaoColetaInsumo))
            {
                versao = Convert.FromBase64String(model.VersaoColetaInsumo);
            }

            var updateToAndamento = "true".Equals(Request.Params["UpdateToAndamento"]);

            ColetaInsumo coletaInsumo = coletaInsumoService.ObterValidarColetaInsumoInformarDadosPorChave(model.IdColetaInsumo, versao, updateToAndamento);


            string[] idsInsumosRecuperarDados = ConfigurationManager.AppSettings["InsumosRecuperarDados"].Split(';');
            foreach (var item in idsInsumosRecuperarDados)
            {
                if (int.Parse(item) == coletaInsumo.InsumoId)
                {
                    ViewBag.RecuperarDados = true;
                    break;
                }
            }
            ColetaInsumoModel modelColeta = Mapper.DynamicMap<ColetaInsumoModel>(coletaInsumo);

            string viewName = "InformarDadosEstudo";
            if (coletaInsumo.Insumo.Reservado)
            {
                viewName = "InformarDadoColetaManutencao";
            }
            else if (coletaInsumo.Insumo.TipoInsumo.Equals("L"))
            {
                viewName = "InformarDadosEstudoNaoEstruturado";
                ColetaInsumoNaoEstruturadoModel modelNaoEstruturadoModelo = Mapper.DynamicMap<ColetaInsumoNaoEstruturadoModel>(modelColeta);
                if (coletaInsumo.Situacao.Id != (int)SituacaoColetaInsumoEnum.NaoIniciado)
                {
                    modelNaoEstruturadoModelo = ObterColetaInsumoNaoEstruturadoModel(model.IdColetaInsumo);
                }
                modelColeta = modelNaoEstruturadoModelo;
            }

            modelColeta.VisualizarBotaoIncluirManutencao = coletaInsumoService.VerificarPermissaoIncluirManutencao();

            return View(viewName, modelColeta);
        }

        #region Dados Estruturados


        public JsonResult CarregarGridDadosColeta(GridSettings gridSettings, ColetaInsumoModel model)
        {
            DadosInformarColetaInsumoModel resultado = new DadosInformarColetaInsumoModel();

            if (ModelStateHandleValid)
            {
                ColetaInsumoFilter filtro = new ColetaInsumoFilter
                {
                    IdColetaInsumo = model.IdColetaInsumo,
                    PageIndex = gridSettings.PageIndex,
                    PageSize = gridSettings.PageSize
                };

                DadosInformarColetaInsumoDTO dados =
                    coletaInsumoService.ObterDadosParaInformarDadosPorChaveColetaInsumo(filtro);

                Mapper.DynamicMap(dados, resultado);

                PreencherDados(resultado);
            }

            var jsonData = new
            {
                total = resultado.TotalPaginas,
                page = gridSettings.PageIndex,
                records = resultado.DadosColetaInsumoPaginado.TotalCount,
                rows = resultado.DadosColetaInsumoPaginado.List,
                userdata = resultado.Dados == null ? string.Empty : JsonConvert.SerializeObject(resultado.Dados),
                dadostela = resultado.DadosTela == null ? string.Empty : JsonConvert.SerializeObject(resultado.DadosTela),
                header = resultado.Header == null ? string.Empty : JsonConvert.SerializeObject(resultado.Header),
                recuperaValor = resultado.DadosColetaInsumoPaginado.List.Any(d => d.IsRecuperaValor)
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private Dictionary<string, byte[]> MontarExcel(int[] selRowIds, GridSettings gridSettings)
        {
            try
            {
                ExcelPackage pck = new ExcelPackage();

                string AgenteExcel = string.Empty;
                string EstudoExcel = string.Empty;

                for (int k = 0; k < selRowIds.Length; k++)
                {
                    int selRowIds_ = selRowIds[k];

                    #region CONSULTA A LINHA DA GRID

                    //Consulta a linha da Grid
                    ColetaInsumo dadosColetaInsumo = coletaInsumoService.ObterColetaInsumoInformarDadosPorChave(selRowIds_);
                    AgenteExcel = dadosColetaInsumo.Agente.Nome;
                    EstudoExcel = dadosColetaInsumo.SemanaOperativa.Nome;
                    string InsumoExel = dadosColetaInsumo.Insumo.Nome;
                    string SituacaoColetaExcel = dadosColetaInsumo.Situacao.Descricao;
                    string SiglaInsumoExcel = dadosColetaInsumo.Insumo.SiglaInsumo;
                    bool ReservadoExcel = dadosColetaInsumo.Insumo.Reservado;

                    #endregion

                    #region Monta Início Excel

                    List<InicioExcel> ListInicioExcel = new List<InicioExcel>();
                    ListInicioExcel.Add(new InicioExcel() { Inicio = "Agente: ", ColunaInicio = "A", ColunaDescricao = "B", Descricao = AgenteExcel });
                    ListInicioExcel.Add(new InicioExcel() { Inicio = "Estudo: ", ColunaInicio = "A", ColunaDescricao = "B", Descricao = EstudoExcel });
                    ListInicioExcel.Add(new InicioExcel() { Inicio = "Insumo: ", ColunaInicio = "A", ColunaDescricao = "B", Descricao = InsumoExel });
                    ListInicioExcel.Add(new InicioExcel() { Inicio = "Situacao Coleta: ", ColunaInicio = "A", ColunaDescricao = "B", Descricao = SituacaoColetaExcel });
                    ListInicioExcel.Add(new InicioExcel() { Inicio = "", ColunaInicio = "A", ColunaDescricao = "B", Descricao = "" });

                    #endregion

                    if (ReservadoExcel)
                    {

                        #region PESQUISA PARA MONTAR EXCEL

                        //Pesquisa para montar Excel
                        ColetaInsumoModel model = new ColetaInsumoModel();
                        model.IdColetaInsumo = selRowIds_;

                        DadoColetaInsumoFilter filter = Mapper.DynamicMap<DadoColetaInsumoFilter>(model);

                        Mapper.DynamicMap(gridSettings, filter);

                        IList<PesquisaDadoColetaManutencaoModel> dadosColetaList = new List<PesquisaDadoColetaManutencaoModel>();

                        var dadosColeta = coletaInsumoService.ConsultarDadoColetaManutencaoPorColetaInsumoPaginado(filter);
                        filter.PageSize = dadosColeta.TotalCount;

                        dadosColeta = coletaInsumoService.ConsultarDadoColetaManutencaoPorColetaInsumoPaginado(filter);

                        #endregion

                        #region MONTA CABEÇALHO FIXO

                        List<CabecalhoExcel> listCabecalho = new List<CabecalhoExcel>();
                        listCabecalho.Add(new CabecalhoExcel() { Coluna = "A", Descricao = "Usina" });
                        listCabecalho.Add(new CabecalhoExcel() { Coluna = "B", Descricao = "Unidade" });
                        listCabecalho.Add(new CabecalhoExcel() { Coluna = "C", Descricao = "Número" });
                        listCabecalho.Add(new CabecalhoExcel() { Coluna = "D", Descricao = "Data Início" });
                        listCabecalho.Add(new CabecalhoExcel() { Coluna = "E", Descricao = "Data Término" });
                        listCabecalho.Add(new CabecalhoExcel() { Coluna = "F", Descricao = "Tempo Retorno" });
                        listCabecalho.Add(new CabecalhoExcel() { Coluna = "G", Descricao = "Justificativa" });

                        #endregion

                        #region MONTA DADOS DO EXCEL DINAMICAMENTE

                        string Alfabeto = "ABCDEFGHIJKLMNOPQRSTUVXZ";
                        List<DadosExcel> listDadosExcel = new List<DadosExcel>();

                        if (dadosColeta.List.Count == 0)
                        {
                            int i = 0;
                            int row = listCabecalho.Count;
                            int j = i + (row - 1);
                            listDadosExcel.Add(new DadosExcel() { Coluna = Alfabeto.Substring(i, 1), Coluna2 = Alfabeto.Substring(j, 1), Descricao = SGIPMOMessages.MS004, RowsPan = row.ToString() });
                        }
                        foreach (var dadoColetaManutencao in dadosColeta.List)
                        {
                            int i = 0;
                            listDadosExcel.Add(new DadosExcel() { Coluna = Alfabeto.Substring(i, 1), Descricao = dadoColetaManutencao.NomeUsina, RowsPan = "0" });
                            i++;
                            listDadosExcel.Add(new DadosExcel() { Coluna = Alfabeto.Substring(i, 1), Descricao = dadoColetaManutencao.NomeUnidade, RowsPan = "0" });
                            i++;
                            listDadosExcel.Add(new DadosExcel() { Coluna = Alfabeto.Substring(i, 1), Descricao = dadoColetaManutencao.Numero, RowsPan = "0" });
                            i++;
                            listDadosExcel.Add(new DadosExcel() { Coluna = Alfabeto.Substring(i, 1), Descricao = dadoColetaManutencao.DataInicio.ToString(), RowsPan = "0" });
                            i++;
                            listDadosExcel.Add(new DadosExcel() { Coluna = Alfabeto.Substring(i, 1), Descricao = dadoColetaManutencao.DataFim.ToString(), RowsPan = "0" });
                            i++;
                            listDadosExcel.Add(new DadosExcel() { Coluna = Alfabeto.Substring(i, 1), Descricao = dadoColetaManutencao.TempoRetorno, RowsPan = "0" });
                            i++;
                            listDadosExcel.Add(new DadosExcel() { Coluna = Alfabeto.Substring(i, 1), Descricao = dadoColetaManutencao.Justificativa, RowsPan = "0" });
                            i++;
                        }

                        #endregion

                        #region ESCREVER EXCEL

                        //Em que linha começa a escrever o conteudo do excel (Cabeçalho e Dados)
                        int linha = 1;

                        ExcelWorksheet abas = pck.Workbook.Worksheets.Add(Convert.ToString(SiglaInsumoExcel));

                        foreach (var item in ListInicioExcel)
                        {
                            var LabelInicioExcel = abas.Cells[item.ColunaInicio + linha.ToString()];
                            LabelInicioExcel.Value = item.Inicio;

                            var ValueInicioExcel = abas.Cells[item.ColunaDescricao + linha.ToString()];
                            ValueInicioExcel.Value = item.Descricao;

                            linha++;
                        }

                        foreach (var item in listCabecalho)
                        {
                            var Cabecalho = abas.Cells[item.Coluna + linha.ToString()];
                            Cabecalho.Value = item.Descricao;

                            Cabecalho.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            Cabecalho.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                            Cabecalho.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Cabecalho.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(169, 208, 142));

                            Cabecalho.Style.Border.BorderAround(ExcelBorderStyle.Medium, System.Drawing.Color.FromArgb(70, 198, 128));
                        }

                        int atualLinha = (linha + 1);
                        int proximaLinha = 0;

                        List<LinhaColunaExcel> listLinhaColunaExcel = new List<LinhaColunaExcel>();
                        List<LinhaColunaExcel> listLinhaColunaExcelSizeExcel = new List<LinhaColunaExcel>();

                        foreach (var item in listDadosExcel)
                        {
                            ExcelRange Celulas;
                            LinhaColunaExcel linhaColunaExcel = new LinhaColunaExcel();

                            // Verifica se existe linha na coluna
                            var existe_linhaColunaExcel = listLinhaColunaExcel.Where(x => x.Coluna.Equals(item.Coluna)).FirstOrDefault();

                            //existindo Linha na coluna
                            if (!(existe_linhaColunaExcel == null))
                            {
                                linhaColunaExcel = existe_linhaColunaExcel;
                                atualLinha = int.Parse(linhaColunaExcel.linha);
                                listLinhaColunaExcel.Remove(linhaColunaExcel);
                            }

                            if (int.Parse(item.RowsPan) > 0)
                            {
                                proximaLinha = int.Parse(item.RowsPan);
                                Celulas = abas.Cells[item.Coluna + atualLinha + ":" + item.Coluna2 + proximaLinha];
                                Celulas.Merge = true;
                                Celulas.Value = item.Descricao;

                                Celulas.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                Celulas.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                Celulas.Style.Border.BorderAround(ExcelBorderStyle.Medium, System.Drawing.Color.FromArgb(70, 198, 128));

                                //Pegar Maior String de Cada Coluna
                                listLinhaColunaExcelSizeExcel.Add(new LinhaColunaExcel { Coluna = item.Coluna, linha = item.Descricao });
                            }
                            else
                            {
                                proximaLinha = (int.Parse(item.RowsPan) + atualLinha);

                                Celulas = abas.Cells[item.Coluna + atualLinha];
                                Celulas.Value = item.Descricao;

                                Celulas.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                Celulas.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                Celulas.Style.Border.BorderAround(ExcelBorderStyle.Medium, System.Drawing.Color.FromArgb(70, 198, 128));

                                //Pegar Maior String de Cada Coluna
                                listLinhaColunaExcelSizeExcel.Add(new LinhaColunaExcel { Coluna = item.Coluna, linha = item.Descricao });
                            }

                            proximaLinha++;
                            linhaColunaExcel.Coluna = item.Coluna;
                            linhaColunaExcel.linha = proximaLinha.ToString();
                            listLinhaColunaExcel.Add(linhaColunaExcel);

                        }

                        #endregion

                        #region PEGAR MAIOR STRING DE CADA COLUNA

                        //Pegar Maior String de Cada Coluna
                        if (dadosColeta.List.Count == 0)
                        {
                            //quando não tem registros.
                            var colunaSize = listCabecalho.GroupBy(x => x.Coluna).ToList();
                            for (int j = 0; j < colunaSize.Count(); j++)
                            {
                                var valor = listCabecalho.Where(x => x.Coluna == colunaSize[j].Key).Max(x => x.Descricao);
                                if (valor != null)
                                {
                                    abas.Column(j + 1).AutoFit(valor.Length + 8);
                                }
                                else
                                {
                                    abas.Column(j + 1).AutoFit(15);
                                }
                            }
                        }
                        else
                        {
                            var colunaSize = listLinhaColunaExcelSizeExcel.GroupBy(x => x.Coluna).ToList();
                            for (int j = 0; j < colunaSize.Count(); j++)
                            {
                                var ticaracatica = listLinhaColunaExcelSizeExcel.Where(x => x.Coluna == colunaSize[j].Key && !string.IsNullOrEmpty(x.linha)).Max(x => x.linha);
                                if (ticaracatica != null)
                                {
                                    abas.Column(j + 1).AutoFit(ticaracatica.Length + 8);
                                }
                            }
                        }

                        #endregion

                    }
                    else
                    {
                        #region PESQUISA PARA MONTAR EXCEL

                        //Pesquisa para montar Excel
                        DadosInformarColetaInsumoModel resultado = new DadosInformarColetaInsumoModel();
                        ColetaInsumoFilter filtro = new ColetaInsumoFilter
                        {
                            IdColetaInsumo = selRowIds_,
                            PageIndex = 1,
                            PageSize = 1
                        };

                        DadosInformarColetaInsumoDTO dados = coletaInsumoService.ObterDadosParaInformarDadosPorChaveColetaInsumo(filtro);
                        Mapper.DynamicMap(dados, resultado);
                        var _retornoHeader = PreencherDados(resultado);
                        IList<DadoColetaModel> listaPaginaAtual = resultado.DadosColetaInsumoPaginado.List;
                        int TotalPaginas = resultado.TotalPaginas;
                        if (TotalPaginas > 1)
                        {
                            for (int j = 1; j < TotalPaginas; j++)
                            {
                                resultado = new DadosInformarColetaInsumoModel();
                                filtro = new ColetaInsumoFilter
                                {
                                    IdColetaInsumo = selRowIds_,
                                    PageIndex = (j + 1),
                                    PageSize = 1
                                };

                                dados = coletaInsumoService.ObterDadosParaInformarDadosPorChaveColetaInsumo(filtro);
                                Mapper.DynamicMap(dados, resultado);

                                ((List<DadoColetaModel>)listaPaginaAtual).AddRange(resultado.DadosColetaInsumoPaginado.List);
                            }
                        }

                        #endregion

                        #region MONTA CABEÇALHO FIXO E DINAMICO

                        List<CabecalhoExcel> listCabecalho = new List<CabecalhoExcel>();
                        string Alfabeto = "ABCDEFGHIJKLMNOPQRSTUVXZ";
                        listCabecalho.Add(new CabecalhoExcel() { Coluna = "A", Descricao = "Usina" });
                        listCabecalho.Add(new CabecalhoExcel() { Coluna = "B", Descricao = "Grandeza" });
                        listCabecalho.Add(new CabecalhoExcel() { Coluna = "C", Descricao = "Patamar" });
                        listCabecalho.Add(new CabecalhoExcel() { Coluna = "D", Descricao = "Limite" });
                        int i = 4;
                        foreach (var item in _retornoHeader)
                        {
                            listCabecalho.Add(new CabecalhoExcel() { Coluna = Alfabeto.Substring(i, 1), Descricao = item });
                            i++;
                        }

                        #endregion

                        #region MONTA DADOS DO EXCEL DINAMICAMENTE

                        i = 0;
                        List<DadosExcel> listDadosExcel = new List<DadosExcel>();
                        foreach (DadoColetaModel dcm in listaPaginaAtual)
                        {
                            if (dcm.RowspanUsina > 0)
                            {
                                listDadosExcel.Add(new DadosExcel() { Coluna = Alfabeto.Substring(i, 1), Descricao = dcm.OrigemColetaNome, RowsPan = dcm.RowspanUsina.ToString() });
                            }
                            i++;
                            if (dcm.RowspanGrandeza > 0)
                            {
                                listDadosExcel.Add(new DadosExcel() { Coluna = Alfabeto.Substring(i, 1), Descricao = dcm.GrandezaNome, RowsPan = dcm.RowspanGrandeza.ToString() });
                            }
                            i++;
                            listDadosExcel.Add(new DadosExcel() { Coluna = Alfabeto.Substring(i, 1), Descricao = dcm.TipoPatamarDescricao, RowsPan = "0" });
                            i++;
                            listDadosExcel.Add(new DadosExcel() { Coluna = Alfabeto.Substring(i, 1), Descricao = dcm.TipoLimiteDescricao, RowsPan = "0" });
                            i++;

                            for (int j = 0; j < dcm.ValoresDadoColeta.Count; j++)
                            {
                                listDadosExcel.Add(new DadosExcel() { Coluna = Alfabeto.Substring(i, 1), Descricao = dcm.ValoresDadoColeta[j].Valor, RowsPan = "0" });
                                i++;
                            }
                            i = 0;
                        }

                        #endregion

                        #region ESCREVER EXCEL

                        //Em que linha começa a escrever o conteudo do excel (Cabeçalho e Dados)
                        int linha = 1;

                        ExcelWorksheet abas = pck.Workbook.Worksheets.Add(Convert.ToString(SiglaInsumoExcel));

                        foreach (var item in ListInicioExcel)
                        {
                            var LabelInicioExcel = abas.Cells[item.ColunaInicio + linha.ToString()];
                            LabelInicioExcel.Value = item.Inicio;

                            var ValueInicioExcel = abas.Cells[item.ColunaDescricao + linha.ToString()];
                            ValueInicioExcel.Value = item.Descricao;

                            linha++;
                        }

                        foreach (var item in listCabecalho)
                        {
                            var Cabecalho = abas.Cells[item.Coluna + linha.ToString()];
                            Cabecalho.Value = item.Descricao;

                            Cabecalho.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            Cabecalho.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                            Cabecalho.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Cabecalho.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(169, 208, 142));

                            Cabecalho.Style.Border.BorderAround(ExcelBorderStyle.Medium, System.Drawing.Color.FromArgb(70, 198, 128));
                        }

                        int atualLinha = (linha + 1);
                        int proximaLinha = 0;

                        List<LinhaColunaExcel> listLinhaColunaExcel = new List<LinhaColunaExcel>();
                        List<LinhaColunaExcel> listLinhaColunaExcelSizeExcel = new List<LinhaColunaExcel>();

                        foreach (var item in listDadosExcel)
                        {
                            ExcelRange Celulas;
                            LinhaColunaExcel linhaColunaExcel = new LinhaColunaExcel();

                            // Verifica se existe linha na coluna
                            var existe_linhaColunaExcel = listLinhaColunaExcel.Where(x => x.Coluna.Equals(item.Coluna)).FirstOrDefault();

                            //existindo Linha na coluna
                            if (!(existe_linhaColunaExcel == null))
                            {
                                linhaColunaExcel = existe_linhaColunaExcel;
                                atualLinha = int.Parse(linhaColunaExcel.linha);
                                listLinhaColunaExcel.Remove(linhaColunaExcel);
                            }

                            if (int.Parse(item.RowsPan) > 0)
                            {
                                proximaLinha = ((int.Parse(item.RowsPan) - 1) + atualLinha);

                                Celulas = abas.Cells[item.Coluna + atualLinha + ":" + item.Coluna + proximaLinha];
                                Celulas.Merge = true;
                                Celulas.Value = item.Descricao;

                                Celulas.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                Celulas.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                Celulas.Style.Border.BorderAround(ExcelBorderStyle.Medium, System.Drawing.Color.FromArgb(70, 198, 128));

                                //Pegar Maior String de Cada Coluna
                                listLinhaColunaExcelSizeExcel.Add(new LinhaColunaExcel { Coluna = item.Coluna, linha = item.Descricao });
                            }
                            else
                            {
                                proximaLinha = (int.Parse(item.RowsPan) + atualLinha);
                                Celulas = abas.Cells[item.Coluna + atualLinha];
                                Celulas.Value = item.Descricao;

                                Celulas.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                Celulas.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                Celulas.Style.Border.BorderAround(ExcelBorderStyle.Medium, System.Drawing.Color.FromArgb(70, 198, 128));

                                //Pegar Maior String de Cada Coluna
                                listLinhaColunaExcelSizeExcel.Add(new LinhaColunaExcel { Coluna = item.Coluna, linha = item.Descricao });
                            }

                            proximaLinha++;
                            linhaColunaExcel.Coluna = item.Coluna;
                            linhaColunaExcel.linha = proximaLinha.ToString();
                            listLinhaColunaExcel.Add(linhaColunaExcel);

                        }

                        #endregion

                        #region PEGAR MAIOR STRING DE CADA COLUNA

                        //Pegar Maior String de Cada Coluna
                        var colunaSize = listLinhaColunaExcelSizeExcel.GroupBy(x => x.Coluna).ToList();
                        for (int j = 0; j < colunaSize.Count(); j++)
                        {
                            var valor = listLinhaColunaExcelSizeExcel.Where(x => x.Coluna == colunaSize[j].Key).Max(x => x.linha);
                            if (valor != null)
                            {
                                abas.Column(j + 1).AutoFit(valor.Length + 8);
                            }
                            else
                            {
                                abas.Column(j + 1).AutoFit(15);
                            }
                        }

                        #endregion
                    }
                }

                #region BLOQUEAR EXCEL E COLOCAR SENHA

                //pck.Encryption.Password = "EPPlus";
                //pck.Encryption.Algorithm = EncryptionAlgorithm.AES192;
                pck.Workbook.Protection.SetPassword(ConfigurationManager.AppSettings["senhaExcelInformarDadosEstudo"]);
                pck.Workbook.Protection.LockRevision = true;
                pck.Workbook.Protection.LockStructure = true;
                //pck.Workbook.Protection.LockWindows = true;

                foreach (ExcelWorksheet ew in pck.Workbook.Worksheets)
                {
                    ew.Protection.AllowAutoFilter = true;
                    ew.Protection.AllowDeleteColumns = true;
                    ew.Protection.AllowDeleteRows = true;
                    ew.Protection.AllowEditObject = true;
                    ew.Protection.AllowEditScenarios = true;
                    ew.Protection.AllowFormatCells = true;
                    ew.Protection.AllowFormatColumns = true;
                    ew.Protection.AllowFormatRows = true;
                    ew.Protection.AllowInsertColumns = true;
                    ew.Protection.AllowInsertHyperlinks = true;
                    ew.Protection.AllowInsertRows = true;
                    ew.Protection.AllowPivotTables = true;
                    ew.Protection.AllowSelectLockedCells = true;
                    ew.Protection.AllowSelectUnlockedCells = true;
                    ew.Protection.AllowSort = true;
                    ew.Protection.IsProtected = true;
                    ew.Protection.SetPassword(ConfigurationManager.AppSettings["senhaExcelInformarDadosEstudo"]);
                    ew.Cells[1, 1, 1000, 50].Style.Locked = true;
                }

                #endregion

                #region CONFIGURA NOME, EXCLUI ARQUIVOS DOS DIAS ANTERIORES E SALVAR O NOVO EXCEL

                string[] splitEstudoExcel = EstudoExcel.Split('-'); //PMO Fevereiro 2017 - Revisão 1

                string[] NomeFullPMO = splitEstudoExcel[0].Split(' '); //PMO Fevereiro 2017
                string PMO = NomeFullPMO[0]; //PMO
                string MesPMO = NomeFullPMO[1]; //Fevereiro
                string AnoPMO = NomeFullPMO[2]; //2017

                string MesTratado = "00";
                string[] MesesBR = { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
                string[] MesesBRint = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
                for (int i = 0; i < MesesBR.Length; i++)
                {
                    if (MesPMO == MesesBR[i])
                    {
                        MesTratado = MesesBRint[i]; //02
                        break;
                    }
                }

                string inicioNome = PMO + "_" + AnoPMO + MesTratado; //PMO_201503

                if (EstudoExcel.Contains("-"))
                {
                    var RevNum = splitEstudoExcel[1]; // Revisão 1
                    var RevTratado = RevNum.Substring(1, 3).ToLower(); //Rev
                    var NumTratado = RevNum.Substring(RevNum.Length - 1, 1); //3
                    var RevNumTratado = RevTratado + NumTratado; //Rev3

                    inicioNome = inicioNome + RevNumTratado; //PMO_201503rev3
                }

                string AgenteNomeExcel = "_" + AgenteExcel.Substring(0, 3); //_FUR
                DateTime DiaHoraArquivo = DateTime.Now;
                string DiaHoraArquivoTratada = "_" + DiaHoraArquivo.ToString("yyyyMMddHHmmss"); //_20150401095915

                string fimNome = AgenteNomeExcel + DiaHoraArquivoTratada; //_FUR_20150401095915

                string nomeDoExcel = inicioNome + fimNome; //PMO_201503rev3_FUR_20150401095915

                //Cria o Excel e retorna o Nome
                string NameExcelFull = nomeDoExcel + ".xlsx";

                #endregion

                //Adiciona o Excel em um Dictionary
                var ret = new Dictionary<string, byte[]>();
                ret.Add(NameExcelFull, pck.GetAsByteArray());

                //Retorna o Dictionary
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [WebPermission("InformarDadosEstudo", IgnorePermissionsInClass = true)]
        [Auditoria("Informar Dados Estudo", "ExportarExcel", typeof(SemanaOperativa))]
        public ActionResult ExportarExcel(string SelRowIds, GridSettings gridSettings, ColetaInsumoModel model)
        {
            try
            {
                string UrlDownloadExcel = string.Empty;

                List<int> ListIdsGrid = new List<int>();
                List<int> ListIdsPodeExportar = new List<int>();
                List<string> ListMensagensPodeExportar = new List<string>();
                List<string> ListMensagensNaoPodeExportar = new List<string>();
                int cont = 0;

                //Guarda em uma lista os Ids das linhas selecionadas da Grid
                if (!string.IsNullOrEmpty(SelRowIds))
                {
                    if (SelRowIds.Contains(","))
                    {
                        string[] splitSelRowIds = SelRowIds.Split(',');
                        for (int i = 0; i < splitSelRowIds.Length; i++)
                        {
                            var IDs = splitSelRowIds[i];
                            ListIdsGrid.Add(Convert.ToInt32(IDs));
                        }
                    }
                    else
                    {
                        ListIdsGrid.Add(Convert.ToInt32(SelRowIds));
                    }
                }

                //Varrer a lista de Ids das linhas selecionadas da Grid
                foreach (var IdsGrid in ListIdsGrid)
                {
                    //Consulta a Linha da Grid
                    ColetaInsumo dadosColetaInsumo = coletaInsumoService.ObterColetaInsumoInformarDadosPorChave(IdsGrid);
                    string AgenteExcel = dadosColetaInsumo.Agente.Nome;
                    string EstudoExcel = dadosColetaInsumo.SemanaOperativa.Nome;
                    string InsumoExel = dadosColetaInsumo.Insumo.Nome;
                    string SituacaoColetaExcel = dadosColetaInsumo.Situacao.Descricao;
                    string SiglaInsumo = dadosColetaInsumo.Insumo.SiglaInsumo;
                    bool exportarInsumo = dadosColetaInsumo.Insumo.ExportarInsumo;

                    //Verifica se o Insumo pode ser exportado (estruturado).
                    if (exportarInsumo)
                    {
                        //Guarda na lista os Ids das linhas selecionadas da Grid, que pode ser expotados (estruturado).
                        ListIdsPodeExportar.Add(IdsGrid);

                        //Contagem para monstar a mensagem.
                        cont++;
                    }
                    else
                    {
                        //Guarda em uma lista os Agentes/Insumos linhas selecionadas da Grid, que não pode ser expotados (não estruturado).
                        ListMensagensNaoPodeExportar.Add(string.Format(SGIPMOMessages.MS068, AgenteExcel, InsumoExel));
                    }
                }

                //Cria um array com os Ids das linhas selecionadas da Grid, que pode ser expotados (estruturado).
                int[] ArrayPodeExportar = new int[ListIdsPodeExportar.Count];
                for (int i = 0; i < ListIdsPodeExportar.Count; i++)
                {
                    ArrayPodeExportar[i] = ListIdsPodeExportar[i];
                }

                Object btExcel = new Object();
                string NomeExcel = string.Empty;
                //Monta o excel se existir pelo menos um array.
                if (ArrayPodeExportar.Length > 0)
                {
                    //Cria o Excel e retorna o Nome
                    var conteudoExcel = MontarExcel(ArrayPodeExportar, gridSettings);
                    NomeExcel = conteudoExcel.First().Key;
                    btExcel = conteudoExcel.First().Value;
                }

                //Montando as mesnagens que serão visualizadas na tela.
                if (ListMensagensNaoPodeExportar.Any() && cont == 0)
                {
                    throw new ONSBusinessException(SGIPMOMessages.MS069, ReturnUrl());
                }

                return File((Byte[])btExcel, "application/vnd.ms-excel", NomeExcel);

            }
            catch (Exception ex)
            {
                throw new ONSBusinessException(ex.Message, ReturnUrl());
            }
        }

        static List<string> PreencherDados(DadosInformarColetaInsumoModel dadosInformarColetaInsumoModel)
        {
            IList<DadoColetaModel> listaPaginaAtual = dadosInformarColetaInsumoModel.DadosColetaInsumoPaginado.List;
            List<string> headerTextoOut = new List<string>();
            int x = listaPaginaAtual.Count();
            if (x > 0)
            {
                int y = listaPaginaAtual.Max(d => d.ValoresDadoColeta.Count);

                string[,] dadosTela = new string[x, y];
                string[] headerTexto = new string[y];
                string[] headerTitle = new string[y];
                ValorDadoColetaModel[,] dados = new ValorDadoColetaModel[x, y];


                // Preencher Cabeçalho.
                for (int j = 0; j < y; j++)
                {
                    headerTexto[j] = listaPaginaAtual[0].ValoresDadoColeta[j].Estagio;
                    headerTextoOut.Add(listaPaginaAtual[0].ValoresDadoColeta[j].Estagio);
                    headerTitle[j] = listaPaginaAtual[0].ValoresDadoColeta[j].PeriodoSemana;
                }

                var header = new { Estagio = headerTexto, Periodos = headerTitle };

                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        dados[i, j] = listaPaginaAtual[i].ValoresDadoColeta[j];
                        dadosTela[i, j] = listaPaginaAtual[i].ValoresDadoColeta[j].Valor;
                    }
                }

                int totalRegistrosPorOrigemColeta = 1;
                if (listaPaginaAtual.Any())
                {
                    var origemColetaId = listaPaginaAtual[0].ValoresDadoColeta[0].OrigemColetaId;
                    totalRegistrosPorOrigemColeta = listaPaginaAtual.Count(l => l.ValoresDadoColeta
                        .Any(v => v.OrigemColetaId == origemColetaId));
                }

                dadosInformarColetaInsumoModel.Dados = dados;
                dadosInformarColetaInsumoModel.DadosTela = dadosTela;
                dadosInformarColetaInsumoModel.Header = header;

                int rowListPage;
                int.TryParse(ConfigurationManager.AppSettings["grid.rowListPage"], out rowListPage);
                if (rowListPage == 0)
                {
                    rowListPage = 15;
                }

                dadosInformarColetaInsumoModel.TotalPaginas = (int)Math.Ceiling((float)
                    dadosInformarColetaInsumoModel.DadosColetaInsumoPaginado.TotalCount
                        / (totalRegistrosPorOrigemColeta * rowListPage)); //TODO: Quantidade de registros por página
            }
            return headerTextoOut;
        }

        private void CarregarDadosPesquisa(FiltroPesquisaColetaInsumoModel model)
        {
            DadosPesquisaColetaInsumoDTO dadosFiltro = coletaInsumoPresentation
                .ObterDadosPesquisaColetaInsumo(model.IdSemanaOperativa);
            Mapper.DynamicMap(dadosFiltro, model);
        }

        private void CarregarColetaInsumoModel(ColetaInsumoModel model)
        {
            ColetaInsumo coletaInsumo = coletaInsumoService.ObterColetaInsumoInformarDadosPorChave(model.IdColetaInsumo);
            Mapper.DynamicMap(coletaInsumo, model);
        }

        private void CarregarColetaInsumoNaoEstruturadoModel(ColetaInsumoNaoEstruturadoModel model)
        {
            List<UploadFileModel> arquivos = new List<UploadFileModel>();
            arquivos.AddRange(model.UploadFileModels);
            ColetaInsumoNaoEstruturadoModel coletaInsumo = ObterColetaInsumoNaoEstruturadoModel(model.IdColetaInsumo, model.Versao);
            Mapper.DynamicMap(coletaInsumo, model);
            model.UploadFileModels = arquivos;
        }

        [HttpPost]
        [Auditoria("Informar Dados Estudo", "Enviar", typeof(SemanaOperativa))]
        public ActionResult SalvarEnviarDadosEstruturados(IList<ValorDadoColetaModel> dados, ColetaInsumoModel coletaInsumoModel)
        {
            return this.PostarDadosEstruturados(dados, coletaInsumoModel, OperacaoColetaInsumoEnum.Enviar);
        }

        [HttpPost]
        [Auditoria("Informar Dados Estudo", "Informar", typeof(SemanaOperativa))]
        public ActionResult SalvarDadosEstruturados(IList<ValorDadoColetaModel> dados, ColetaInsumoModel coletaInsumoModel)
        {
            return this.PostarDadosEstruturados(dados, coletaInsumoModel, OperacaoColetaInsumoEnum.Salvar);
        }

        private ActionResult PostarDadosEstruturados(IList<ValorDadoColetaModel> dados, ColetaInsumoModel coletaInsumoModel, OperacaoColetaInsumoEnum operacaoColetaInsumo)
        {

            //throw new ONSBusinessException("erro aqui");

            IList<ValorDadoColetaDTO> dtos = new List<ValorDadoColetaDTO>();
            if (dados != null)
            {
                foreach (ValorDadoColetaModel dado in dados)
                {
                    dtos.Add(Mapper.DynamicMap<ValorDadoColetaDTO>(dado));
                }
            }


            switch (operacaoColetaInsumo)
            {
                case OperacaoColetaInsumoEnum.Enviar:
                    SetViewError("InformarDadosEstudo", coletaInsumoModel, CarregarColetaInsumoModel);
                    coletaInsumoService.EnviarColetaDadosEstruturados(dtos, coletaInsumoModel.IdColetaInsumo,
                        coletaInsumoModel.VersaoString);
                    return AjaxSuccessResult(SGIPMOMessages.MS013, ReturnUrl());
                case OperacaoColetaInsumoEnum.Salvar:
                    DadoColetaInsumoDTO dto = Mapper.DynamicMap<DadoColetaInsumoDTO>(coletaInsumoModel);
                    coletaInsumoService.SalvarColetaDadosEstruturados(dtos, dto);
                    break;
            }

            return AjaxSuccessResult(SGIPMOMessages.MS013, GetReturnUrlDecodedPesquisa(coletaInsumoModel.IdColetaInsumo));
        }

        #endregion

        #region Dado Coleta Manutenção
        public ActionResult CarregarGridDadosColetaManutencao(GridSettings gridSettings, ColetaInsumoModel model)
        {
            PagedResult<PesquisaDadoColetaManutencaoModel> resultadoPaginado = null;

            if (ModelStateHandleValid)
            {
                DadoColetaInsumoFilter filter = Mapper.DynamicMap<DadoColetaInsumoFilter>(model);

                Mapper.DynamicMap(gridSettings, filter);

                IList<PesquisaDadoColetaManutencaoModel> dadosColetaList = new List<PesquisaDadoColetaManutencaoModel>();

                var dadosColeta = coletaInsumoService.ConsultarDadoColetaManutencaoPorColetaInsumoPaginado(filter);
                foreach (var dadoColetaManutencao in dadosColeta.List)
                {
                    dadosColetaList.Add(Mapper.DynamicMap<PesquisaDadoColetaManutencaoModel>(dadoColetaManutencao));
                }

                resultadoPaginado = new PagedResult<PesquisaDadoColetaManutencaoModel>(dadosColetaList, dadosColeta.TotalCount,
                    dadosColeta.CurrentPage, dadosColeta.PageSize);
            }

            return JsonToPagedGrid(resultadoPaginado, gridSettings.PageIndex);
        }

        public ActionResult CarregarInclusaoDadoColetaManutencao(int idColetaInsumo, string versaoColetaInsumo)
        {
            DadosInclusaoDadoColetaManutencaoDTO dto =
                coletaInsumoPresentation.ObterDadosInclusaoDadoColetaManutencao(idColetaInsumo);
            InclusaoDadoColetaManutencaoModel model = Mapper.DynamicMap<InclusaoDadoColetaManutencaoModel>(dto);
            model.VersaoColetaInsumo = versaoColetaInsumo;
            return View("_IncluirDadoColetaManutencao", model);
        }

        [HttpPost]
        [Auditoria("Informar Dados Estudo", "Informar", typeof(SemanaOperativa))]
        public ActionResult IncluirDadoColetaManutencao(InclusaoDadoColetaManutencaoModel model)
        {
            if (ModelStateHandleValid)
            {
                InclusaoDadoColetaManutencaoDTO dto = Mapper.DynamicMap<InclusaoDadoColetaManutencaoDTO>(model);
                coletaInsumoService.IncluirDadoColetaManutencao(dto);
            }

            return AjaxSuccessResult(SGIPMOMessages.MS013, GetReturnUrlDecodedPesquisa(model.IdColetaInsumo));
        }

        public ActionResult CarregarAlteracaoDadoColetaManutencao(int idDadoColeta, string versaoColetaInsumo, int idColetaInsumo)
        {
            DadosAlteracaoDadoColetaManutencaoDTO dto =
                coletaInsumoPresentation.ObterDadosAlteracaoDadoColetaManutencao(idDadoColeta);
            AlteracaoDadoColetaManutencaoModel model = Mapper.DynamicMap<AlteracaoDadoColetaManutencaoModel>(dto);
            model.IdColetaInsumo = idColetaInsumo;
            model.VersaoColetaInsumo = versaoColetaInsumo;
            return View("_AlterarDadoColetaManutencao", model);
        }

        [HttpPost]
        [Auditoria("Informar Dados Estudo", "Informar", typeof(SemanaOperativa))]
        public ActionResult AlterarDadoColetaManutencao(AlteracaoDadoColetaManutencaoModel model)
        {
            if (ModelStateHandleValid)
            {
                AlteracaoDadoColetaManutencaoDTO dto = Mapper.DynamicMap<AlteracaoDadoColetaManutencaoDTO>(model);
                coletaInsumoService.AlterarDadoColetaManutencao(dto);
            }

            return AjaxSuccessResult(SGIPMOMessages.MS013, GetReturnUrlDecodedPesquisa(model.IdColetaInsumo));
        }

        public ActionResult ConsultarUnidadeGeradora(int idColetaInsumo, string idUsina)
        {
            IList<UnidadeGeradora> unidadesGeradoras = origemColetaService
                .ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumoUsina(
                    idColetaInsumo, idUsina);

            IList<ChaveDescricaoDTO<string>> dtos = unidadesGeradoras
                .Select(unidadeGeradora => new ChaveDescricaoDTO<string>(unidadeGeradora.Id, unidadeGeradora.Nome))
                .ToList();

            return Json(new { UnidadesGeradoras = dtos }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Auditoria("Informar Dados Estudo", "Informar", typeof(SemanaOperativa))]
        public ActionResult ExcluirDadoColetaManutencao(ExclusaoDadoColetaManutencaoModel model)
        {
            ExclusaoDadoColetaManutencaoDTO dto = Mapper.DynamicMap<ExclusaoDadoColetaManutencaoDTO>(model);
            coletaInsumoService.ExcluirDadoColetaManutencao(dto, 0);

            ColetaInsumoModel coletaInsumoModel = new ColetaInsumoModel { IdColetaInsumo = model.IdColetaInsumo };
            CarregarColetaInsumoModel(coletaInsumoModel);
            string returnUrl = Url.Action("CarregarInformarDados",
                new
                {
                    idColetaInsumo = coletaInsumoModel.IdColetaInsumo,
                    versaoColetaInsumo = coletaInsumoModel.VersaoString
                });
            return AjaxSuccessResult(SGIPMOMessages.MS013, returnUrl);
        }

        /// <summary>
        /// ticket - 189613 -
        /// mover a logica de transformar um registro de manutencao diaria em varios registros de acordo com regras de negocio
        /// a transformacao ocorria ao recuperar os dados do sgiop e foi pedido para que a transformacao so ocorra no momento de
        /// gravar os dados em bancos de dados 
        /// 
        /// metodo criado como static para permitir ser udsado tambem no controller de monitorar dados
        /// </summary>
        /// <param name="manutencao"></param>
        /// <returns></returns>
        internal static List<InclusaoDadoColetaManutencaoDTO> TransformarDadosManutencao(InclusaoDadoColetaManutencaoDTO manutencao)
        {
            List<InclusaoDadoColetaManutencaoDTO> retList = new List<InclusaoDadoColetaManutencaoDTO>();


            if (manutencao.Periodicidade == "D")
            {
                int diffDias = (manutencao.DataFim - manutencao.DataInicio).Days;
                if (diffDias > 0)
                {
                    InclusaoDadoColetaManutencaoDTO confOriginal = manutencao;


                    DateTime instanteInicial = new DateTime(manutencao.DataInicio.Year, manutencao.DataInicio.Month, manutencao.DataInicio.Day
                        , manutencao.DataInicio.Hour, manutencao.DataInicio.Minute, 0);
                    DateTime instanteFinal = new DateTime(manutencao.DataInicio.Year, manutencao.DataInicio.Month, manutencao.DataInicio.Day
                        , manutencao.DataFim.Hour, manutencao.DataFim.Minute, 0);

                    if (instanteFinal <= instanteInicial)
                    {
                        // exemplo
                        // DataInicio 24/07/2024 22:00:00  - dataFim 25/07/2024 10:00:00
                        // instanteInicial = 24/07/2024 22:00:00  - instante final = 24/07/2024 10:00:00


                        DateTime diaInicial = manutencao.DataInicio;
                        int horaInicio = manutencao.DataInicio.Hour;
                        int MinutoInicio = manutencao.DataInicio.Minute;
                        int horaFim = manutencao.DataFim.Hour;
                        int MinutoFim = manutencao.DataFim.Minute;

                        for (int dia = 0; dia <= diffDias; dia++)
                        {

                            InclusaoDadoColetaManutencaoDTO confPartMesmoDia = manutencao;

                            // inicio ate 23:59 do mesmo dia 
                            DateTime dtIniAux = diaInicial.AddDays(dia);
                            DateTime dtIni = new DateTime(dtIniAux.Year, dtIniAux.Month, dtIniAux.Day
                                , horaInicio, MinutoInicio, 0);
                            confPartMesmoDia.DataInicio = dtIni;

                            confPartMesmoDia.DataFim = new DateTime(dtIni.Year
                                    , dtIni.Month
                                    , dtIni.Day
                                    , 23, 59, 59);
                            InclusaoDadoColetaManutencaoDTO dto = ConvertFromService(confPartMesmoDia);
                            retList.Add(dto);


                            // 00:00 do dia seguinte ate o horario final do dia seguinte
                            InclusaoDadoColetaManutencaoDTO confPartDiaSeguinte = manutencao;

                            DateTime dtFimAux = dtIni.AddDays(1);
                            DateTime dtFim = new DateTime(dtFimAux.Year, dtFimAux.Month, dtFimAux.Day
                                , horaFim, MinutoFim, 0);

                            confPartDiaSeguinte.DataInicio = new DateTime(dtFim.Year
                                    , dtFim.Month
                                    , dtFim.Day
                                    , 0, 0, 0);

                            confPartDiaSeguinte.DataFim = dtFim;

                            dto = ConvertFromService(confPartDiaSeguinte);
                            retList.Add(dto);
                        }
                    }
                    else
                    {
                        // exemplo
                        // DataInicio 24/07/2024 08:00:00  - dataFim 25/07/2024 17:00:00
                        // instanteInicial = 24/07/2024 08:00:00  - instante final = 24/07/2024 17:00:00
                        DateTime diaInicial = manutencao.DataInicio;
                        int horaInicio = manutencao.DataInicio.Hour;
                        int MinutoInicio = manutencao.DataInicio.Minute;
                        int horaFim = manutencao.DataFim.Hour;
                        int MinutoFim = manutencao.DataFim.Minute;

                        for (int dia = 0; dia <= diffDias; dia++)
                        {

                            InclusaoDadoColetaManutencaoDTO confPartMesmoDia = manutencao;

                            DateTime dtIniAux = diaInicial.AddDays(dia);
                            DateTime dtIni = new DateTime(dtIniAux.Year, dtIniAux.Month, dtIniAux.Day
                                , horaInicio, MinutoInicio, 0);
                            confPartMesmoDia.DataInicio = dtIni;

                            confPartMesmoDia.DataFim = new DateTime(dtIni.Year
                                    , dtIni.Month
                                    , dtIni.Day
                                    , horaFim, MinutoFim, 0);
                            InclusaoDadoColetaManutencaoDTO dto = ConvertFromService(confPartMesmoDia);
                            retList.Add(dto);

                        }
                    }

                }
                else
                {
                    retList.Add(manutencao);
                }
            }
            else
            {
                retList.Add(manutencao);
            }

            return retList;
        }

        private static InclusaoDadoColetaManutencaoDTO ConvertFromService(InclusaoDadoColetaManutencaoDTO from)
        {
            InclusaoDadoColetaManutencaoDTO dadoColeta = new InclusaoDadoColetaManutencaoDTO();
            dadoColeta.DataInicio = from.DataInicio;
            dadoColeta.DataFim = from.DataFim;
            dadoColeta.TempoRetorno = from.TempoRetorno;
            dadoColeta.Numero = from.Numero;
            dadoColeta.Justificativa = from.Justificativa;
            dadoColeta.Periodicidade = from.Periodicidade;
            dadoColeta.Situacao = from.Situacao;
            dadoColeta.ClassificacaoPorTipoEquipamento = from.ClassificacaoPorTipoEquipamento;
            dadoColeta.IdUnidadeGeradora = from.IdUnidadeGeradora;
            dadoColeta.IdColetaInsumo = from.IdColetaInsumo;
            dadoColeta.IdUsina = from.IdUsina;
            dadoColeta.VersaoColetaInsumo = from.VersaoColetaInsumo;

            return dadoColeta;
        }


        [HttpPost]
        [Auditoria("Informar Dados Estudo", "Informar", typeof(SemanaOperativa))]
        public ActionResult ImportarManutencoes(ExclusaoDadoColetaManutencaoModel model, IList<ImportacaoManutencaoModel> importarModel)
        {
            // Excluir dados de manutenção antigos
            if (null == model.ListaIdsDadoColeta)
            {
                model.ListaIdsDadoColeta = string.Empty;
            }
            model.IdColetaInsumo = importarModel[0].IdColetaInsumo;
            model.VersaoColetaInsumo = importarModel[0].VersaoColetaInsumo;

            ExclusaoDadoColetaManutencaoDTO dto = Mapper.DynamicMap<ExclusaoDadoColetaManutencaoDTO>(model);
            coletaInsumoService.ExcluirDadoColetaManutencao(dto, importarModel[0].IdColetaInsumo);

            // Carregar dados da coleta insumo
            ColetaInsumoModel coletaInsumoModel = new ColetaInsumoModel { IdColetaInsumo = model.IdColetaInsumo };
            CarregarColetaInsumoModel(coletaInsumoModel);

            // Retornar sucesso com o URL de retorno
            string returnUrl = Url.Action("CarregarInformarDados", new
            {
                idColetaInsumo = coletaInsumoModel.IdColetaInsumo,
                versaoColetaInsumo = coletaInsumoModel.VersaoString
            });

            // Importar novos dados de manutenção, se o modelo for válido
            if (ModelState.IsValid)
            {
                IList<InclusaoDadoColetaManutencaoDTO> dtos = new List<InclusaoDadoColetaManutencaoDTO>();
                foreach (ImportacaoManutencaoModel importacao in importarModel)
                {
                    var tratado = TransformarDadosManutencao(Mapper.DynamicMap<InclusaoDadoColetaManutencaoDTO>(importacao));
                    dtos = dtos.Concat(tratado).ToList();
                }
                coletaInsumoService.IncluirDadoColetaManutencaoImportacao(dtos);
            }

            return AjaxSuccessResult(SGIPMOMessages.MS013, returnUrl);
        }

        public ActionResult CarregarImportacaoManutencao(int idColetaInsumo, string versaoColetaInsumo)
        {
            IList<ImportacaoManutencaoModel> models = new List<ImportacaoManutencaoModel>();
            IList<DadoColetaManutencao> dadosColeta = coletaInsumoPresentation.ConsultarManutencaoSGI(idColetaInsumo);
            foreach (DadoColetaManutencao dadoColeta in dadosColeta)
            {
                ImportacaoManutencaoModel model = Mapper.DynamicMap<ImportacaoManutencaoModel>(dadoColeta);
                model.IdColetaInsumo = idColetaInsumo;
                model.VersaoColetaInsumo = versaoColetaInsumo;
                models.Add(model);
            }

            return View("_ImportarManutencao", models);
        }

        [HttpPost]
        [Auditoria("Informar Dados Estudo", "Informar", typeof(SemanaOperativa))]
        public ActionResult ImportarManutencao(IList<ImportacaoManutencaoModel> model)
        {
            if (ModelStateHandleValid)
            {
                IList<InclusaoDadoColetaManutencaoDTO> dtos = new List<InclusaoDadoColetaManutencaoDTO>();
                foreach (ImportacaoManutencaoModel importacao in model)
                {
                    dtos.Add(Mapper.DynamicMap<InclusaoDadoColetaManutencaoDTO>(importacao));
                }
                coletaInsumoService.IncluirDadoColetaManutencaoImportacao(dtos); // em *dtos* tem periodicidade 
            }

            int idColetaInsumo = model.FirstOrDefault().IdColetaInsumo;
            ColetaInsumoModel coletaInsumoModel = new ColetaInsumoModel { IdColetaInsumo = idColetaInsumo }; //aqui não tem periodicidade!!!
            CarregarColetaInsumoModel(coletaInsumoModel);
            string returnUrl = Url.Action("CarregarInformarDados",
                new
                {
                    idColetaInsumo = coletaInsumoModel.IdColetaInsumo,
                    versaoColetaInsumo = coletaInsumoModel.VersaoString
                });
            return AjaxSuccessResult(SGIPMOMessages.MS013, returnUrl);
        }

        [HttpPost]
        [Auditoria("Informar Dados Estudo", "Enviar", typeof(SemanaOperativa))]
        public ActionResult EnviarDadosManutencao(ColetaInsumoModel model)
        {
            if (ModelStateHandleValid)
            {
                EnviarDadosColetaInsumoManutencaoFilter filter =
                    Mapper.DynamicMap<EnviarDadosColetaInsumoManutencaoFilter>(model);
                coletaInsumoService.EnviarDadosColetaInsumoManutencao(filter);
            }

            return AjaxSuccessResult(SGIPMOMessages.MS013, ReturnUrl());
        }

        #endregion

        #region Dados Coleta não-estruturados

        [HttpPost]
        [Auditoria("Informar Dados Estudo", "Informar", typeof(SemanaOperativa))]
        public ActionResult SalvarDadosNaoEstruturados(ColetaInsumoNaoEstruturadoModel model, List<UploadFileModel> uploadFileModels)
        {
            if (ModelStateHandleValid)
            {
                model.UploadFileModels = uploadFileModels;
                SetViewError("InformarDadosEstudoNaoEstruturado", model, CarregarColetaInsumoNaoEstruturadoModel);

                ISet<ArquivoDadoNaoEstruturadoDTO> arquivosColeta = new HashSet<ArquivoDadoNaoEstruturadoDTO>();
                if (model.UploadFileModels != null)
                {
                    foreach (UploadFileModel fileModel in model.UploadFileModels)
                    {
                        arquivosColeta.Add(Mapper.DynamicMap<ArquivoDadoNaoEstruturadoDTO>(fileModel));
                    }
                }

                DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dadosDTO =
                    Mapper.DynamicMap<DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO>(model);
                dadosDTO.Arquivos = arquivosColeta;
                dadosDTO.PreservarSituacaoDadoColeta = true;

                arquivoService.SalvarDadoColetaNaoEstruturada(dadosDTO, null);
                arquivoService.LimparArquivosTemporariosUpload(dadosDTO.Arquivos);

                DisplaySuccessMessage(SGIPMOMessages.MS013);

                if (model.EnviarDadosAoSalvar)
                {
                    return Redirect(ReturnUrl());
                }
            }
            return View("InformarDadosEstudoNaoEstruturado", ObterColetaInsumoNaoEstruturadoModel(model.IdColetaInsumo));
        }

        private ColetaInsumoNaoEstruturadoModel ObterColetaInsumoNaoEstruturadoModel(int idColetaInsumo, byte[] versao = null)
        {
            ColetaInsumoNaoEstruturadoModel modelNaoEstruturado = new ColetaInsumoNaoEstruturadoModel();

            // Consultando os dados não-estruturados do repositório
            DadoColetaInsumoNaoEstruturadoFilter filtros = new DadoColetaInsumoNaoEstruturadoFilter() { IdColetaInsumo = idColetaInsumo };
            DadoColetaNaoEstruturadoDTO dadoColetaNaoEstruturado = coletaInsumoService.ObterDadoColetaNaoEstruturado(filtros);
            if (dadoColetaNaoEstruturado == null)
            {
                ColetaInsumo coletaInsumo = coletaInsumoService.ObterColetaInsumoInformarDadosPorChave(idColetaInsumo);
                return Mapper.DynamicMap<ColetaInsumoNaoEstruturadoModel>(coletaInsumo);
            }
            else
            {
                modelNaoEstruturado = ColetaInsumoNaoEstruturadoModel(dadoColetaNaoEstruturado);
                modelNaoEstruturado.ObservacaoColetaNaoEstruturada = dadoColetaNaoEstruturado.Observacao;
                if (versao != null)
                {
                    modelNaoEstruturado.Versao = versao;
                    modelNaoEstruturado.VersaoString = Convert.ToBase64String(versao);
                }
                return modelNaoEstruturado;
            }
        }

        private ColetaInsumoNaoEstruturadoModel ColetaInsumoNaoEstruturadoModel(DadoColetaNaoEstruturadoDTO dadoColetaNaoEstruturado)
        {
            ColetaInsumoNaoEstruturadoModel modelNaoEstruturado = Mapper.Map<ColetaInsumoNaoEstruturadoModel>(dadoColetaNaoEstruturado);
            foreach (ArquivoDadoNaoEstruturadoDTO arquivo in dadoColetaNaoEstruturado.Arquivos)
            {
                modelNaoEstruturado.UploadFileModels.Add(Mapper.DynamicMap<UploadFileModel>(arquivo));
            }
            return modelNaoEstruturado;
        }

        protected override byte[] DownloadFileDatabase(string id)
        {
            Guid idGuid = Guid.Parse(id);
            return arquivoService.ObterArquivoDadoNaoEstruturadoEmBytes(idGuid);
        }

        protected override ResponseDownload DownloadFilesDatabase(RequestDownload request)
        {
            return arquivoService.ObterArquivosCompactados(request);
        }

        #endregion


        [HttpPost]
        public JsonResult ExibirAlertaSeVolumeInicialIgualAoDaSemanaAnterior(IList<ValorDadoColetaModel> dados, int idColetaConsumo)
        {
            string resposta = string.Empty;

            IList<ValorDadoColetaDTO> dtos = new List<ValorDadoColetaDTO>();
            if (dados != null)
            {
                foreach (ValorDadoColetaModel dado in dados)
                {
                    dtos.Add(Mapper.DynamicMap<ValorDadoColetaDTO>(dado));
                }
            }

            if (dtos.Any())
                resposta = coletaInsumoService.ChecarSeVolumeInicialIgualAoDaSemanaAnterior(dtos, idColetaConsumo);

            return Json(new { Resposta = resposta });
        }


    }


    public class InicioExcel
    {
        public InicioExcel() { }
        public string ColunaInicio { get; set; }
        public string Descricao { get; set; }
        public string ColunaDescricao { get; set; }
        public string Inicio { get; set; }

    }
    public class CabecalhoExcel
    {
        public CabecalhoExcel() { }
        public string Coluna { get; set; }
        public string Descricao { get; set; }
    }
    public class DadosExcel
    {
        public DadosExcel() { }
        public string Coluna { get; set; }
        public string Coluna2 { get; set; }
        public string Descricao { get; set; }
        public string RowsPan { get; set; }
    }
    public class LinhaColunaExcel
    {
        public LinhaColunaExcel() { }
        public string Coluna { get; set; }
        public string linha { get; set; }
    }

}
