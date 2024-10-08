using ONS.Common.Util.Pagination;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    //[ServiceContract]
    public interface ILogDadosInformadosService 
    {
        
        //[UseNetDataContractSerializer("Agente", "SemanaOperativa")]
        PagedResult<LogDadosInformados> obterLogDadosInformados(LogDadosInformadosFilter filter);

    }
}
