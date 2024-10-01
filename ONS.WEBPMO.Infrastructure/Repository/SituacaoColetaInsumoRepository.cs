using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories
{
    public class SituacaoColetaInsumoRepository : Repository<SituacaoColetaInsumo>, ISituacaoColetaInsumoRepository
    {
        public SituacaoColetaInsumoRepository(WEBPMODbContext context) : base(context)
        {
        }
    }
}
