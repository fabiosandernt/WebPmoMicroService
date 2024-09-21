namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class CategoriaInsumoRepository : Repository<CategoriaInsumo>, ICategoriaInsumoRepository
    {
        public override IList<CategoriaInsumo> All()
        {
            return base.All().Where(c => c.UsoPmo).ToList();
        }

        public override IList<CategoriaInsumo> All(Func<IQueryable<CategoriaInsumo>, IOrderedQueryable<CategoriaInsumo>> orderBy)
        {
            return base.All(orderBy).Where(c => c.UsoPmo).ToList();
        }
    }
}
