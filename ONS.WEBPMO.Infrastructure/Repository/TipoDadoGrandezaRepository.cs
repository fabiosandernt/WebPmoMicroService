namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class TipoDadoGrandezaRepository : Repository<TipoDadoGrandeza>, ITipoDadoGrandezaRepository
    {
        public override IList<TipoDadoGrandeza> All()
        {
            return base.All().Where(c => c.UsoPmo).ToList();
        }
        public override IList<TipoDadoGrandeza> All(Func<IQueryable<TipoDadoGrandeza>, IOrderedQueryable<TipoDadoGrandeza>> orderBy)
        {
            return base.All(orderBy).Where(c => c.UsoPmo).ToList();
        }
    }
}
