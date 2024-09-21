using AutoMapper;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Xml.Linq;

namespace ONS.WEBPMO.Api.Controllers
{

    [WebPermission("ExtracaoLogAuditoria")]
    public class LogAuditoriaController : ControllerBase
    {

        #region Constantes

        public static readonly string KEY_SESSION_EXTRATO_LOG_AUDITORIA = "EXTRATO_LOG_AUDITORIA_RESULT";

        #endregion 

        #region Atributos

        private readonly IColetaInsumoPresentation presentationColetaInsumo;
        private readonly ISemanaOperativaService serviceSemanaOperativa;
        private readonly ILogAuditoriaService serviceAuditoria;
        private readonly IAgenteService serviceAgente;
        private readonly IColetaInsumoService serviceColetaInsumo;

        #endregion

        #region Propriedades

        #endregion

        #region Construtores

        public LogAuditoriaController(IColetaInsumoPresentation coletaInsumoPresentation
            , ISemanaOperativaService semanaOperativaService, ILogAuditoriaService logAuditoriaService
            , IAgenteService agenteService, IColetaInsumoService coletaInsumoService)
        {
            this.presentationColetaInsumo = coletaInsumoPresentation;
            this.serviceSemanaOperativa = semanaOperativaService;
            this.serviceAuditoria = logAuditoriaService;
            this.serviceAgente = agenteService;
            this.serviceColetaInsumo = coletaInsumoService;
        }

        #endregion

        #region Metodos

        #region GET

        public ActionResult Index()
        {
            var dadosFiltro = presentationColetaInsumo.ObterDadosPesquisaColetaInsumo();
            var model = Mapper.Map<PesquisaColetaInsumoModel>(dadosFiltro);
            model.IdsSituacaoColeta.Add(int.Parse(model.SituacoesColeta.FirstOrDefault(s => s.Text == "Informado").Value));
            return View(model);
        }

