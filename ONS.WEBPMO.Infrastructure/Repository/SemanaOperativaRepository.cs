namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class SemanaOperativaRepository : Repository<SemanaOperativa>, ISemanaOperativaRepository
    {
        public IList<SemanaOperativa> ConsultarSemanasOperativasComGabarito()
        {
            return QuerySemanasOperativasComGabarito().ToList();
        }

        public IList<SemanaOperativa> ConsultarEstudoPorNome(string nomeEstudo, int quantidadeMaxima)
        {
            var query = QuerySemanasOperativasComGabarito();
            if (!string.IsNullOrWhiteSpace(nomeEstudo))
            {
                query = query.Where(e => e.Nome.ToLower().Contains(nomeEstudo.ToLower()));
            }
            return query.Take(quantidadeMaxima).ToList();
        }

        public IList<SemanaOperativa> ConsultarEstudoPorNomeEStatus(string nomeEstudo, int? idStatus, int quantidadeMaxima)
        {
            var query = EntitySet.AsQueryable();
            if (!string.IsNullOrWhiteSpace(nomeEstudo))
            {
                query = query.Where(e => e.Nome.ToLower().Contains(nomeEstudo.ToLower()));
            }
            if (idStatus.HasValue)
            {
                query = query.Where(e => e.Situacao.Id == idStatus.Value);
            }
            return query.Take(quantidadeMaxima).ToList();
        }

        private IQueryable<SemanaOperativa> QuerySemanasOperativasComGabarito()
        {
            return EntitySet
                .Where(e => e.Gabaritos.Any())
                .OrderByDescending(so => so.DataInicioSemana)
                .ThenByDescending(so => so.DataFimManutencao);
        }
    }
}
