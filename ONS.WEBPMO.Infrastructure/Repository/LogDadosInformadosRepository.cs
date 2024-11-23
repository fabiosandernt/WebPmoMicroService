using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.PMO;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class LogDadosInformadosRepository : Repository<LogDadosInformados>, ILogDadosInformadosRepository
    {
        public LogDadosInformadosRepository(WEBPMODbContext context) : base(context)
        {
        }

        public ICollection<LogDadosInformados> ConsultarPorFiltro(LogDadosInformadosFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
