using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using Newtonsoft.Json;
using ONS.Common.Entities;
using ONS.Common.Seguranca;
using ONS.Common.Util.Files;
using ONS.Common.Util.Pagination;

using ONS.Common.Web.Helpers;
using ONS.SGIPMO.Domain.Entities;
using ONS.SGIPMO.Domain.Entities.DTO;
using ONS.SGIPMO.Domain.Entities.Filters;
using ONS.SGIPMO.Domain.Presentations;
using ONS.SGIPMO.Domain.Services;
using ONS.SGIPMO.WebSite.Models;
using ONS.SGIPMO.WebSite.Models.ColetaInsumo;
using System.Configuration;
using ONS.Common.Exceptions;
using ONS.Common.Util.Collection;

namespace ONS.WEBPMO.Api.Controllers
{
    [WebPermission("MonitorarEstudo")]
    public class MonitorarDadosEstudoController : ControllerBase
    {
        private readonly IColetaInsumoPresentation coletaInsumoPresentation;
        private readonly ISemanaOperativaService semanaOperativaService;
        private readonly IColetaInsumoService coletaInsumoService;
        private readonly IOrigemColetaService origemColetaService;
        private readonly IGeracaoBlocosService geracaoBlocosService;
        private readonly IArquivoService arquivoService;

        public MonitorarDadosEstudoController(
            IColetaInsumoPresentation coletaInsumoPresentation,
            ISemanaOperativaService semanaOperativaService,
            IColetaInsumoService coletaInsumoService,
            IOrigemColetaService origemColetaService,
            IGeracaoBlocosService geracaoBlocosService,
            IArquivoService arquivoService)
        {
            this.coletaInsumoPresentation = coletaInsumoPresentation;
            this.semanaOperativaService = semanaOperativaService;
            this.coletaInsumoService = coletaInsumoService;
            this.origemColetaService = origemColetaService;
            this.geracaoBlocosService = geracaoBlocosService;
            this.arquivoService = arquivoService;
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
            PesquisaColetaInsumoModel model = CarregarPesquisa(pesquisaColetaInsumoModel);
            var semanaOperativa = semanaOperativaService.ObterSemanaOperativaPorChave(model.IdSemanaOperativa.HasValue ? model.IdSemanaOperativa.Value : 0);
            if (semanaOperativa != null)
                model.EstaEmColetaDeDados = semanaOperativa.Situacao.Id == (int)SituacaoSemanaOperativaEnum.ColetaDados;

            return View("Index", model);
        }

        private PesquisaColetaInsumoModel CarregarPesquisa(FiltroPesquisaColetaInsumoModel pesquisaColetaInsumoModel)
        {
            PesquisaColetaInsumoModel model = new PesquisaColetaInsumoModel();

            DadosPesquisaColetaInsumoDTO dadosFiltro =
                coletaInsumoPresentation.ObterDadosPesquisaColetaInsumo(pesquisaColetaInsumoModel.IdSemanaOperativa, true);
            model = Mapper.Map<PesquisaColetaInsumoModel>(dadosFiltro);

            if (pesquisaColetaInsumoModel.IdSemanaOperativa.HasValue)
            {
                model.IdSemanaOperativa = pesquisaColetaInsumoModel.IdSemanaOperativa;
                model.IdsAgentes = pesquisaColetaInsumoModel.IdsAgentes;
                model.IdsInsumo = pesquisaColetaInsumoModel.IdsInsumo;
                model.IdsSituacaoColeta = pesquisaColetaInsumoModel.IdsSituacaoColeta;
                model.SituacaoSemanaOperativa = pesquisaColetaInsumoModel.SituacaoSemanaOperativa;
                model.EtapaSelecionada = pesquisaColetaInsumoModel.EtapaSelecionada;
                model.isGerarBlocoParaEncerrar = pesquisaColetaInsumoModel.isGerarBlocoParaEncerrar;
            }

            return model;
        }

