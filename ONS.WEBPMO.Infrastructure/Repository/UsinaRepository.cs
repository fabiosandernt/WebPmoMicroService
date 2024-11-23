using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;
using ONS.WEBPMO.Domain.Repository.PMO;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class UsinaRepository : Repository<Usina>, IUsinaRepository
    {
        public UsinaRepository(WEBPMODbContext context) : base(context)
        {
        }
    }
}
