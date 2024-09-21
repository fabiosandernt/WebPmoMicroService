using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ONS.WEBPMO.Api.Controllers
{
    using ONS.Common.Seguranca;
    using ONS.Common.Util.Pagination;
    
    using ONS.SGIPMO.Domain.Entities;
    using ONS.SGIPMO.Domain.Entities.Filters;
    using ONS.SGIPMO.Domain.Services;
    using ONS.SGIPMO.WebSite.Models;

    using global::AutoMapper;
    [WebPermission("ConfigurarGabarito")]
    public class ParticipantesGabaritoController : ControllerBase
    {
        private readonly IAgenteService agenteService;
        private readonly IOrigemColetaService origemColetaService;

        public ParticipantesGabaritoController(IAgenteService agenteService, IOrigemColetaService origemColetaService)
        {
            this.agenteService = agenteService;
            this.origemColetaService = origemColetaService;
        }

        #region Pesquisa Participante
        public ActionResult PesquisaGabaritoParticipante(GabaritoConsultaModel model)
        {
            return PartialView("_PesquisaGabaritoParticipante", model);
        }

        public ActionResult PesquisaParticipanteAgente(GabaritoConsultaModel model)
        {
            return PartialView("_PesquisaParticipanteAgente", model);
        }

        public ActionResult PesquisaParticipanteOrigemColeta(GabaritoConsultaModel model)
        {
            if (model.TipoOrigemColeta == TipoOrigemColetaEnum.Reservatorio)
            {
                return this.PartialView("_PesquisaParticipanteReservatorio", model);
            }
            return PartialView("_PesquisaParticipanteUsina", model);
        }

        #endregion

        public ActionResult CarregarGridAgentesParticipantesGabarito(GridSettings gridSettings, GabaritoConsultaModel model)
        {
            GabaritoParticipantesFilter filter = Mapper.DynamicMap<GabaritoParticipantesFilter>(model);
            
            Mapper.DynamicMap(gridSettings, filter);

            PagedResult<Agente> result = agenteService.ConsultarAgentesParticipamGabaritoPaginado(filter);

            return JsonToPagedGrid(result, gridSettings.PageIndex);
        }

        public ActionResult CarregarGridOrigensColetasParticipanteGabarito(GridSettings gridSettings, GabaritoConsultaModel model)
        {
            GabaritoParticipantesFilter filter = Mapper.DynamicMap<GabaritoParticipantesFilter>(model);
            
            Mapper.DynamicMap(gridSettings, filter);

            PagedResult<OrigemColeta> result = this.origemColetaService.ConsultarOrigemColetasGabaritoPaginado(filter);

            return JsonToPagedGrid(result, gridSettings.PageIndex);
        }
        public ActionResult CarregarGridUnidadesGeradorasParticipanteGabarito(GridSettings gridSettings, GabaritoConsultaModel model, string idUsinaPai)
        {
            GabaritoParticipantesFilter filter = Mapper.DynamicMap<GabaritoParticipantesFilter>(model);
            
            Mapper.DynamicMap(gridSettings, filter);

            filter.IdUsinaPai = idUsinaPai;

            PagedResult<OrigemColeta> result = this.origemColetaService.ConsultarOrigemColetasGabaritoPaginado(filter);

            return JsonToPagedGrid(result, gridSettings.PageIndex);
        }
        

    }
}
