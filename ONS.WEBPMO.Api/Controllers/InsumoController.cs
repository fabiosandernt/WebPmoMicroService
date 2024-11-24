using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.Filters;

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

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            try
            {
                var insumos = await _insumoService.ConsultarTodosInsumosAsync();
                return Ok(insumos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("filter")]
        public IActionResult GetByFilter([FromQuery] InsumoFiltro filter)
        {
            try
            {
                var result = _insumoService.GetByQueryableAsync(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var insumoDto = await _insumoService.ConsultarInsumoAsync(id);

            return Ok(insumoDto);
        }

    }
}
