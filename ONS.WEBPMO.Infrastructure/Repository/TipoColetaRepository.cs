using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class TipoColetaRepository : Repository<TipoColeta>, ITipoColetaRepository
    {
       
        public TipoColetaRepository(WEBPMODbContext context) : base(context)
        {
        }
        //public override IList<TipoColeta> All()
        //{
        //    return base.All().Where(c => c.UsoPmo).ToList();
        //}
        //public override IList<TipoColeta> All(Func<IQueryable<TipoColeta>, IOrderedQueryable<TipoColeta>> orderBy)
        //{
        //    return base.All(orderBy).Where(c => c.UsoPmo).ToList();
        //}
    }
}
