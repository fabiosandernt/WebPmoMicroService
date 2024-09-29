using ONS.WEBPMO.Domain.Entities.Base;
using ONS.WEBPMO.Domain.Entities.Filters;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    //[ServiceContract]
    public interface ILogDadosInformadosService 
    {
        
        //[UseNetDataContractSerializer("Agente", "SemanaOperativa")]
        PagedResult<LogDadosInformados> obterLogDadosInformados(LogDadosInformadosFilter filter);

    }
}
