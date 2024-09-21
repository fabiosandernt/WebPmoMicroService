namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class GrandezaRepository : Repository<Grandeza>, IGrandezaRepository
    {
        public bool ExisteGrandezaPorEstagio(int idInsumo)
        {
            return EntitySet.Where(g => g.Insumo.Id == idInsumo).Any(g => g.IsColetaPorEstagio);
        }

        public bool ExistePreAprovadoComAlteracao(int idInsumo)
        {
            return EntitySet.Where(g => g.Insumo.Id == idInsumo).Any(g => g.IsPreAprovadoComAlteracao);
        }


        public bool ExisteGrandezaPorPatamar(int idInsumo)
        {
            return EntitySet.Where(g => g.Insumo.Id == idInsumo).Any(g => g.IsColetaPorPatamar);
        }

        public bool ExisteGrandezaPorLimite(int idInsumo)
        {
            return EntitySet.Where(g => g.Insumo.Id == idInsumo).Any(g => g.IsColetaPorLimite);
        }

        public bool ExisteDadosColetaNaGrandeza(int idGrandeza)
        {
            return EntitySet.Include(g => g.DadosColeta).Where(g => g.Id == idGrandeza && g.DadosColeta.Any()).Any();
        }

        public IList<Grandeza> ConsultarPorFiltro(GrandezaFilter filter)
        {
            var query = EntitySet.AsQueryable();

            if (filter.IdsGrandeza.Any())
            {
                query = query.Where(g => filter.IdsGrandeza.Contains(g.Id));
            }

            if (filter.IdInsumo.HasValue)
            {
                query = query.Where(g => g.Insumo.Id == filter.IdInsumo);
            }

            if (filter.IsOrdenacaoPadrao)
            {
                query = query.OrderBy(g => g.OrdemExibicao);
            }

            return query.ToList();
        }

        public Grandeza ConsultarPorNome(string nomeGrandeza)
        {
            var query = EntitySet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nomeGrandeza))
            {
                query = query.Where(i => i.Nome.ToLower().Equals(nomeGrandeza.ToLower()));
            }
            return query.FirstOrDefault();
        }

        public IList<Grandeza> ConsultarPorInsumo(int idInsumo)
        {
            return Context.Set<Grandeza>().Where(grandeza => grandeza.Insumo.Id == idInsumo).ToList();
        }
    }
}
