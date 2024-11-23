using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository.PMO
{
    public interface ILogDadosInformadosRepository : IRepository<LogDadosInformados>
    {
        ICollection<LogDadosInformados> ConsultarPorFiltro(LogDadosInformadosFilter filter);
    }
}
