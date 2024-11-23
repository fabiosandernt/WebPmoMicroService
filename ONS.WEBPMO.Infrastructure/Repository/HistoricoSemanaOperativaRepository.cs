using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.PMO;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class HistoricoSemanaOperativaRepository : Repository<HistoricoSemanaOperativa>, IHistoricoSemanaOperativaRepository
    {
        public HistoricoSemanaOperativaRepository(WEBPMODbContext context) : base(context)
        {
        }
    }
}
