namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class DadoColetaNaoEstruturadoRepository : Repository<DadoColetaNaoEstruturado>, IDadoColetaNaoEstruturadoRepository
    {
        public DadoColetaNaoEstruturado ObterDadoColetaNaoEstruturado(DadoColetaInsumoNaoEstruturadoFilter filtros)
        {
            var query = EntitySet.AsQueryable();
            query = query.Where(row => row.ColetaInsumo.Id == filtros.IdColetaInsumo);

            if (query.Any()) return query.ToList().First();

            return null;
        }

        public void DeletarPorIdGabarito(IList<int> idsGabarito)
        {
            string sql = @"DELETE [dcne] from [dbo].[tb_dadocoletanaoestruturado] as [dcne]
                           inner join [dbo].[tb_dadocoleta] as [dc]
                            on [dc].[id_dadocoleta] = [dcne].[id_dadocoleta]
                            where [dc].[id_gabarito] in ({0})";

            Context.Database.ExecuteSqlCommand(string.Format(sql, String.Join(",", idsGabarito)));
        }

        public IList<DadoColetaNaoEstruturado> ObterDadosColetaNaoEstruturado(ArquivosSemanaOperativaFilter filtro)
        {
            var query = EntitySet.AsQueryable();

            query = query.Where(row => row.Arquivos.Any());
            query = query.Where(row => row.ColetaInsumo.SemanaOperativa.Id == filtro.IdSemanaOperativa);

            if (filtro.IsConsiderarInsumosConvergenciaCCEE.HasValue ||
                filtro.IsConsiderarInsumosDECOMP.HasValue ||
                filtro.IsConsiderarInsumosProcessamento.HasValue ||
                filtro.IsConsiderarInsumosPublicacao.HasValue)
            {
                query = from dado in query
                        join ci in Context.Set<ColetaInsumo>()
                        on dado.ColetaInsumo.Id equals ci.Id
                        join ine in Context.Set<InsumoNaoEstruturado>()
                        on ci.Insumo.Id equals ine.Id
                        where
                            (filtro.IsConsiderarInsumosConvergenciaCCEE.HasValue && ine.IsUtilizadoConvergencia == filtro.IsConsiderarInsumosConvergenciaCCEE.Value) ||
                            (filtro.IsConsiderarInsumosDECOMP.HasValue && ine.IsUtilizadoDECOMP == filtro.IsConsiderarInsumosDECOMP.Value) ||
                            (filtro.IsConsiderarInsumosProcessamento.HasValue && ine.IsUtilizadoProcessamento == filtro.IsConsiderarInsumosProcessamento.Value) ||
                            (filtro.IsConsiderarInsumosPublicacao.HasValue && ine.IsUtilizadoPublicacao == filtro.IsConsiderarInsumosPublicacao.Value)
                        select dado;
            }


            return query.ToList();
        }
    }
}
