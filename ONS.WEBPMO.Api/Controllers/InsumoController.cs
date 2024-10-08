using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Models.Insumo;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Presentations;

namespace ONS.WEBPMO.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[WebPermission("ConfigurarInsumo")]
    public class InsumoController : ControllerBase
    {
        private readonly IInsumoService insumoService;
        private readonly IInsumoPresentation insumoPresentation;
        private readonly IMapper mapper;

        public InsumoController(
            IInsumoService insumoService,
            IInsumoPresentation insumoPresentation,
            IMapper mapper)
        {
            this.insumoService = insumoService;
            this.insumoPresentation = insumoPresentation;
            this.mapper = mapper;
        }

        
    }
}
