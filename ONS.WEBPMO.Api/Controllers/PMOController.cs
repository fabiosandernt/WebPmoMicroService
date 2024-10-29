using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Models.PMO;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.Resources;
using ONS.Infra.Core.Exceptions;

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

        [HttpPost("ManterPMO/{id}")]
        public async Task<IActionResult> ManterPMOAsync(int id)
        {
            var pmo = await pmoService.ObterPMOPorChaveAsync(id);
            if (pmo == null)
            {
                return NotFound();
            }
            var model = RecarregarPMOModel(pmo);
            return Ok(model);
        }

        [HttpPost("ManterPMO")]
        public async Task<IActionResult> ManterPMOAsync([FromBody] PMOConsultaModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pmo = await pmoService.GerarPMOAsync(model.Ano.Value, model.Mes.Value);
            return Ok(new { message = SGIPMOMessages.MS013, pmoId = pmo.Id });
        }

        [HttpPost("SalvarPMO")]
        public async Task<IActionResult> SalvarPMOAsync([FromBody] PMOManterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await pmoService.AtualizarMesesAdiantePMOAsync(model.Id, model.QuantidadeMesesAdiante, model.Versao);
            return Ok(new { message = SGIPMOMessages.MS013 });
        }

        [HttpPost("ExcluirPMO")]
        public async Task<IActionResult> ExcluirPMOAsync([FromBody] PMOManterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dto = new DadosPMODTO
            {
                IdPMO = model.Id,
                VersaoPMO = Convert.FromBase64String(model.VersaoPmoString)
            };

            await pmoService.ExcluirPMOAsync(dto);
            return Ok(new { message = SGIPMOMessages.MS013 });
        }

        private PMOManterModel RecarregarPMOModel(Domain.Entities.PMO.PMO pmo)
        {
            PMOManterModel manterModel = new PMOManterModel();
            if (pmo != null)
            {
                manterModel = _mapper.Map<PMOManterModel>(pmo);
                manterModel.VersaoPmoString = Convert.ToBase64String(pmo.Versao);
            }
            return manterModel;
        }
      
        [HttpGet("PesquisaPMO")]
        public async Task<IActionResult> PesquisaPMOAsync([FromQuery] int? ano, [FromQuery] int? mes)
        {
            if (!ano.HasValue || !mes.HasValue)
            {
                return BadRequest("Ano e Mês são obrigatórios.");
            }

            var filtro = new PMOFilter { Ano = ano.Value, Mes = mes.Value };
            var pmo = await pmoService.ObterPMOPorFiltroAsync(filtro);
            if (pmo != null)
            {
                return Ok(new { pmo });
            }
            return NotFound(SGIPMOMessages.MS004);
        }

        [HttpPost("CarregarAbrirEstudo")]
        public async Task<IActionResult> CarregarAbrirEstudoAsync([FromBody] AberturaEstudoModel model)
        {
            ValidarSelecaoSemanaOperativa(model.IdSemanaOperativa);

            var dto = new DadosSemanaOperativaDTO
            {
                IdSemanaOperativa = model.IdSemanaOperativa.Value,
                VersaoPMO = Convert.FromBase64String(model.VersaoPmoString)
            };

            var semanaOperativa = await semanaOperativaService.ObterSemanaOperativaValidaParaAbrirEstudoAsync(dto);
            var modelModalAberturaEstudo = new EscolhaGabaritoModel
            {
                IdSemanaOperativa = semanaOperativa.Id,
                VersaoSemanaOperativa = semanaOperativa.Versao,
                VersaoPMO = semanaOperativa.PMO.Versao
            };

            return Ok(modelModalAberturaEstudo);
        }

        private void ValidarSelecaoSemanaOperativa(int? idSemanaOperativa)
        {
            if (!idSemanaOperativa.HasValue)
            {
                throw new ONSBusinessException(SGIPMOMessages.MS025);
            }
        }

        [HttpPost("AbrirEstudo")]
        public async Task<IActionResult> AbrirEstudoAsync([FromBody] EscolhaGabaritoModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dto = new AberturaEstudoDTO
            {
                IdSemanaOperativa = model.IdSemanaOperativa,
                IdEstudo = model.IdEstudo,
                IsPadrao = model.IsPadrao,
                VersaoPMO = model.VersaoPMO,
                VersaoSemanaOperativa = model.VersaoSemanaOperativa
            };

            await semanaOperativaService.AbrirEstudoAsync(dto);
            return Ok(new { message = SGIPMOMessages.MS013 });
        }

        [HttpPost("IncluirSemanaOperativa")]
        public async Task<IActionResult> IncluirSemanaOperativaAsync([FromBody] InclusaoSemanaOperativaModel model)
        {
            throw new NotImplementedException();
        }

        [HttpPost("ExcluirUltimaSemanaOperativaPMO")]
        public async Task<IActionResult> ExcluirUltimaSemanaOperativaPMOAsync([FromBody] ManutencaoSemanaOperativaModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await pmoService.ExcluirUltimaSemanaOperativaAsync(model.IdPMO, Convert.FromBase64String(model.VersaoPmoString));
            return Ok(new { message = SGIPMOMessages.MS013 });
        }

        [HttpPost("CarregarAlterarSemanaOperativa")]
        public async Task<IActionResult> CarregarAlterarSemanaOperativaAsync([FromQuery] int idSemanaOperativaSelecionada)
        {
            throw new NotImplementedException();
        }

        [HttpPost("AlterarSemanaOperativa")]
        public async Task<IActionResult> AlterarSemanaOperativaAsync([FromBody] AlteracaoSemanaOperativaModel model)
        {
            throw new NotImplementedException();
        }

        [HttpGet("CarregarResetarGabarito")]
        public async Task<IActionResult> CarregarResetarGabaritoAsync([FromQuery] int idSemanaOperativa)
        {
            ValidarSelecaoSemanaOperativa(idSemanaOperativa);

            var semanaOperativa = await semanaOperativaService.ObterSemanaOperativaValidaParaResetarGabaritoAsync(idSemanaOperativa);

            var estudosDesconsiderados = new List<object>
            {
                new { Id = semanaOperativa.Id.ToString(), Nome = "Estudo " + semanaOperativa.Id }
            };

            var model = new EscolhaGabaritoModel
            {
                IdSemanaOperativa = semanaOperativa.Id,
                VersaoPMO = semanaOperativa.PMO.Versao,
                VersaoSemanaOperativa = semanaOperativa.Versao,
                EstudosParaDesconsiderar = (IList<System.Web.Mvc.SelectListItem>)estudosDesconsiderados
            };

            return Ok(model);
        }

        [HttpPost("ResetarGabarito")]
        public async Task<IActionResult> ResetarGabaritoAsync([FromBody] EscolhaGabaritoModel model)
        {
            throw new NotImplementedException();
        }
    }
}
