using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class UnidadeGeradoraRepository : Repository<UnidadeGeradora>, IUnidadeGeradoraRepository
    {        
        public UnidadeGeradoraRepository(WEBPMODbContext context) : base(context)
        {
        }

        public IList<UnidadeGeradora> FindByKeys(params string[] ids)
        {
            throw new NotImplementedException();
        }
    }
}
