using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class TipoDadoGrandezaRepository : Repository<TipoDadoGrandeza>, ITipoDadoGrandezaRepository
    {
        public TipoDadoGrandezaRepository(WEBPMODbContext context) : base(context)
        {
        }

        //public override IList<TipoDadoGrandeza> All()
        //{
        //    return base.All().Where(c => c.UsoPmo).ToList();
        //}
        //public override IList<TipoDadoGrandeza> All(Func<IQueryable<TipoDadoGrandeza>, IOrderedQueryable<TipoDadoGrandeza>> orderBy)
        //{
        //    return base.All(orderBy).Where(c => c.UsoPmo).ToList();
        //}
    }
}
