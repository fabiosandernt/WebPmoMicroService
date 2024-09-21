namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class PMORepository : Repository<PMO>, IPMORepository
    {
        public DbContext _Context2;

        public PMORepository()
        {
            _Context2 = ApplicationContext.Resolve<DbContext>("SGIPMOModel");
        }

        public PMO ObterPorFiltro(PMOFilter filtro)
        {
            var query = EntitySet.AsQueryable();

            if (filtro.Ano.HasValue)
            {
                query = query.Where(e => e.AnoReferencia == filtro.Ano.Value);
            }

            if (filtro.Mes.HasValue)
            {
                query = query.Where(e => e.MesReferencia == filtro.Mes.Value);
            }

            return query.FirstOrDefault();
        }

        public PMO ObterPorFiltroExterno(PMOFilter filtro)
        {
            var query = _Context2.Set<PMO>().AsQueryable();

            if (filtro.Ano.HasValue)
            {
                query = query.Where(e => e.AnoReferencia == filtro.Ano.Value);
            }

            if (filtro.Mes.HasValue)
            {
                query = query.Where(e => e.MesReferencia == filtro.Mes.Value);
            }

            return query.FirstOrDefault();
        }

        public int ObterQuantidadeSemanasPMO(int idSemanaOperativa)
        {
            return EntitySet.Single(pmo => pmo.SemanasOperativas.Any(s => s.Id == idSemanaOperativa))
                .SemanasOperativas.Count;
        }
    }
}