        public ActionResult ConsultarEstudo(string term)
        {
            var semanasOperativas = semanaOperativaService.ConsultarEstudoPorNome(term);

            return Json(semanasOperativas.Select(s => new { label = s.Nome, id = s.Id }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Pesquisar(PesquisaColetaInsumoModel model)
        {
            if (ModelStateHandleValid)
            {
                SemanaOperativa semanaOperativa =
                    semanaOperativaService.ObterSemanaOperativaPorChave(model.IdSemanaOperativa.Value);

                model.NomeSemanaOperativaSituacao = string.Format("{0} - {1}", semanaOperativa.Nome,
                    semanaOperativa.Situacao.Descricao);
                model.IsSemanaOperativaEmConfiguracao = semanaOperativa.Situacao.Id == (int)SituacaoSemanaOperativaEnum.Configuracao;
                model.VersaoStringSemanaOperativa = Convert.ToBase64String(semanaOperativa.Versao);
                model.EstaEmColetaDeDados = semanaOperativa.Situacao.Id == (int)SituacaoSemanaOperativaEnum.ColetaDados;
            }

            return PartialView("_PesquisaMonitoramento", model);
        }

        public ActionResult CarregarDadosEstudo(int? idSemanaOperativa)
        {
            DadosPesquisaColetaInsumoDTO dto = coletaInsumoPresentation.ObterDadosPesquisaColetaInsumo(idSemanaOperativa, true);
            return Json(new { dto.Agentes, dto.Insumos }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PesquisaColetaInsumo(PesquisaColetaInsumoModel model)
        {
            return PartialView("_PesquisaColetaInsumo", model);
        }

        public ActionResult CarregarMonitorarDados(InformaDadosColetaInsumoModel model)
        {
            PesquisaColetaInsumoModel pesquisaModel = new PesquisaColetaInsumoModel();
            SetViewError("Index", pesquisaModel, CarregarDadosPesquisa);

            ColetaInsumoModel modelColeta = new ColetaInsumoModel { IdColetaInsumo = model.IdColetaInsumo };
            CarregarColetaInsumoModel(modelColeta);

            return View("MonitorarDadoColetaManutencao", modelColeta);
        }

        private void CarregarDadosPesquisa(FiltroPesquisaColetaInsumoModel model)
        {
            DadosPesquisaColetaInsumoDTO dadosFiltro = coletaInsumoPresentation
                .ObterDadosPesquisaColetaInsumo(model.IdSemanaOperativa, true);
            Mapper.Map(dadosFiltro, model);
            var semanaOperativa = semanaOperativaService.ObterSemanaOperativaPorChave(model.IdSemanaOperativa.HasValue ? model.IdSemanaOperativa.Value : 0);
            if (semanaOperativa != null)
                model.EstaEmColetaDeDados = semanaOperativa.Situacao.Id == (int)SituacaoSemanaOperativaEnum.ColetaDados;
            model.VersaoStringSemanaOperativa = dadosFiltro.VersaoStringSemanaOperativa;
        }

        private void CarregarColetaInsumoModel(ColetaInsumoModel model)
        {
            ColetaInsumo coletaInsumo = coletaInsumoService.ObterColetaInsumoInformarDadosPorChave(model.IdColetaInsumo);
            Mapper.DynamicMap(coletaInsumo, model);
        }

        private void CarregarColetaInsumoNaoEstruturadoModel(ColetaInsumoNaoEstruturadoModel model)
        {
            List<UploadFileModel> arquivos = new List<UploadFileModel>();
            if (model.UploadFileModels != null) arquivos.AddRange(model.UploadFileModels);

            ColetaInsumoNaoEstruturadoModel coletaInsumo = ObterColetaInsumoNaoEstruturadoModel(model.IdColetaInsumo, model.Versao);
            Mapper.DynamicMap(coletaInsumo, model);
            model.UploadFileModels = arquivos;
        }

        [Auditoria("Monitorar Coleta", "Capturar Coleta", typeof(SemanaOperativa))]
        public ActionResult CapturarDadosColetaInsumo(CapturarDadosColetaInsumoModel model)
        {
            PesquisaColetaInsumoModel pesquisaModel = CastPesquisaColetaInsumoModel(model);
            SetViewError("Index", pesquisaModel, CarregarDadosPesquisa);

            DadosMonitoramentoColetaInsumoDTO dto = new DadosMonitoramentoColetaInsumoDTO();
            for (int i = 0; i < model.IdsColetaInsumo.Count; i++)
            {
                dto.IdsColetaInsumoCapturaVersaoString.Add(new KeyValuePair<int, string>(model.IdsColetaInsumo[i], model.VersoesColetaInsumoString[i]));
            }
            coletaInsumoService.CapturarColetaDados(dto);

            return AjaxSuccessResult(SGIPMOMessages.MS013, ReturnUrl());
        }

        public ActionResult VisualizarColetaInsumo(InformaDadosColetaInsumoModel visualizarModel)
        {
            PesquisaColetaInsumoModel pesquisaModel = new PesquisaColetaInsumoModel();
            Mapper.DynamicMap(visualizarModel, pesquisaModel);
            SetViewError("Index", pesquisaModel, CarregarDadosPesquisa);

            ColetaInsumo coletaInsumo = coletaInsumoService.ObterColetaInsumoInformarDadosPorChave(visualizarModel.IdColetaInsumo);
            ColetaInsumoModel model = Mapper.DynamicMap<ColetaInsumoModel>(coletaInsumo);
            model.Situacao = model.SituacaoColetaInsumoDescricao;

            // Chamando a view com o model de acordo com o tipo de coleta
            string viewName = "VisualizarColetaInsumoEstruturado";
            if (coletaInsumo.Insumo.Reservado)
            {
                viewName = "VisualizarColetaInsumoManutencao";
            }
            else if (coletaInsumo.Insumo.TipoInsumo.Equals("L"))
            {
                model = ObterColetaInsumoNaoEstruturadoModel(visualizarModel.IdColetaInsumo);
                viewName = "VisualizarColetaInsumoNaoEstruturado";
            }
            return View(viewName, model);
        }

        public ActionResult AnalisarColetaInsumo(InformaDadosColetaInsumoModel analisarModel)
        {
            SetViewError("Index", CastPesquisaColetaInsumoModel(analisarModel), CarregarDadosPesquisa);

            ColetaInsumo coletaInsumo = coletaInsumoService.ObterValidarColetaInsumoMonitorarDadosPorChave(analisarModel.IdColetaInsumo, analisarModel.IdSituacaoColeta);
            string[] idsInsumosRecuperarDados = ConfigurationManager.AppSettings["InsumosRecuperarDados"].Split(';');
            foreach (var item in idsInsumosRecuperarDados)
            {
                if (int.Parse(item) == coletaInsumo.InsumoId)
                {
                    ViewBag.RecuperarDados = true;
                    break;
                }
            }
            ColetaInsumoModel model = Mapper.DynamicMap<ColetaInsumoModel>(coletaInsumo);

            // Chamando a view com o model de acordo com o tipo de coleta
            string viewName = "AnalisarColetaInsumoEstruturado";
            if (coletaInsumo.Insumo.Reservado)
            {
                viewName = "MonitorarDadoColetaManutencao";
            }
            else if (coletaInsumo.Insumo.TipoInsumo.Equals("L"))
            {
                model = ObterColetaInsumoNaoEstruturadoModel(analisarModel.IdColetaInsumo);
                viewName = "AnalisarColetaInsumoNaoEstruturado";
            }

            return View(viewName, model);
        }

        private PesquisaColetaInsumoModel CastPesquisaColetaInsumoModel(FiltroPesquisaColetaInsumoModel model)
        {
            PesquisaColetaInsumoModel pesquisaModel = new PesquisaColetaInsumoModel();
            Mapper.DynamicMap(model, pesquisaModel);
            return CarregarPesquisa(model);
        }

        /// <summary>
        /// Método útil para requisições Ajax que precisam manter os filtros com atualização da versão de semana operativa
        /// </summary>
        /// <param name="idSemanaOperativa"></param>
        /// <returns></returns>
        private string GetReturnUrlDecodedPesquisa(int idSemanaOperativa, EtapaMonitoramento etapa, bool isGerarBlocoParaEncerrar = false)
        {
            PesquisaColetaInsumoModel modelPesquisa = new PesquisaColetaInsumoModel() { IdSemanaOperativa = idSemanaOperativa };
            CarregarDadosPesquisa(modelPesquisa);
            modelPesquisa.EtapaSelecionada = etapa;
            modelPesquisa.isGerarBlocoParaEncerrar = isGerarBlocoParaEncerrar;
            return Server.UrlDecode(Url.Action("CarregarPesquisaColetaInsumo", "MonitorarDadosEstudo") + "?" + modelPesquisa.AsQueryString());
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

                resultadoPaginado = coletaInsumoService.ConsultarColetasInsumoParaMonitorarDadosPaginado(filter);
            }

            return JsonToPagedGrid(resultadoPaginado, gridSettings.PageIndex);
        }

        #endregion

        #region Dado Coleta Estruturado

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

        private void PreencherDados(DadosInformarColetaInsumoModel dadosInformarColetaInsumoModel)
        {
            IList<DadoColetaModel> listaPaginaAtual = dadosInformarColetaInsumoModel.DadosColetaInsumoPaginado.List;

            int x = listaPaginaAtual.Count();
            int y = listaPaginaAtual.Max(d => d.ValoresDadoColeta.Count);

            string[,] dadosTela = new string[x, y];
            string[] headerTexto = new string[y];
            string[] headerTitle = new string[y];
            ValorDadoColetaModel[,] dados = new ValorDadoColetaModel[x, y];


            // Preencher Cabeçalho.
            for (int j = 0; j < y; j++)
            {
                headerTexto[j] = listaPaginaAtual[0].ValoresDadoColeta[j].Estagio;
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

        [HttpPost]
        [Auditoria("Monitorar Coleta", "Alterar Coleta", typeof(SemanaOperativa))]
        public ActionResult SalvarDadosEstruturados(IList<ValorDadoColetaModel> dados, ColetaInsumoModel coletaInsumoModel)
        {
            if (dados == null)
            {
                dados = new List<ValorDadoColetaModel>();
            }
            if (dados.Any() || !string.IsNullOrWhiteSpace(coletaInsumoModel.MotivoAlteracaoONS))
            {
                IList<ValorDadoColetaDTO> dtos = new List<ValorDadoColetaDTO>();
                foreach (ValorDadoColetaModel dado in dados)
                {
                    dtos.Add(Mapper.Map<ValorDadoColetaDTO>(dado));
                }
                DadoColetaInsumoDTO dto = Mapper.DynamicMap<DadoColetaInsumoDTO>(coletaInsumoModel);
                dto.IsMonitorar = true;
                coletaInsumoService.SalvarColetaDadosEstruturados(dtos, dto);
            }
            return AjaxSuccessResult(SGIPMOMessages.MS013, true);
        }

        [HttpPost]
        [Auditoria("Monitorar Coleta", "Aprovar Coleta", typeof(SemanaOperativa))]
        public ActionResult AprovarDadosEstruturados(ColetaInsumoModel coletaInsumoModel, IList<ValorDadoColetaModel> dados)
        {
            if (ModelStateHandleValid)
            {
                DadoColetaInsumoDTO coletaInsumoDto = Mapper.DynamicMap<DadoColetaInsumoDTO>(coletaInsumoModel);
                coletaInsumoDto.IsMonitorar = true;

                IList<ValorDadoColetaDTO> valoresDto = new List<ValorDadoColetaDTO>();
                foreach (ValorDadoColetaModel dado in dados ?? Enumerable.Empty<ValorDadoColetaModel>())
                {
                    valoresDto.Add(Mapper.Map<ValorDadoColetaDTO>(dado));
                }

                coletaInsumoService.AprovarColetaDadosEstruturados(coletaInsumoDto, valoresDto);
            }

            return AjaxSuccessResult(SGIPMOMessages.MS013, ReturnUrl());
        }

        [HttpPost]
        [Auditoria("Monitorar Coleta", "Aprovar Coleta", typeof(SemanaOperativa))]
        public ActionResult AprovarDadosEstruturadosID(int[] selRowIds, IList<ValorDadoColetaModel> dados, int IDSemanaOperativa)
        {
            IList<ValorDadoColetaDTO> valoresDto = new List<ValorDadoColetaDTO>();
            foreach (ValorDadoColetaModel dado in dados ?? Enumerable.Empty<ValorDadoColetaModel>())
            {
                valoresDto.Add(Mapper.Map<ValorDadoColetaDTO>(dado));
            }

            IList<string> mensagens = new List<string>();
            List<string> mensagensAprovadas = new List<string>();
            int cont = 0;

            for (int i = 0; i < selRowIds.Count(); i++)
            {
                //if (ModelStateHandleValid)
                ColetaInsumo coletaInsumo = coletaInsumoService.ObterColetaInsumoInformarDadosPorChave(selRowIds[i]);
                DadoColetaInsumoDTO coletaInsumoDto = new DadoColetaInsumoDTO();

                coletaInsumoDto.IdColetaInsumo = coletaInsumo.Id;
                coletaInsumoDto.VersaoString = coletaInsumo.VersaoString;
                coletaInsumoDto.Versao = coletaInsumo.Versao;
                coletaInsumoDto.MotivoAlteracaoONS = coletaInsumo.MotivoAlteracaoONS;
                coletaInsumoDto.MotivoRejeicaoONS = coletaInsumo.MotivoRejeicaoONS;
                coletaInsumoDto.IsMonitorar = true;
                //ALTERAÇÃO FEITA PELA RERUM - TFS 5438

                if (coletaInsumo.Situacao.Id != (int)SituacaoColetaInsumoEnum.NaoIniciado && coletaInsumo.Situacao.Id != (int)SituacaoColetaInsumoEnum.PreAprovado)
                {
                    mensagens.Add(string.Format(SGIPMOMessages.MS071, coletaInsumo.Agente.Nome, coletaInsumo.Insumo.Nome));
                }
                else
                {
                    coletaInsumoService.AprovarColetaDadosEstruturadosEmLote(coletaInsumoDto, valoresDto);
                    cont++;
                }
            }

            if (mensagens.Any())
            {
                if (cont > 0)
                {
                    mensagensAprovadas.Add(cont + " " + SGIPMOMessages.MS072);
                }
                else
                {
                    throw new ONSBusinessException(mensagens);
                }
                mensagensAprovadas.AddRange(mensagens);
                return AjaxSuccessResult(mensagensAprovadas, ReturnUrl());
            }
            return AjaxSuccessResult(SGIPMOMessages.MS013, ReturnUrl());
        }

        [HttpPost]
        [Auditoria("Monitorar Coleta", "Rejeitar Coleta", typeof(SemanaOperativa))]
        public ActionResult RejeitarDadosEstruturados(ColetaInsumoModel coletaInsumoModel)
        {
            SetViewError("AnalisarColetaInsumoEstruturado", coletaInsumoModel, CarregarColetaInsumoModel);
            if (ModelStateHandleValid)
            {
                DadoColetaInsumoDTO dto = Mapper.DynamicMap<DadoColetaInsumoDTO>(coletaInsumoModel);
                coletaInsumoService.RejeitarColetaDadosEstruturados(dto);
            }
            return AjaxSuccessResult(SGIPMOMessages.MS013, ReturnUrl());
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
                    var dadoColetaModel = Mapper.DynamicMap<PesquisaDadoColetaManutencaoModel>(dadoColetaManutencao);


                    dadoColetaModel.SituacaoColetaInsumoDescricao = dadoColetaManutencao.SituacaoColetaInsumoDescricao;

                    dadosColetaList.Add(dadoColetaModel);
                }

                resultadoPaginado = new PagedResult<PesquisaDadoColetaManutencaoModel>(dadosColetaList, dadosColeta.TotalCount, dadosColeta.CurrentPage, dadosColeta.PageSize);
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
        [Auditoria("Monitorar Coleta", "Alterar Coleta", typeof(SemanaOperativa))]
        public ActionResult IncluirDadoColetaManutencao(InclusaoDadoColetaManutencaoModel model)
        {
            if (ModelStateHandleValid)
            {
                InclusaoDadoColetaManutencaoDTO dto = Mapper.DynamicMap<InclusaoDadoColetaManutencaoDTO>(model);
                dto.IsMonitorar = true;
                coletaInsumoService.IncluirDadoColetaManutencao(dto);
            }

            return AjaxSuccessResult(SGIPMOMessages.MS013, true);
        }

        public ActionResult CarregarAlteracaoDadoColetaManutencao(int idDadoColeta, string versaoColetaInsumo)
        {
            DadosAlteracaoDadoColetaManutencaoDTO dto =
                coletaInsumoPresentation.ObterDadosAlteracaoDadoColetaManutencao(idDadoColeta);
            AlteracaoDadoColetaManutencaoModel model = Mapper.DynamicMap<AlteracaoDadoColetaManutencaoModel>(dto);
            model.VersaoColetaInsumo = versaoColetaInsumo;
            return View("_AlterarDadoColetaManutencao", model);
        }

        [HttpPost]
        [Auditoria("Monitorar Coleta", "Alterar Coleta", typeof(SemanaOperativa))]
        public ActionResult AlterarDadoColetaManutencao(AlteracaoDadoColetaManutencaoModel model)
        {
            if (ModelStateHandleValid)
            {
                AlteracaoDadoColetaManutencaoDTO dto = Mapper.DynamicMap<AlteracaoDadoColetaManutencaoDTO>(model);
                dto.IsMonitorar = true;
                coletaInsumoService.AlterarDadoColetaManutencao(dto);
            }

            return AjaxSuccessResult(SGIPMOMessages.MS013, true);
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
        [Auditoria("Monitorar Coleta", "Alterar Coleta", typeof(SemanaOperativa))]
        public ActionResult ExcluirDadoColetaManutencao(ExclusaoDadoColetaManutencaoModel model)
        {
            ExclusaoDadoColetaManutencaoDTO dto = Mapper.DynamicMap<ExclusaoDadoColetaManutencaoDTO>(model);
            dto.IsMonitorar = true;
            coletaInsumoService.ExcluirDadoColetaManutencao(dto, -1);
            return AjaxSuccessResult(SGIPMOMessages.MS013, true);
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
        [Auditoria("Monitorar Coleta", "Alterar Coleta", typeof(SemanaOperativa))]
        public ActionResult ImportarManutencao(IList<ImportacaoManutencaoModel> model)
        {
            if (ModelStateHandleValid)
            {
                IList<InclusaoDadoColetaManutencaoDTO> dtos = new List<InclusaoDadoColetaManutencaoDTO>();
                foreach (ImportacaoManutencaoModel importacao in model)
                {
                    InclusaoDadoColetaManutencaoDTO dto = Mapper.DynamicMap<InclusaoDadoColetaManutencaoDTO>(importacao);
                    dto.IsMonitorar = true;
                    dtos.Add(dto);
                }
                coletaInsumoService.IncluirDadoColetaManutencaoImportacao(dtos);
            }

            return AjaxSuccessResult(SGIPMOMessages.MS013, true);
        }

        [HttpPost]
        [Auditoria("Informar Dados Estudo", "Informar", typeof(SemanaOperativa))]
        public ActionResult ImportarManutencoes(ExclusaoDadoColetaManutencaoModel model, IList<ImportacaoManutencaoModel> importarModel)
        {
            if (null == model.ListaIdsDadoColeta)
            {
                model.ListaIdsDadoColeta = string.Empty;
            }
            model.IdColetaInsumo = importarModel[0].IdColetaInsumo;
            model.VersaoColetaInsumo = importarModel[0].VersaoColetaInsumo;

            // Excluir dados de manutenção antigos
            ExclusaoDadoColetaManutencaoDTO dto = Mapper.DynamicMap<ExclusaoDadoColetaManutencaoDTO>(model);
            coletaInsumoService.ExcluirDadoColetaManutencao(dto, model.IdColetaInsumo);

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
                    var tratado = InformarDadosEstudoController.TransformarDadosManutencao(Mapper.DynamicMap<InclusaoDadoColetaManutencaoDTO>(importacao));
                    dtos = dtos.Concat(tratado).ToList();

                }
                coletaInsumoService.IncluirDadoColetaManutencaoImportacao(dtos);
            }

            return AjaxSuccessResult(SGIPMOMessages.MS013, true);
        }


        [HttpPost]
        [ActionSource("SalvarColetaManutencao")]
        [Auditoria("Monitorar Coleta", "Alterar Coleta", typeof(SemanaOperativa))]
        public ActionResult SalvarColetaManutencao(ColetaInsumoModel model)
        {
            if (ModelStateHandleValid)
            {
                ColetaInsumoManutencaoFilter filter = Mapper.DynamicMap<ColetaInsumoManutencaoFilter>(model);
                coletaInsumoService.SalvarColetaDadosManutencao(filter);
            }

            return AjaxSuccessResult(SGIPMOMessages.MS013, true);
        }

        [HttpPost]
        [ActionSource("AprovarColetaManutencao")]
        [Auditoria("Monitorar Coleta", "Aprovar Coleta", typeof(SemanaOperativa))]
        public ActionResult AprovarColetaManutencao(ColetaInsumoModel model)
        {
            return MonitorarColetaManutencao(model, coletaInsumoService.AprovarColetaDadosManutencao);
        }

        [HttpPost]
        [ActionSource("RejeitarColetaManutencao")]
        [Auditoria("Monitorar Coleta", "Rejeitar Coleta", typeof(SemanaOperativa))]
        public ActionResult RejeitarColetaManutencao(ColetaInsumoModel model)
        {
            return MonitorarColetaManutencao(model, coletaInsumoService.RejeitarColetaDadosManutencao);
        }

        private ActionResult MonitorarColetaManutencao(ColetaInsumoModel model,
            Action<ColetaInsumoManutencaoFilter> executarAcao)
        {
            if (ModelStateHandleValid)
            {
                ColetaInsumoManutencaoFilter filter = Mapper.DynamicMap<ColetaInsumoManutencaoFilter>(model);
                executarAcao(filter);
            }

            return AjaxSuccessResult(SGIPMOMessages.MS013, ReturnUrl());
        }

        #endregion

        #region Dado Coleta Não-estruturado

        [HttpPost]
        [Auditoria("Monitorar Coleta", "Alterar Coleta", typeof(SemanaOperativa))]
        public ActionResult SalvarAnaliseDadosNaoEstruturados(ColetaInsumoNaoEstruturadoModel model, List<UploadFileModel> uploadFileModels)
        {
            if (ModelStateHandleValid)
            {
                model.UploadFileModels = uploadFileModels;
                SetViewError("AnalisarColetaInsumoNaoEstruturado", model, CarregarColetaInsumoNaoEstruturadoModel);

                ISet<ArquivoDadoNaoEstruturadoDTO> arquivosEnviados = new HashSet<ArquivoDadoNaoEstruturadoDTO>();

                if (model.UploadFileModels != null)
                {
                    foreach (UploadFileModel fileModel in model.UploadFileModels)
                    {
                        arquivosEnviados.Add(Mapper.DynamicMap<ArquivoDadoNaoEstruturadoDTO>(fileModel));
                    }
                }

                DadosMonitoramentoColetaInsumoDTO dtoColetaInsumo = Mapper.DynamicMap<DadosMonitoramentoColetaInsumoDTO>(model);
                DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dtoDadosColeta = Mapper.DynamicMap<DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO>(model);
                dtoDadosColeta.Arquivos = arquivosEnviados;

                if (model.AprovarDadosAoAnalisar)
                {
                    arquivoService.AprovarColetaDadosNaoEstruturados(dtoColetaInsumo, dtoDadosColeta);
                    arquivoService.LimparArquivosTemporariosUpload(dtoDadosColeta.Arquivos);
                    DisplaySuccessMessage(SGIPMOMessages.MS013);
                    return Redirect(ReturnUrl());
                }

                if (model.RejeitarDadosAoAnalisar)
                {
                    arquivoService.RejeitarColetaDadosNaoEstruturados(dtoColetaInsumo, dtoDadosColeta);
                    arquivoService.LimparArquivosTemporariosUpload(dtoDadosColeta.Arquivos);
                    DisplaySuccessMessage(SGIPMOMessages.MS013);
                    return Redirect(ReturnUrl());
                }

                dtoDadosColeta.IsMonitorar = true;
                arquivoService.SalvarDadoColetaNaoEstruturada(dtoDadosColeta, dtoColetaInsumo);
                arquivoService.LimparArquivosTemporariosUpload(dtoDadosColeta.Arquivos);

                DisplaySuccessMessage(SGIPMOMessages.MS013);
            }

            return View("AnalisarColetaInsumoNaoEstruturado", ObterColetaInsumoNaoEstruturadoModel(model.IdColetaInsumo));
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
                modelNaoEstruturado = Mapper.DynamicMap<ColetaInsumoNaoEstruturadoModel>(dadoColetaNaoEstruturado);
                modelNaoEstruturado.ObservacaoColetaNaoEstruturada = dadoColetaNaoEstruturado.Observacao;
                if (versao != null)
                {
                    modelNaoEstruturado.Versao = versao;
                    modelNaoEstruturado.VersaoString = Convert.ToBase64String(versao);
                }
                foreach (ArquivoDadoNaoEstruturadoDTO arquivo in dadoColetaNaoEstruturado.Arquivos)
                {
                    modelNaoEstruturado.UploadFileModels.Add(Mapper.DynamicMap<UploadFileModel>(arquivo));
                }
                return modelNaoEstruturado;
            }
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

        #region Abrir e Fechar Coleta de Dados

        [ValidateInput(false)]
        [Auditoria("Monitorar Coleta", "Abertura de Coleta", typeof(SemanaOperativa))]
        public ActionResult AbrirColetaPOPUP(PesquisaColetaInsumoModel model)
        {
            return AbrirOuFecharColeta(model, coletaInsumoService.AbrirColeta);
        }

        [ValidateInput(false)]
        [Auditoria("Monitorar Coleta", "Abrir Coleta", typeof(SemanaOperativa))]
        public ActionResult AbrirColetaPOP(PesquisaColetaInsumoModel model)
        {
            SemanaOperativa semanaOperativa = semanaOperativaService.ObterSemanaOperativaPorChave(model.IdSemanaOperativa.Value);
            model.NomeSemanaOperativa = "[ONS-WEBPMO] Abertura da coleta de dados do " + semanaOperativa.Nome;

            DadosSemanaOperativaDTO dadosSemanaOperativaDTO = new DadosSemanaOperativaDTO();
            dadosSemanaOperativaDTO.IdSemanaOperativa = semanaOperativa.Id;
            dadosSemanaOperativaDTO.VersaoSemanaOperativa = semanaOperativa.Versao;

            Parametro pa = coletaInsumoService.MensagemAberturaColetaEditavel(dadosSemanaOperativaDTO);
            model.MensagemDaAberturaColetaEditavel = pa.Valor.Replace(@"\r\n?|\n", "<br />").Replace("\r\n", "<br />");

            model.ReenvioDeNotificacao = Request.QueryString["Reenviar"];

            return PartialView("_AbrirColeta", model);
        }

        [Auditoria("Monitorar Coleta", "Abrir Coleta", typeof(SemanaOperativa))]
        public ActionResult AbrirColeta(PesquisaColetaInsumoModel model)
        {
            return AbrirOuFecharColeta(model, coletaInsumoService.AbrirColeta);
        }

        [Auditoria("Monitorar Coleta", "Abrir Coleta", typeof(SemanaOperativa))]
        public ActionResult VerificaAbrirColeta(PesquisaColetaInsumoModel model)
        {
            SemanaOperativa semanaOperativa = semanaOperativaService.ObterSemanaOperativaPorChave(model.IdSemanaOperativa.Value);

            DadosSemanaOperativaDTO dadosSemanaOperativaDTO = new DadosSemanaOperativaDTO();
            dadosSemanaOperativaDTO.IdSemanaOperativa = semanaOperativa.Id;
            dadosSemanaOperativaDTO.VersaoSemanaOperativa = semanaOperativa.Versao;

            bool situacaoBoolSemanaOperativa = coletaInsumoService.situacaoBoolSemanaOperativa(dadosSemanaOperativaDTO);

            return Json(situacaoBoolSemanaOperativa);
        }

        [Auditoria("Monitorar Coleta", "Reenviar Notificacao de Abertura de Coleta", typeof(SemanaOperativa))]
        public ActionResult VerificaSemanaOperativa(PesquisaColetaInsumoModel model)
        {
            SemanaOperativa semanaOperativa = semanaOperativaService.ObterSemanaOperativaPorChave(model.IdSemanaOperativa.Value);

            bool BoolSituacaoSemanaOperativaColetaDados = false;
            if (semanaOperativa.Situacao.Id == (int)SituacaoSemanaOperativaEnum.ColetaDados)
            {
                BoolSituacaoSemanaOperativaColetaDados = true;
            }

            return Json(BoolSituacaoSemanaOperativaColetaDados);
        }

        [Auditoria("Monitorar Coleta", "Fechar Coleta", typeof(SemanaOperativa))]
        public ActionResult FecharColeta(PesquisaColetaInsumoModel model)
        {
            return AbrirOuFecharColeta(model, coletaInsumoService.FecharColeta);
        }

        private ActionResult AbrirOuFecharColeta(PesquisaColetaInsumoModel model, Action<DadosSemanaOperativaDTO> abrirOuFecharColeta)
        {
            SetViewError("Index", model, CarregarDadosPesquisa);

            //var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                DadosSemanaOperativaDTO dtoAbrirOuFecharColeta = new DadosSemanaOperativaDTO()
                {
                    IdSemanaOperativa = model.IdSemanaOperativa.Value,
                    VersaoSemanaOperativa = Convert.FromBase64String(model.VersaoStringSemanaOperativa),
                    Assunto = model.NomeSemanaOperativa,
                    Mensagem = model.MensagemDaAberturaColetaEditavel,
                    EnviarTodos = model.EnviarTodos,
                    ReenvioDeNotificacao = Convert.ToBoolean(model.ReenvioDeNotificacao)
                };

                abrirOuFecharColeta(dtoAbrirOuFecharColeta);
                DisplaySuccessMessage(SGIPMOMessages.MS013);
            }

            model.MensagemDaAberturaColetaEditavel = null;
            model.NomeSemanaOperativa = null;

            CarregarDadosPesquisa(model);
            return Redirect(ReturnUrl(model));
        }


        #endregion

        #region Convergência CCEE

        public ActionResult PesquisaConvergenciaCCEE(PesquisaColetaInsumoModel model)
        {
            ArquivosSemanaOperativaFilter filter = Mapper.DynamicMap<ArquivosSemanaOperativaFilter>(model);
            filter.IsConsiderarInsumosConvergenciaCCEE = true;

            ArquivosSemanaOperativaDTO arquivosSemanaOperativa =
                semanaOperativaService.ConsultarArquivosSemanaOperativaConvergenciaCcee(filter);

            ConvergenciaCCEEModel modelCCEE = Mapper.DynamicMap<ConvergenciaCCEEModel>(model);
            modelCCEE.ArquivosInsumosProcessamento = arquivosSemanaOperativa.ArquivosInsumos;
            modelCCEE.SituacaoSemanaOperativa = arquivosSemanaOperativa.SituacaoSemanaOperativa;

            if (arquivosSemanaOperativa.ArquivosEnviados != null)
            {
                foreach (var arquivoDadoNaoEstruturadoDto in arquivosSemanaOperativa.ArquivosEnviados)
                {
                    modelCCEE.ArquivosEnviadosConvergenciaCcee.Add(Mapper.DynamicMap<UploadFileModel>(arquivoDadoNaoEstruturadoDto));
                }
            }

            return PartialView("_PesquisaConvergenciaCCEE", modelCCEE);
        }

        [HttpPost]
        [Auditoria("Convergência com CCEE", "Iniciar Convergência", typeof(SemanaOperativa))]
        public ActionResult IniciarConvergenciaCCEE(ConvergenciaCCEEModel model, List<UploadFileModel> uploadFileModels)
        {
            if (ModelStateHandleValid)
            {
                model.ArquivosEnviadosConvergenciaCcee = uploadFileModels;

                InicializacaoConvergenciaCceeDTO dto = Mapper.DynamicMap<InicializacaoConvergenciaCceeDTO>(model);
                dto.VersaoSemanaOperativa = Convert.FromBase64String(model.VersaoStringSemanaOperativa);

                dto.Arquivos = new HashSet<ArquivoDadoNaoEstruturadoDTO>();
                if (model.ArquivosEnviadosConvergenciaCcee != null)
                {
                    foreach (UploadFileModel fileModel in model.ArquivosEnviadosConvergenciaCcee)
                    {
                        dto.Arquivos.Add(Mapper.DynamicMap<ArquivoDadoNaoEstruturadoDTO>(fileModel));
                    }
                }
                arquivoService.IniciarConvergenciaCCEE(dto);
                arquivoService.LimparArquivosTemporariosUpload(dto.Arquivos);
            }
            return AjaxSuccessResult(SGIPMOMessages.MS013, GetReturnUrlDecodedPesquisa(model.IdSemanaOperativa.Value, EtapaMonitoramento.ConvergenciaCCEE));
        }

        #endregion

        #region Publicação de Resultados

        /// <summary>
        /// Action utilizada para carregar a aba de Publicação de Resultados.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult PesquisaPublicacaoResultados(PesquisaColetaInsumoModel model)
        {
            ArquivosSemanaOperativaFilter filter = Mapper.DynamicMap<ArquivosSemanaOperativaFilter>(model);
            filter.IsConsiderarInsumosPublicacao = true;

            ArquivosSemanaOperativaDTO arquivosSemanaOperativa =
                semanaOperativaService.ConsultarArquivosSemanaOperativaPublicacaoResultados(filter);

            PublicaoResultadosModel modelPublicacao = Mapper.DynamicMap<PublicaoResultadosModel>(model);
            modelPublicacao.ArquivosInsumos = arquivosSemanaOperativa.ArquivosInsumos;
            modelPublicacao.SituacaoSemanaOperativa = arquivosSemanaOperativa.SituacaoSemanaOperativa;

            if (arquivosSemanaOperativa.ArquivosEnviados != null)
            {
                foreach (var arquivoDadoNaoEstruturadoDto in arquivosSemanaOperativa.ArquivosEnviados)
                {
                    UploadFileModel fileModel = Mapper.DynamicMap<UploadFileModel>(arquivoDadoNaoEstruturadoDto);
                    if (arquivoDadoNaoEstruturadoDto.IsPublicado.HasValue) fileModel.AddOrSetExtendedProperty("IsPublicado", (arquivoDadoNaoEstruturadoDto.IsPublicado.Value ? "Sim" : "Não"));
                    modelPublicacao.ArquivosEnviadosPublicacao.Add(fileModel);
                }
            }
            return PartialView("_PesquisaPublicacaoResultados", modelPublicacao);
        }

        [HttpPost]
        [Auditoria("Publicação de Resultados", "Publicar Resultado", typeof(SemanaOperativa))]
        public ActionResult PublicarResultados(PublicaoResultadosModel model, List<UploadFileModel> uploadFileModels)
        {
            if (ModelStateHandleValid)
            {
                model.ArquivosEnviadosPublicacao = uploadFileModels;

                PublicacaoResultadosDTO dto = Mapper.DynamicMap<PublicacaoResultadosDTO>(model);
                dto.VersaoSemanaOperativa = Convert.FromBase64String(model.VersaoStringSemanaOperativa);

                dto.Arquivos = new HashSet<ArquivoDadoNaoEstruturadoDTO>();
                if (model.ArquivosEnviadosPublicacao != null)
                {
                    foreach (UploadFileModel fileModel in model.ArquivosEnviadosPublicacao)
                    {
                        dto.Arquivos.Add(Mapper.DynamicMap<ArquivoDadoNaoEstruturadoDTO>(fileModel));
                    }
                }
                arquivoService.PublicarResultados(dto);
                arquivoService.LimparArquivosTemporariosUpload(dto.Arquivos);
            }
            return AjaxSuccessResult(SGIPMOMessages.MS013, GetReturnUrlDecodedPesquisa(model.IdSemanaOperativa.Value, EtapaMonitoramento.PublicacaoResultados));
        }

        [HttpPost]
        [Auditoria("Publicação de Resultados", "Reprocessar PMO", typeof(SemanaOperativa))]
        public ActionResult ReprocessarPMO(PublicaoResultadosModel model)
        {
            if (ModelStateHandleValid)
            {
                ReprocessamentoPMODTO dto = Mapper.DynamicMap<ReprocessamentoPMODTO>(model);
                dto.VersaoSemanaOperativa = Convert.FromBase64String(model.VersaoStringSemanaOperativa);

                semanaOperativaService.ReprocessarPMO(dto);
            }
            return AjaxSuccessResult(SGIPMOMessages.MS013, GetReturnUrlDecodedPesquisa(model.IdSemanaOperativa.Value, EtapaMonitoramento.PublicacaoResultados));
        }

        #endregion

        #region Geração de Blocos
        public ActionResult PesquisaGeracaoBlocos(PesquisaColetaInsumoModel model)
        {
            PesquisaGeracaoBlocosModel modelGeracao = null;

            if (ModelStateHandleValid)
            {
                DadosPesquisaGeracaoBlocosDTO dto =
                    coletaInsumoPresentation.ObterDadosPesquisaGeracaoBloco(model.IdSemanaOperativa.Value);
                modelGeracao = Mapper.DynamicMap<PesquisaGeracaoBlocosModel>(dto);
                Mapper.Map(model, modelGeracao);
            }

            return PartialView("_PesquisaGeracaoBlocos", modelGeracao);
        }

        [HttpPost]
        [Auditoria("Geração de Blocos", "Gerar Blocos", typeof(SemanaOperativa))]
        public ActionResult GerarBlocos(int idSemanaOperativa, string versaoStringSemanaOperativa, bool isGerarBlocoParaEncerrar = false)
        {
            geracaoBlocosService.GerarBlocos(idSemanaOperativa, Convert.FromBase64String(versaoStringSemanaOperativa), false);

            return AjaxSuccessResult(SGIPMOMessages.MS013, GetReturnUrlDecodedPesquisa(idSemanaOperativa, EtapaMonitoramento.GeracaoBlocos, isGerarBlocoParaEncerrar));
        }

        [HttpPost]
        [Auditoria("Geração de Blocos Insumos Aprovados", "Gerar Blocos", typeof(SemanaOperativa))]
        public ActionResult GerarBlocoInsumosAprovados(int idSemanaOperativa, string versaoStringSemanaOperativa)
        {
            geracaoBlocosService.GerarBlocos(idSemanaOperativa, Convert.FromBase64String(versaoStringSemanaOperativa), true);
            return AjaxSuccessResult(SGIPMOMessages.MS013, GetReturnUrlDecodedPesquisa(idSemanaOperativa, EtapaMonitoramento.GeracaoBlocos));
        }

        [HttpPost]
        [Auditoria("Geração de Blocos", "Encerrar Estudo", typeof(SemanaOperativa))]
        public ActionResult EncerrarEstudo(PublicaoResultadosModel model)
        {
            if (ModelStateHandleValid)
            {
                PublicacaoResultadosDTO dto = Mapper.DynamicMap<PublicacaoResultadosDTO>(model);
                dto.VersaoSemanaOperativa = Convert.FromBase64String(model.VersaoStringSemanaOperativa);
                dto.IsEncerradoDiretamente = true;

                arquivoService.PublicarResultados(dto);
            }
            return AjaxSuccessResult(SGIPMOMessages.MS013, GetReturnUrlDecodedPesquisa(model.IdSemanaOperativa.Value, EtapaMonitoramento.PublicacaoResultados));
        }

        #endregion

        #region Importar Cronograma Manutenção Hidráulica e Térmica
        public ActionResult ImportarCronogramaManutencaoHidraulicaTermica(InformaDadosColetaInsumoModel analisarModel)
        {
            IList<int> idsInsumos = ConfigurationManager.AppSettings["InsumosManutencaoHidraulicaTermica"].Split(',').Select(id => int.Parse(id)).ToList();

            coletaInsumoPresentation.ImportarCronogramaManutencaoHidraulicaTermica(analisarModel.IdSemanaOperativa.Value, idsInsumos);

            return AjaxSuccessResult(SGIPMOMessages.MS079, GetReturnUrlDecodedPesquisa(analisarModel.IdSemanaOperativa.Value, EtapaMonitoramento.ColetaDados));
        }
        #endregion
    }

}
