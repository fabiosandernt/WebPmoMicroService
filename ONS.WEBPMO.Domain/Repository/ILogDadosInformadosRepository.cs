
using ONS.WEBPMO.Domain.Entities.Base;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository
{
    public interface ILogDadosInformadosRepository : IRepository<LogDadosInformados>
    {
        PagedResult<LogDadosInformados> ConsultarPorFiltro(LogDadosInformadosFilter filter);
    }
}
