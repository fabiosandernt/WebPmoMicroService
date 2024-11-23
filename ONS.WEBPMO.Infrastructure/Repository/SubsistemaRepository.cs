using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;
using ONS.WEBPMO.Domain.Repository.PMO;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class SubsistemaRepository : Repository<Subsistema>, ISubsistemaRepository
    {
        public SubsistemaRepository(WEBPMODbContext context) : base(context)
        {
        }
    }
}
