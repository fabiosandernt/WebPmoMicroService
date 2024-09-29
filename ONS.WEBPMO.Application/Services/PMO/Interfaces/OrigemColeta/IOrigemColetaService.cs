using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Domain.Entities.Base;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;
using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta
{
    //[ServiceContract]
    public interface IOrigemColetaService 
    {
        
        
        PagedResult<ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO.OrigemColeta> ConsultarOrigemColetasGabaritoPaginado(GabaritoParticipantesFilter filter);

        T ObterOrigemColetaPorChaveOnline<T>(string id) where T : ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO.OrigemColeta;

        T ObterOrigemColetaPorChave<T>(string id) where T : ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO.OrigemColeta;



        ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO.OrigemColeta ObterOuCriarOrigemColetaPorId(string idOrigemColeta, TipoOrigemColetaEnum tipoOrigemColeta);

        
        
        IList<ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO.OrigemColeta> ConsultarOuCriarOrigemColetaPorIds(IList<string> idsOrigemColeta, TipoOrigemColetaEnum tipoOrigemColeta);

        
        
        IList<ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO.OrigemColeta> ConsultarOrigemColetaPorTipoNomeOnline(TipoOrigemColetaEnum tipoOrigemColeta, string nome);

        
        
        IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorUsinaOnline(string idOrigemColeta);

        
        
        IList<Usina> ConsultarUsinaParticipanteGabaritoPorColetaInsumo(int idColetaInsumo);

        
        
        IList<UnidadeGeradora> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumoUsina(
            int idColetaInsumo, string idUsina);

        
        
        IList<UnidadeGeradora> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumo(int idColetaInsumo);

        
        
        IList<UnidadeGeradoraManutencaoSGIDTO> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumos(List<int> idColetaInsumos);

        
        void SincronizarOrigensColetaComBDT();
    }
}