        public ActionResult CarregarDadosEstudo(int? idSemanaOperativa)
        {
            var dto = presentationColetaInsumo.ObterDadosPesquisaColetaInsumo(idSemanaOperativa, true);
            return Json(new { dto.Agentes, dto.Insumos }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CarregarPesquisaLogAuditoria(FiltroPesquisaColetaInsumoModel pesquisaColetaInsumoModel)
        {
            var dadosFiltro = presentationColetaInsumo.ObterDadosPesquisaColetaInsumo(pesquisaColetaInsumoModel.IdSemanaOperativa);
            var model = Mapper.Map<PesquisaColetaInsumoModel>(dadosFiltro);

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

        public ActionResult CarregarGridLogAuditoria(GridSettings gridSettings, PesquisaColetaInsumoModel model)
        {
            PagedResult<ExtracaoLogAuditoriaModel> resultadoPaginado = null;

            if (ModelStateHandleValid)
            {
                resultadoPaginado = this.RecuperarLogs(model, gridSettings.PageIndex, gridSettings.PageSize);
            }

            return JsonToPagedGrid(resultadoPaginado, gridSettings.PageIndex);
        }


        private PagedResult<ExtracaoLogAuditoriaModel> RecuperarLogs(PesquisaColetaInsumoModel model, int pageIndex, int pageSize)
        {
            if (this.Session[KEY_SESSION_EXTRATO_LOG_AUDITORIA] != null)
                this.Session.Remove(KEY_SESSION_EXTRATO_LOG_AUDITORIA);

            var listaLogsExtraidos = new Dictionary<string, ExtracaoLogAuditoriaModel>();

            // recupera dados do estudo
            var dto = presentationColetaInsumo.ObterDadosPesquisaColetaInsumo(model.IdSemanaOperativa, true);

            // identifica lote de pesquisa
            // se lote  =  0, assume valor max
            var loteStr = ConfigurationManager.AppSettings["loteExportacaoLogsAuditoria"];
            var lote = string.IsNullOrWhiteSpace(loteStr) ? int.MaxValue : Convert.ToInt32(loteStr);
            lote = lote <= 0 ? int.MaxValue : lote;

            // cria filtro para consulta de logs de auditoria
            var filter = new LogAuditoriaFilter();
            filter.CodigoAplicacao = "SGIPMO";
            filter.NomeRegistro = dto.SemanasOperativas.First().Descricao;
            filter.TipoAcaoExecutada = "Informar";
            filter.PageIndex = 1;
            filter.PageSize = lote; // para recuperar todos os registros e realizar a paginacao após processamento

            // consulta de logs de auditoria
            var listaLogs = serviceAuditoria.ConsultarLogsAuditoriaPaginado(filter);

            // processa resultado da consulta
            if (listaLogs != null)
            {
                // seta todos ids de agentes caso usuario nao tenha selecionado nenhum na tela
                if (model.IdsAgentes.Count == 0)
                    model.IdsAgentes = dto.Agentes.Select(a => a.Chave).ToList();

                // seta todos ids de insumo caso usuario nao tenha selecionado nenhum na tela
                if (model.IdsInsumo.Count == 0)
                    model.IdsInsumo = dto.Insumos.Select(i => i.Chave).ToList();

                //processa lista de logs da aplicacao
                listaLogs.List.ToList().ForEach(log =>
                {

                    // obtem xml opercao para analisar
                    log.LogXmlOperacao = serviceAuditoria.ObterOperacoesXmlPorChaveLogAuditoria(log.Id);

                    // converte xml operacao para lista de objetos dinamicos
                    var entidades = CriarListaObjetoDinamico(log.LogXmlOperacao);

                    // identifica primeira entidade com fields
                    var entidadesFiltered = entidades.Where(e => e.ContainsField("coletainsumoid", "agente", "insumo"));

                    // processa objetos 
                    if (entidadesFiltered != null)
                    {
                        entidadesFiltered.ToList().ForEach(entidade =>
                        {

                            if (entidade != null)
                            {
                                var atributos = entidade.FieldValue as List<DynamicObject>;

                                var coletainsumoid = atributos.FirstOrDefault(a => a.FieldName.ToLower() == "coletainsumoid");
                                var agente = atributos.FirstOrDefault(a => a.FieldName.ToLower() == "agente");
                                var insumo = atributos.FirstOrDefault(a => a.FieldName.ToLower() == "insumo");
                                var origemColeta = atributos.FirstOrDefault(a => a.FieldName.ToLower() == "origem da coleta");
                                var unidadeGeradora = atributos.FirstOrDefault(a => a.FieldName.ToLower() == "unidadegeradora");
                                if (coletainsumoid != null)
                                {
                                    // recupera dados da coleta de insumo
                                    var coletaInsumo = serviceColetaInsumo.ObterPorChave(Convert.ToInt32(coletainsumoid.FieldValue));

                                    if (model.IdsAgentes.Contains(coletaInsumo.AgenteId)
                                        && model.IdsInsumo.Contains(coletaInsumo.InsumoId))
                                    {
                                        var itemLog = new ExtracaoLogAuditoriaModel
                                        {
                                            Id = string.Format("{0}_{1}", model.IdSemanaOperativa, log.Id),
                                            Agente = agente.FieldValue != null ? agente.FieldValue.ToString() : "",
                                            Insumo = insumo.FieldValue != null ? insumo.FieldValue.ToString() : "",
                                            DataExecucao = log.DataExecucao,
                                            Estudo = dto.SemanasOperativas.First().Descricao,
                                            Executor = log.NomeExecutor,
                                            OrigemColeta = string.Empty
                                        };

                                        if (origemColeta != null && origemColeta.FieldValue != null)
                                        {
                                            itemLog.OrigemColeta = origemColeta.FieldValue.ToString();
                                        }
                                        else if (unidadeGeradora != null && unidadeGeradora.FieldValue != null)
                                        {
                                            itemLog.OrigemColeta = unidadeGeradora.FieldValue.ToString();
                                        }

                                        if (!listaLogsExtraidos.ContainsKey(itemLog.IdLogico))
                                            listaLogsExtraidos.Add(itemLog.IdLogico, itemLog);
                                    }
                                }
                            }

                        });

                    };
                });
            }

            // executa filtro de paginacao
            var pagina = listaLogsExtraidos.Values.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            if (pagina.Count > 0)
                this.Session[KEY_SESSION_EXTRATO_LOG_AUDITORIA] = pagina;

            return new PagedResult<ExtracaoLogAuditoriaModel>(pagina, listaLogsExtraidos.Count, pageIndex, pageSize);
        }

        private List<DynamicObject> CriarListaObjetoDinamico(string xmlOperacao)
        {

            return (from entidade in XElement.Parse(xmlOperacao).Descendants("Entidade")
                    where Convert.ToBoolean(entidade.Attribute("Visivel").Value)
                    select CriarObjetoDinamico(entidade)).ToList();
        }


        private DynamicObject CriarObjetoDinamico(XElement element)
        {
            DynamicObject entidade = null;
            if (element != null)
            {
                IList<DynamicObject> registrosAlterados =
                    (from atributo in element.Descendants("Atributo") //  where Convert.ToBoolean(atributo.Attribute("Visivel").Value)                    
                     select MontarRegistro(atributo)).ToList();

                entidade = new DynamicObject
                {
                    FieldName = string.Format("{0} - {1} - {2}",
                                       element.Attribute("NomeExterno").Value,
                                       element.Attribute("Identificacao").Value,
                                       element.Attribute("Acao").Value),
                    FieldValue = registrosAlterados
                };
            }
            return entidade;
        }

        private DynamicObject MontarRegistro(XElement element)
        {
            DynamicObject registro = null;
            if (element != null)
            {
                registro = new DynamicObject
                {
                    FieldName = element.Attribute("NomeExterno").Value,
                    FieldValue = element.Element("Novo").Attribute("Exibicao").Value
                };
            }
            return registro;
        }


        private class DynamicObject
        {

            public string FieldName { get; set; }
            public object FieldValue { get; set; }

            public bool ContainsField(params string[] names)
            {
                var result = false;
                if (names != null && names.Count() > 0)
                {
                    if (typeof(ICollection).IsAssignableFrom(FieldValue.GetType()) ||
                        typeof(ICollection).IsInstanceOfType(FieldValue))
                    {
                        var fields = FieldValue as List<DynamicObject>;
                        foreach (var name in names)
                        {
                            result = fields.Any(f => f.FieldName.ToLower().Equals(name.ToString().ToLower()));
                            if (!result)
                                break;
                        }
                    }
                }
                return result;
            }
        }

        [WebPermission("ExtracaoLogAuditoria", IgnorePermissionsInClass = true)]
        [Auditoria("Extracao de Log de Auditoria", "ExportarExcel", typeof(SemanaOperativa))]
        public ActionResult ExportarExcel(string SelRowIds, GridSettings gridSettings, ColetaInsumoModel model)
        {
            try
            {
                string UrlDownloadExcel = string.Empty;

                List<string> ListIdsGrid = new List<string>();
                List<string> ListIdsPodeExportar = new List<string>();
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
                            ListIdsGrid.Add(IDs);
                        }
                    }
                    else
                    {
                        ListIdsGrid.Add(SelRowIds);
                    }
                }

                var logs = this.Session[KEY_SESSION_EXTRATO_LOG_AUDITORIA] as List<ExtracaoLogAuditoriaModel>;

                //Varrer a lista de Ids das linhas selecionadas da Grid
                foreach (var IdsGrid in ListIdsGrid)
                {
                    //Consulta a Linha da Grid
                    //ColetaInsumo dadosColetaInsumo = coletaInsumoService.ObterColetaInsumoInformarDadosPorChave(IdsGrid);
                    var item = logs.FirstOrDefault(o => o.Id == IdsGrid);
                    string AgenteExcel = item.Agente;
                    string EstudoExcel = item.Estudo;
                    string InsumoExel = item.Insumo;
                    string ExecutorExel = item.Executor;
                    DateTime DataExecucaoExel = item.DataExecucao;
                    string OrigemColetaExel = item.OrigemColeta;


                    //Guarda na lista os Ids das linhas selecionadas da Grid, que pode ser expotados (estruturado).
                    ListIdsPodeExportar.Add(IdsGrid);

                    //Contagem para monstar a mensagem.
                    cont++;

                }

                //Cria um array com os Ids das linhas selecionadas da Grid, que pode ser expotados (estruturado).
                string[] ArrayPodeExportar = new string[ListIdsPodeExportar.Count];
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

                return File((Byte[])btExcel, "application/vnd.ms-excel", NomeExcel);
            }
            catch (Exception ex)
            {
                throw new ONSBusinessException(ex.Message, ReturnUrl());
            }
        }


