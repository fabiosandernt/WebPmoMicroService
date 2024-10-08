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

    }
}
