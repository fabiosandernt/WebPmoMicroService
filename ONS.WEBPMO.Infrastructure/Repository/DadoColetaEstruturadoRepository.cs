using System.Data.Entity;
using System.Data.SqlClient;
using ONS.Common.Repositories.Impl;
using ONS.SGIPMO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ONS.SGIPMO.Domain.Entities.Filters;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class DadoColetaEstruturadoRepository : Repository<DadoColetaEstruturado>, IDadoColetaEstruturadoRepository
    {
        public IList<DadoColetaEstruturado> ConsultarPorFiltro(DadoColetaInsumoFilter filter)
        {
            return EntitySet
                .Where(dado => filter.IdsDadoColeta.Contains(dado.Id))
                .ToList();
        }

        public IList<DadoColetaEstruturado> ConsultarDadosComInsumoEGrandezaParticipaBlocoGNL(int idSemanaOperativa)
        {
            string tipoBlocoTG = TipoBlocoEnum.TG.ToString();

            var query = from dado in EntitySet
                        join ci in Context.Set<ColetaInsumo>() on dado.ColetaInsumo.Id equals ci.Id
                        join ie in Context.Set<InsumoEstruturado>() on ci.Insumo.Id equals ie.Id
                        join gz in Context.Set<Grandeza>() on ie.Id equals gz.Insumo.Id
                        where
                            ci.SemanaOperativa.Id == idSemanaOperativa
                            && ie.TipoBloco == tipoBlocoTG
                            && gz.ParticipaBlocoMontador
                        select dado;

            return query
                .Include(dado => dado.ColetaInsumo.Insumo)
                .Include(dado => dado.ColetaInsumo.SemanaOperativa)
                .Include(dado => dado.ColetaInsumo.Agente)
                .Include(dado => dado.Grandeza)
                .ToList();
        }

        public IList<DadoColetaEstruturado> ConsultarDadosComInsumoEGrandezaParticipaBloco(int idSemanaOperativa)
        {
            string tipoBlocoTG = TipoBlocoEnum.TG.ToString();

            var query = from dado in EntitySet
                        join ci in Context.Set<ColetaInsumo>() on dado.ColetaInsumo.Id equals ci.Id
                        join gz in Context.Set<Grandeza>() on dado.Grandeza.Id equals gz.Id
                        join ie in Context.Set<InsumoEstruturado>() on gz.Insumo.Id equals ie.Id
                        where
                            ci.SemanaOperativa.Id == idSemanaOperativa
                            && ie.TipoBloco != tipoBlocoTG
                            && gz.ParticipaBlocoMontador
                        select dado;

            return query
                .Include(dado => dado.ColetaInsumo.Insumo)
                .Include(dado => dado.ColetaInsumo.SemanaOperativa)
                .Include(dado => dado.ColetaInsumo.Agente)
                .Include(dado => dado.Grandeza)
                .ToList();
        }

        public void DeletarPorIdGabarito(IList<int> idsGabarito)
        {
            string sql = @"DELETE [dce] from [dbo].[tb_dadocoletaestruturado] as [dce]
                           inner join [dbo].[tb_dadocoleta] as [dc]
                            on [dc].[id_dadocoleta] = [dce].[id_dadocoleta]
                            where [dc].[id_gabarito] in ({0})";

            Context.Database.ExecuteSqlCommand(string.Format(sql, String.Join(",", idsGabarito)));
        }

        public int ContarQuantidadeLinhasDadosEstruturados(int idColetaInsumo)
        {
            var query = (from g in Context.Set<Grandeza>()
                         join ie in Context.Set<InsumoEstruturado>() on g.Insumo.Id equals ie.Id
                         join ga in Context.Set<Gabarito>() on ie.Id equals ga.Insumo.Id
                         join ci in Context.Set<ColetaInsumo>() on 
                             new
                             {
                                 InsumoId = ga.Insumo.Id, 
                                 AgenteId = ga.Agente.Id,
                                 CodigoPerfilONS = ga.CodigoPerfilONS,
                                 SemanaOperativaId = ga.SemanaOperativa.Id
                             }
                             equals new
                             {
                                 InsumoId = ci.Insumo.Id,
                                 AgenteId = ci.Agente.Id,
                                 CodigoPerfilONS = ci.CodigoPerfilONS,
                                 SemanaOperativaId = ci.SemanaOperativa.Id
                             } 
                         where ci.Id == idColetaInsumo
                                && g.Ativo
                         select g);

            var count1 = query.Count(g => g.IsColetaPorPatamar && g.IsColetaPorLimite) * 6;
            var count2 = query.Count(g => g.IsColetaPorPatamar && !g.IsColetaPorLimite) * 3;
            var count3 = query.Count(g => !g.IsColetaPorPatamar && g.IsColetaPorLimite) * 2;
            var count4 = query.Count(g => !g.IsColetaPorPatamar && !g.IsColetaPorLimite);

            return count1 + count2 + count3 + count4;
        }
    }
}
