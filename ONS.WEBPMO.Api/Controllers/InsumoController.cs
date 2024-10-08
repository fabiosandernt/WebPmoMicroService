using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Models.Insumo;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Presentations;

namespace ONS.WEBPMO.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[WebPermission("ConfigurarInsumo")]
    public class InsumoController : ControllerBase
    {
        private readonly IInsumoService _insumoService;       

        public InsumoController(
            IInsumoService insumoService,            
            IMapper mapper)
        {
            _insumoService = insumoService;         
            
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetByFilter([FromQuery] InsumoFiltro filter)
        {
            try
            {
                var result = _insumoService.GetByQueryable(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
