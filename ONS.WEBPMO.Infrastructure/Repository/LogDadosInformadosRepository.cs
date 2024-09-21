namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class LogDadosInformadosRepository : Repository<LogDadosInformados>, ILogDadosInformadosRepository
    {
        public PagedResult<LogDadosInformados> ConsultarPorFiltro(LogDadosInformadosFilter filter)
        {
            var query = EntitySet
                .Include(log => log.Agente)
                .AsQueryable();

            DateTime inicio = DateTime.Parse(filter.DataInicioAbrangencia);
            DateTime fim = DateTime.Parse(filter.DataFimAbrangencia).AddDays(1);


            int skip = (filter.PageIndex - 1) * filter.PageSize;

            var values = query
                    .Where(dado => dado.Nom_usuario.Contains(filter.Nome))
                    .Where(dado => dado.Agente.Nome.Contains(filter.Empresa))
                    .Where(dado => dado.Din_acao > inicio && dado.Din_acao < fim)
                    .OrderByDescending(dado => dado.Din_acao)
                    .Skip(skip)
                    .Take(filter.PageSize)
                    .ToList();

            var count = query
                    .Where(dado => dado.Nom_usuario.Contains(filter.Nome))
                    .Where(dado => dado.Agente.Nome.Contains(filter.Empresa))
                    .Where(dado => dado.Din_acao > inicio && dado.Din_acao < fim)
                    .Count();

            return new PagedResult<LogDadosInformados>(values, count, filter.PageIndex, filter.PageSize);
        }
    }
}
