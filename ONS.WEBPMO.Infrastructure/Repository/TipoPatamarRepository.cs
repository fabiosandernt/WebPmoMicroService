using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories
{
    public class TipoPatamarRepository : Repository<TipoPatamar>, ITipoPatamarRepository
    {
        public TipoPatamarRepository(WEBPMODbContext context) : base(context)
        {
        }
    }
}