        private Dictionary<string, byte[]> MontarExcel(string[] selRowIds, GridSettings gridSettings)
        {
            try
            {
                ExcelPackage pck = new ExcelPackage();

                //string AgenteExcel = string.Empty;
                string EstudoExcel = string.Empty;

                var logs = this.Session[KEY_SESSION_EXTRATO_LOG_AUDITORIA] as List<ExtracaoLogAuditoriaModel>;

                #region MONTA CABEÇALHO FIXO

                List<CabecalhoExcel> listCabecalho = new List<CabecalhoExcel>();
                listCabecalho.Add(new CabecalhoExcel() { Coluna = "A", Descricao = "Agente" });
                listCabecalho.Add(new CabecalhoExcel() { Coluna = "B", Descricao = "Estudo" });
                listCabecalho.Add(new CabecalhoExcel() { Coluna = "C", Descricao = "Data de Execução" });
                listCabecalho.Add(new CabecalhoExcel() { Coluna = "D", Descricao = "Executor" });
                listCabecalho.Add(new CabecalhoExcel() { Coluna = "E", Descricao = "Insumo" });
                listCabecalho.Add(new CabecalhoExcel() { Coluna = "F", Descricao = "Origem Coleta" });

                #endregion

                #region MONTA DADOS DO EXCEL DINAMICAMENTE

                string Alfabeto = "ABCDEFGHIJKLMNOPQRSTUVXZ";
                List<DadosExcel> listDadosExcel = new List<DadosExcel>();

                if (logs.Count == 0)
                {
                    int i = 0;
                    int row = listCabecalho.Count;
                    int j = i + (row - 1);
                    listDadosExcel.Add(new DadosExcel() { Coluna = Alfabeto.Substring(i, 1), Coluna2 = Alfabeto.Substring(j, 1), Descricao = SGIPMOMessages.MS004, RowsPan = row.ToString() });
                }
                else
                {

                    EstudoExcel = logs.First().Estudo;

                    for (int k = 0; k < selRowIds.Length; k++)
                    {
                        var selRowIds_ = selRowIds[k];

                        #region CONSULTA A LINHA DA GRID

                        var item = logs.FirstOrDefault(o => o.Id == selRowIds_);

                        #endregion

                        int i = 0;
                        listDadosExcel.Add(new DadosExcel() { Coluna = Alfabeto.Substring(i, 1), Descricao = item.Agente, RowsPan = "0" });
                        i++;
                        listDadosExcel.Add(new DadosExcel() { Coluna = Alfabeto.Substring(i, 1), Descricao = item.Estudo, RowsPan = "0" });
                        i++;
                        listDadosExcel.Add(new DadosExcel() { Coluna = Alfabeto.Substring(i, 1), Descricao = item.DataExecucao.ToString("dd/MM/yyyy HH:mm:ss"), RowsPan = "0" });
                        i++;
                        listDadosExcel.Add(new DadosExcel() { Coluna = Alfabeto.Substring(i, 1), Descricao = item.Executor, RowsPan = "0" });
                        i++;
                        listDadosExcel.Add(new DadosExcel() { Coluna = Alfabeto.Substring(i, 1), Descricao = item.Insumo, RowsPan = "0" });
                        i++;
                        listDadosExcel.Add(new DadosExcel() { Coluna = Alfabeto.Substring(i, 1), Descricao = item.OrigemColeta, RowsPan = "0" });

                    }
                }

                #endregion

                #region ESCREVER EXCEL

                //Em que linha começa a escrever o conteudo do excel (Cabeçalho e Dados)
                int linha = 1;

                ExcelWorksheet abas = pck.Workbook.Worksheets.Add("Logs Auditoria");

                foreach (var itemCab in listCabecalho)
                {
                    var Cabecalho = abas.Cells[itemCab.Coluna + linha.ToString()];
                    Cabecalho.Value = itemCab.Descricao;

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

                foreach (var itemCorpo in listDadosExcel)
                {
                    ExcelRange Celulas;
                    LinhaColunaExcel linhaColunaExcel = new LinhaColunaExcel();

                    // Verifica se existe linha na coluna
                    var existe_linhaColunaExcel = listLinhaColunaExcel.Where(x => x.Coluna.Equals(itemCorpo.Coluna)).FirstOrDefault();

                    //existindo Linha na coluna
                    if (!(existe_linhaColunaExcel == null))
                    {
                        linhaColunaExcel = existe_linhaColunaExcel;
                        atualLinha = int.Parse(linhaColunaExcel.linha);
                        listLinhaColunaExcel.Remove(linhaColunaExcel);
                    }

                    if (int.Parse(itemCorpo.RowsPan) > 0)
                    {
                        proximaLinha = int.Parse(itemCorpo.RowsPan);
                        Celulas = abas.Cells[itemCorpo.Coluna + atualLinha + ":" + itemCorpo.Coluna2 + proximaLinha];
                        Celulas.Merge = true;
                        Celulas.Value = itemCorpo.Descricao;

                        Celulas.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Celulas.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        Celulas.Style.Border.BorderAround(ExcelBorderStyle.Medium, System.Drawing.Color.FromArgb(70, 198, 128));

                        //Pegar Maior String de Cada Coluna
                        listLinhaColunaExcelSizeExcel.Add(new LinhaColunaExcel { Coluna = itemCorpo.Coluna, linha = itemCorpo.Descricao });
                    }
                    else
                    {
                        proximaLinha = (int.Parse(itemCorpo.RowsPan) + atualLinha);

                        Celulas = abas.Cells[itemCorpo.Coluna + atualLinha];
                        Celulas.Value = itemCorpo.Descricao;

                        Celulas.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Celulas.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        Celulas.Style.Border.BorderAround(ExcelBorderStyle.Medium, System.Drawing.Color.FromArgb(70, 198, 128));

                        //Pegar Maior String de Cada Coluna
                        listLinhaColunaExcelSizeExcel.Add(new LinhaColunaExcel { Coluna = itemCorpo.Coluna, linha = itemCorpo.Descricao });
                    }

                    proximaLinha++;
                    linhaColunaExcel.Coluna = itemCorpo.Coluna;
                    linhaColunaExcel.linha = proximaLinha.ToString();
                    listLinhaColunaExcel.Add(linhaColunaExcel);

                }

                #endregion

                #region PEGAR MAIOR STRING DE CADA COLUNA

                //Pegar Maior String de Cada Coluna
                if (listDadosExcel.Count == 0)
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

                #region BLOQUEAR EXCEL E COLOCAR SENHA

                //pck.Encryption.Password = "EPPlus";
                //pck.Encryption.Algorithm = EncryptionAlgorithm.AES192;
                pck.Workbook.Protection.SetPassword(ConfigurationManager.AppSettings["senhaExcel"]);
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
                    ew.Protection.SetPassword(ConfigurationManager.AppSettings["senhaExcel"]);
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

                DateTime DiaHoraArquivo = DateTime.Now;
                string DiaHoraArquivoTratada = "_" + DiaHoraArquivo.ToString("yyyyMMddHHmmss"); //_20150401095915

                string fimNome = DiaHoraArquivoTratada; //_FUR_20150401095915

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

        /*
        [WebPermission("ConsultarLogAuditoria", IgnorePermissionsInClass = true)]
        [Auditoria("Informar ConsultarLogAuditoria Estudo", "ExportarExcel", typeof(SemanaOperativa))]
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

                var lista = RecuperarDados();
                //Varrer a lista de Ids das linhas selecionadas da Grid
                foreach (var IdsGrid in ListIdsGrid)
                {
                    //Consulta a Linha da Grid
                    //ColetaInsumo dadosColetaInsumo = coletaInsumoService.ObterColetaInsumoInformarDadosPorChave(IdsGrid);
                    var item = lista.FirstOrDefault( o => o.Id == IdsGrid );
                    string AgenteExcel = item.Agente;
                    string EstudoExcel = item.Estudo;
                    string InsumoExel = item.Insumo;
                    string ExecutorExel = item.Executor;
                    DateTime DataExecucaoExel = item.DataExecucao;
                    string OrigemColetaExel = item.OrigemColeta;
                    bool exportarInsumo = item.Exportar;

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
         * */

        #endregion

        #region POST

        [HttpPost]
        public ActionResult Pesquisar(PesquisaColetaInsumoModel model)
        {
            if (ModelStateHandleValid)
            {
                var semanaOperativa = serviceSemanaOperativa.ObterSemanaOperativaPorChave(model.IdSemanaOperativa.Value);

                model.NomeSemanaOperativaSituacao = semanaOperativa.Situacao == null ? semanaOperativa.Nome
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
            }

            return PartialView("_PesquisaLogAuditoria", model);
        }

        #endregion

        #endregion
    }
}
