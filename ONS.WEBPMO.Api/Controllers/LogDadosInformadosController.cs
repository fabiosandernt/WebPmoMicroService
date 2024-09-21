using AutoMapper;
using ONS.Common.Seguranca;
using ONS.Common.Util.Pagination;

using ONS.SGIPMO.Domain.Entities;
using ONS.SGIPMO.Domain.Entities.DTO;
using ONS.SGIPMO.Domain.Presentations;
using ONS.SGIPMO.Domain.Services;
using ONS.SGIPMO.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ONS.WEBPMO.Api.Controllers
{
    // Criar permissão para LogDadosInformados
    [WebPermission("LogNotificacao")]
    public class LogDadosInformadosController : ControllerBase
    {

        private readonly ILogDadosInformadosService logDadosInformadosService;

        public LogDadosInformadosController(ILogDadosInformadosService logDadosInformadosService)
        {
            this.logDadosInformadosService = logDadosInformadosService;
        }

        public ActionResult Index()
        {
            PesquisaLogInformarDadosModel model = new PesquisaLogInformarDadosModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Pesquisar(PesquisaLogInformarDadosModel model)
        {   

            return PartialView("_PesquisaLogDadosInformados", model);
        }

        public ActionResult CarregarGridLogInformarDados(GridSettings gridSettings, PesquisaLogInformarDadosModel model)
        {
            PagedResult<LogDadosInformados> resultadoPaginado = null;

            if (ModelStateHandleValid)
            {
                LogDadosInformadosFilter filter = Mapper.DynamicMap<LogDadosInformadosFilter>(model);

                Mapper.DynamicMap(gridSettings, filter);

                resultadoPaginado = logDadosInformadosService.obterLogDadosInformados(filter);
            }

            return JsonToPagedGrid(resultadoPaginado, gridSettings.PageIndex);
        }

        public ActionResult CarregarCsvDatalogInformarDados(PesquisaLogInformarDadosModel model)
        {
            PagedResult<LogDadosInformados> resultadoPaginado = null;

            if (ModelStateHandleValid)
            {
                LogDadosInformadosFilter filter = Mapper.DynamicMap<LogDadosInformadosFilter>(model);
                filter.PageSize = 10000 * 10000;
                filter.PageIndex = 1;

                resultadoPaginado = logDadosInformadosService.obterLogDadosInformados(filter);
            }

            return Json(resultadoPaginado);
        }

    }
}
