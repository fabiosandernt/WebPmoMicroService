using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class HistoricoColetaInsumoRepository : Repository<HistoricoColetaInsumo>, IHistoricoColetaInsumoRepository
    {
        public HistoricoColetaInsumoRepository(WEBPMODbContext context) : base(context)
        {
        }
    }
}
