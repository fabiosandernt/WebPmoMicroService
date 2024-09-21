namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories
{
    public class DadoColetaRepository : Repository<DadoColeta>, IDadoColetaRepository
    {
        public void DeletarPorIdGabarito(IList<int> idsGabarito)
        {
            string sql = @"DELETE [dc] from [dbo].[tb_dadocoleta] as [dc]
                            where [dc].[id_gabarito] in ({0})";

            Context.Database.ExecuteSqlCommand(string.Format(sql, String.Join(",", idsGabarito)));
        }

        public IList<DadoColeta> ConsutarParaGeracaoBlocos(int idSemanaOperativa)
        {
            var query = from dado in EntitySet
                        join coleta in Context.Set<ColetaInsumo>() on dado.ColetaInsumo.Id equals coleta.Id
                        join insumo in Context.Set<InsumoEstruturado>() on coleta.Insumo.Id equals insumo.Id
                        where !(dado is DadoColetaNaoEstruturado)
                        where coleta.SemanaOperativa.Id == idSemanaOperativa
                            && !string.IsNullOrEmpty(insumo.TipoBloco)
                            && (dado.Grandeza == null || dado.Grandeza.ParticipaBlocoMontador)
                        select dado;


            //var query = from dado in EntitySet
            //            join coleta in Context.Set<ColetaInsumo>() on dado.ColetaInsumo.Id equals coleta.Id
            //            join insumoe in Context.Set<InsumoEstruturado>() on coleta.Insumo.Id equals insumoe.Id into ie
            //            from insumoe in ie.DefaultIfEmpty()
            //            join insumone in Context.Set<InsumoNaoEstruturado>() on coleta.Insumo.Id equals insumone.Id into ine
            //            from insumone in ine.DefaultIfEmpty()
            //            join dadoe in Context.Set<DadoColetaEstruturado>() on dado.Id equals dadoe.Id into de
            //            from dadoe in de.DefaultIfEmpty()
            //            join dadone in Context.Set<DadoColetaNaoEstruturado>() on dado.Id equals dadone.Id into dne
            //            from dadone in dne.DefaultIfEmpty()
            //            join dadom in Context.Set<DadoColetaManutencao>() on dado.Id equals dadom.Id into dm
            //            from dadom in dm.DefaultIfEmpty()
            //            where coleta.SemanaOperativa.Id == idSemanaOperativa
            //            select dado;

            return query
                .Include(dado => dado.ColetaInsumo.Insumo)
                .Include(dado => dado.ColetaInsumo.SemanaOperativa)
                .Include(dado => dado.ColetaInsumo.Agente)
                .Include(dado => dado.Grandeza)
                .ToList();
        }

        public IList<DadoColeta> ConsultarDadosComInsumoParticipaBlocoUH(int idSemanaOperativa)
        {
            var query = from dado in EntitySet
                        join ci in Context.Set<ColetaInsumo>() on dado.ColetaInsumo.Id equals ci.Id
                        join ie in Context.Set<InsumoEstruturado>() on ci.Insumo.Id equals ie.Id
                        where
                            ci.SemanaOperativa.Id == idSemanaOperativa
                            && ie.TipoBloco == TipoBlocoEnum.UH.ToString()
                        select dado;

            return query
                .Include(dado => dado.ColetaInsumo.Insumo)
                .Include(dado => dado.ColetaInsumo.SemanaOperativa)
                .Include(dado => dado.ColetaInsumo.Agente)
                .Include(dado => dado.Grandeza)
                .ToList();
        }

        public IList<DadoColeta> ConsultarDadosColetaAssociadosGabaritos(IList<int> idsGabarito)
        {
            var gabaritos = from g in Context.Set<Gabarito>()
                            where idsGabarito.Contains(g.Id)
                            select g;

            return gabaritos.ToList().SelectMany(g => g.DadosColeta).ToList();
        }
    }
}
