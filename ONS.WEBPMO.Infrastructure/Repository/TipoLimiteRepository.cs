using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.PMO;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories
{
    public class TipoLimiteRepository : Repository<TipoLimite>, ITipoLimiteRepository
    {
        public TipoLimiteRepository(WEBPMODbContext context) : base(context)
        {
        }
    }
}
