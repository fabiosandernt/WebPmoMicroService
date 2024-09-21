namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class ColetaInsumoRepository : Repository<ColetaInsumo>, IColetaInsumoRepository
    {
        public bool Any(int idAgente, int idInsumo, int idSemanaOperativa, string codigoPerfilOns)
        {
            if (string.IsNullOrEmpty(codigoPerfilOns))
            {
                return EntitySet.Any(
                    coletaInsumo =>
                        coletaInsumo.Agente.Id == idAgente
                        && coletaInsumo.SemanaOperativa.Id == idSemanaOperativa
                        && coletaInsumo.Insumo.Id == idInsumo
                        && string.IsNullOrEmpty(codigoPerfilOns));
            }

            return EntitySet.Any(
                coletaInsumo =>
                    coletaInsumo.Agente.Id == idAgente
                    && coletaInsumo.SemanaOperativa.Id == idSemanaOperativa
                    && coletaInsumo.Insumo.Id == idInsumo
                    && coletaInsumo.CodigoPerfilONS == codigoPerfilOns);
        }

        public bool AnyNaoAprovado(int idSemanaOperativa)
        {
            return EntitySet.Any(
                coletaInsumo =>
                    coletaInsumo.Situacao.Id != (int)SituacaoColetaInsumoEnum.Aprovado &&
                    coletaInsumo.SemanaOperativa.Id == idSemanaOperativa);
        }

        public IList<ColetaInsumo> ConsultarColetasInsumoOrfaos(int idAgente, int idSemanaOperativa, string codigoPerfilONS)
        {
            var query = EntitySet.Where(coletaInsumo =>
                coletaInsumo.Agente.Id == idAgente
                && coletaInsumo.SemanaOperativa.Id == idSemanaOperativa);

            if (string.IsNullOrWhiteSpace(codigoPerfilONS))
            {
                query = query.Where(coletaInsumo => string.IsNullOrEmpty(coletaInsumo.CodigoPerfilONS));
                query = query.Where(coletaInsumo =>
                    !Context.Set<Gabarito>().Any(gabarito =>
                        gabarito.Agente.Id == coletaInsumo.Agente.Id
                        && gabarito.SemanaOperativa.Id == coletaInsumo.SemanaOperativa.Id
                        && gabarito.Insumo.Id == coletaInsumo.Insumo.Id
                        && string.IsNullOrEmpty(gabarito.CodigoPerfilONS)));
            }
            else
            {
                query = query.Where(coletaInsumo => coletaInsumo.CodigoPerfilONS == codigoPerfilONS);
                query = query.Where(coletaInsumo =>
                    !Context.Set<Gabarito>().Any(gabarito =>
                        gabarito.Agente.Id == coletaInsumo.Agente.Id
                        && gabarito.SemanaOperativa.Id == coletaInsumo.SemanaOperativa.Id
                        && gabarito.Insumo.Id == coletaInsumo.Insumo.Id
                        && gabarito.CodigoPerfilONS == coletaInsumo.CodigoPerfilONS));
            }

            return query.ToList();
        }

        public PagedResult<ColetaInsumo> ConsultarParaInformarDados(PesquisaMonitorarColetaInsumoFilter filter)
        {
            var query = EntitySet
                .Include(coletaInsumo => coletaInsumo.Agente)
                .Include(coletaInsumo => coletaInsumo.Situacao)
                .Include(coletaInsumo => coletaInsumo.Insumo)
                .AsQueryable();

            query = query.Where(coletaInsumo => coletaInsumo.SemanaOperativa.Id == filter.IdSemanaOperativa);

            if (filter.IdsAgentes.Any())
            {
                query = query.Where(coletaInsumo => filter.IdsAgentes.Contains(coletaInsumo.Agente.Id));
            }

            if (filter.IdsInsumo.Any())
            {
                query = query.Where(coletaInsumo => filter.IdsInsumo.Contains(coletaInsumo.Insumo.Id));
            }

            if (filter.IdsSituacaoColeta.Any())
            {
                query = query.Where(coletaInsumo => filter.IdsSituacaoColeta.Contains(coletaInsumo.Situacao.Id));
            }

            if (filter.PerfisONS.Any())
            {
                query = query.Where(coletaInsumo => filter.PerfisONS.Contains(coletaInsumo.CodigoPerfilONS)
                    || string.IsNullOrEmpty(coletaInsumo.CodigoPerfilONS));
            }

            int resutadosPorPagina = filter.PageSize;
            int skip = (filter.PageIndex - 1) * resutadosPorPagina;
            int quantidadeTotal = query.Count();

            IList<ColetaInsumo> coletasInsumo = query
               .OrderBy(coletaInsumo => coletaInsumo.Agente.Nome)
               .ThenBy(coletaInsumo => coletaInsumo.Insumo.Nome)
               .Skip(skip)
               .Take(resutadosPorPagina)
               .ToList();

            return new PagedResult<ColetaInsumo>(coletasInsumo, quantidadeTotal, filter.PageIndex, resutadosPorPagina);
        }

        public IList<ColetaInsumo> FindByKeys(IList<int> idsColetaInsumo)
        {
            return EntitySet
                .Where(coletaInsumo => idsColetaInsumo.Contains(coletaInsumo.Id))
                .ToList();
        }

        /* comentado devido a replicacao de codigo da branche sprint18_Web-PMO_Bug-76601 */
        public ColetaInsumo GetByKey(int idsColetaInsumo)
        {
            return EntitySet
                .Where(coletaInsumo => idsColetaInsumo == coletaInsumo.Id).FirstOrDefault();
        }

        public ColetaInsumo ObterColetaInsumoAnterior(ColetaInsumo coletaInsumoAtual)
        {
            return EntitySet
                .Where(ci => ci.Agente.Id == coletaInsumoAtual.Agente.Id
                    && ci.Insumo.Id == coletaInsumoAtual.Insumo.Id
                    && ci.SemanaOperativa.PMO.Id == coletaInsumoAtual.SemanaOperativa.PMO.Id
                    && ci.SemanaOperativa.Revisao < coletaInsumoAtual.SemanaOperativa.Revisao)
                .OrderByDescending(ci => ci.SemanaOperativa.Revisao)
                .FirstOrDefault();
        }

        public ColetaInsumo ObterColetaInsumoSemanaOperativaAnterior(ColetaInsumo coletaInsumoAtual)
        {
            if (coletaInsumoAtual.SemanaOperativa.PMO.MesReferencia == 1)
            {
                return EntitySet
                    .Where(ci => ci.Agente.Id == coletaInsumoAtual.Agente.Id
                        && ci.Insumo.Id == coletaInsumoAtual.Insumo.Id
                        && ci.SemanaOperativa.PMO.AnoReferencia < coletaInsumoAtual.SemanaOperativa.PMO.AnoReferencia
                        && ci.SemanaOperativa.PMO.Id != coletaInsumoAtual.SemanaOperativa.PMO.Id
                        && ci.SemanaOperativa.SituacaoId != null)
                    .OrderByDescending(ci => ci.SemanaOperativa.PMO.AnoReferencia).ThenByDescending(ci => ci.SemanaOperativa.PMO.MesReferencia).ThenByDescending(ci => ci.SemanaOperativa.Revisao).FirstOrDefault();
            }
            else
            {
                return EntitySet
                    .Where(ci => ci.Agente.Id == coletaInsumoAtual.Agente.Id
                        && ci.Insumo.Id == coletaInsumoAtual.Insumo.Id
                        && ci.SemanaOperativa.PMO.AnoReferencia <= coletaInsumoAtual.SemanaOperativa.PMO.AnoReferencia
                        && ci.SemanaOperativa.PMO.Id != coletaInsumoAtual.SemanaOperativa.PMO.Id
                        && ci.SemanaOperativa.SituacaoId != null)
                    .OrderByDescending(ci => ci.SemanaOperativa.PMO.AnoReferencia).ThenByDescending(ci => ci.SemanaOperativa.PMO.MesReferencia).ThenByDescending(ci => ci.SemanaOperativa.Revisao).FirstOrDefault();
            }
        }

        public void DeletarPorIdSemanaOperativa(int idSemanaOperativa)
        {
            var query = from ci in EntitySet
                        join so in Context.Set<SemanaOperativa>() on ci.SemanaOperativa.Id equals so.Id
                        where so.Id == idSemanaOperativa
                        select ci;

            Delete(query);
        }

        public IList<ColetaInsumo> ConsultarColetaParticipaBloco(int idSemanaOperativa)
        {
            var query = from ci in EntitySet
                        join ie in Context.Set<InsumoEstruturado>() on ci.InsumoId equals ie.Id
                        where ci.SemanaOperativaId == idSemanaOperativa
                              && !string.IsNullOrEmpty(ie.TipoBloco)
                        select ci;

            return query.ToList();
        }

        public IList<ColetaInsumo> ObterColetaInsumoPorSemanaOperativaInsumos(int idSemanaOperativa, IList<int> idInsumos)
        {
            var query = EntitySet.AsQueryable()
                .Include(ci => ci.Agente)
                .Include(ci => ci.Insumo)
                .Include(ci => ci.DadosColeta);

            query = query.Where(ci => ci.SemanaOperativaId == idSemanaOperativa && idInsumos.Contains(ci.InsumoId));

            return query.ToList();
        }
    }
}
