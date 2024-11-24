using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Models.PMO;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.Resources;
using ONS.Infra.Core.Exceptions;
using ONS.WEBPMO.Application.Services.PMO.Implementation;

namespace ONS.WEBPMO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PMOController : ControllerBase
    {
        private readonly IPMOService pmoService;
        private readonly ISemanaOperativaService semanaOperativaService;
        private readonly IMapper _mapper;
        
        public PMOController(IPMOService pmoService,
                             ISemanaOperativaService semanaOperativaService,
                             IMapper mapper)
        {
            this.pmoService = pmoService;
            this.semanaOperativaService = semanaOperativaService;
            _mapper = mapper;
            
        }
        [HttpGet("getPmoByFilter")]
        public async Task<IActionResult> GetPmoByFilter([FromQuery] PMOFilter filter)
        {
            try
            {
                var result = await pmoService.ObterPMOPorFiltroAsync(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await pmoService.GetByIdAsync(id);
            return Ok(result);
        }
       

        [HttpPost("gerar")]
        public async Task<IActionResult> GerarPMO([FromBody] PMOConsultaModel model)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPMO(int id, [FromBody] PMOManterModel model)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirPMO(int id, [FromBody] PMOManterModel model)
        {
            throw new NotImplementedException();
        }

        [HttpPost("pesquisa")]
        public async Task<IActionResult> PesquisaPMO([FromBody] PMOConsultaModel model)
        {
            throw new NotImplementedException();
        }

        [HttpPost("semana-operativa/incluir")]
        public async Task<IActionResult> IncluirSemanaOperativa([FromBody] InclusaoSemanaOperativaModel model)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("semana-operativa/{id}")]
        public async Task<IActionResult> ExcluirUltimaSemanaOperativa(int id, [FromBody] ManutencaoSemanaOperativaModel model)
        {
            throw new NotImplementedException();
        }

        [HttpPost("semana-operativa/alterar")]
        public async Task<IActionResult> AlterarSemanaOperativa([FromBody] AlteracaoSemanaOperativaModel model)
        {
            throw new NotImplementedException();
        }

        [HttpPost("semana-operativa/resetar-gabarito")]
        public async Task<IActionResult> ResetarGabarito([FromBody] EscolhaGabaritoModel model)
        {
            throw new NotImplementedException();
        }

    }
}
