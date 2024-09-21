namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class TipoColetaRepository : Repository<TipoColeta>, ITipoColetaRepository
    {
        public override IList<TipoColeta> All()
        {
            return base.All().Where(c => c.UsoPmo).ToList();
        }
        public override IList<TipoColeta> All(Func<IQueryable<TipoColeta>, IOrderedQueryable<TipoColeta>> orderBy)
        {
            return base.All(orderBy).Where(c => c.UsoPmo).ToList();
        }
    }
}
