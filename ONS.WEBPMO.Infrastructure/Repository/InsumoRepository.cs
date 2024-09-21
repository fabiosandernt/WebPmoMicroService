using System.Collections.Generic;
using System.Linq;
using ONS.Common.Repositories.Impl;
using ONS.SGIPMO.Domain.Entities;
using ONS.Common.Util;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories
{
    using ONS.Common.Configuration;
    using ONS.SGIPMO.Domain.Entities.Filters;
    using System;
    using ONS.Common.Util.Pagination;

    public class InsumoRepository : Repository<Insumo>, IInsumoRepository
    {
        public IList<InsumoEstruturado> ConsultarInsumoEstruturadoComGrandezaAtiva(TipoColetaEnum tipoColeta,
            CategoriaInsumoEnum? categoria = null)
        {
            var query = Context.Set<InsumoEstruturado>()
                .Where(insumo => insumo.TipoColeta.Id == (int)tipoColeta
                    && insumo.Grandezas.Any(grandeza => grandeza.Ativo));

            if (categoria.HasValue)
            {
                query = query.Where(insumo => insumo.CategoriaInsumo.Id == (int)categoria);
            }

            return query.OrderBy(insumo => insumo.Nome).ToList();
        }

        public IList<Insumo> ConsultarPorNomeLike(string nomeInsumo)
        {
            var query = EntitySet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nomeInsumo))
            {
                query = query.Where(i => i.Nome.ToLower().Contains(nomeInsumo.ToLower()));
            }
            return query
                .Distinct()
                .OrderBy(i => i.Nome)
                .Take(ONSConfigurationManager.GetSettings(ONSConfigurationManager.ConfigNameResultsByPage, 10))
                .ToList();
        }

        public Insumo ConsultarPorNome(string nomeInsumo)
        {
            var query = EntitySet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nomeInsumo))
            {
                query = query.Where(i => i.Nome.ToLower().Trim().Equals(nomeInsumo.ToLower().Trim()));
            }
            return query.FirstOrDefault();
        }

        public Insumo ConsultarPorSigla(string siglaInsumo)
        {
            var query = EntitySet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(siglaInsumo))
            {
                query = query.Where(i => i.SiglaInsumo.ToLower().Trim().Equals(siglaInsumo.ToLower().Trim()));
            }
            return query.FirstOrDefault();
        }

        public IList<Insumo> ConsultarInsumosNaoEstruturadoEEstruturadoComGrandezaAtiva()
        {
            string tipoInsumoNaoEstruturado = char.ToString((char)TipoInsumoEnum.NaoEstruturado);
            var query = from insumo in EntitySet
                        join grandeza in Context.Set<Grandeza>() on insumo.Id equals grandeza.Insumo.Id into grandezas
                        from grandeza in grandezas.DefaultIfEmpty()
                        where grandeza == null && insumo.TipoInsumo == tipoInsumoNaoEstruturado || grandeza.Ativo
                        select insumo;

            return query.ToList();
        }

        public IList<InsumoNaoEstruturado> ConsultarInsumoNaoEstruturado()
        {
            return Context.Set<InsumoNaoEstruturado>().ToList();
        }

        public IList<Insumo> ConsultaInsumoPorIds(params int[] ids)
        {
            return EntitySet.Where(insumo => ids.Contains(insumo.Id)).ToList();
        }

        public IList<Insumo> ConsultarPorInsulmoFiltro(InsumoFiltro filtro)
        {
            return CriarQueryConsultaInsumoFiltro(filtro).ToList();
        }

        public PagedResult<Insumo> ConsultarPorInsumoFiltroPaginado(InsumoFiltro filtro)
        {
            return FindPaged(CriarQueryConsultaInsumoFiltro(filtro), filtro.PageIndex, filtro.PageSize,
                insumo => insumo.OrderBy(i => i.OrdemExibicao));
        }

        public IList<Insumo> ConsultarInsumosPorSemanaOperativaAgentes(int idSemanaOperativa, params int[] idsAgente)
        {
            var query = from insumo in EntitySet
                        join coletaInsumo in Context.Set<ColetaInsumo>() 
                            on insumo.Id equals coletaInsumo.Insumo.Id
                        where coletaInsumo.SemanaOperativa.Id == idSemanaOperativa
                            && idsAgente.Contains(coletaInsumo.Agente.Id)
                        select insumo;

            return query.Distinct().ToList();
        }

        private IQueryable<Insumo> CriarQueryConsultaInsumoFiltro(InsumoFiltro filtro)
        {
            /* Insumo não estruturado não possui TipoColetaId e CategoriaId */
            if (!string.IsNullOrEmpty(filtro.TipoInsumo) 
                && filtro.TipoInsumo == TipoInsumoEnum.NaoEstruturado.ToChar()
                && (filtro.TipoColetaId.HasValue || filtro.CategoriaId.HasValue))
            {
                return Enumerable.Empty<Insumo>().AsQueryable();
            }

            var query = EntitySet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.Nome))
            {
                query = query.Where(insumo => insumo.Nome.ToLower().Trim().Contains(filtro.Nome.ToLower().Trim()));
            }

            if (!string.IsNullOrWhiteSpace(filtro.SiglaInsumo))
            {
                query = query.Where(insumo => insumo.SiglaInsumo.ToLower().Trim().Contains(filtro.SiglaInsumo.ToLower().Trim()));
            }

            if (!string.IsNullOrEmpty(filtro.TipoInsumo))
            {
                if (filtro.TipoInsumo == TipoInsumoEnum.NaoEstruturado.ToChar())
                {
                    query = query.OfType<InsumoNaoEstruturado>();
                }
                else
                {
                    query = query.OfType<InsumoEstruturado>();
                }
            }

            if (filtro.CategoriaId.HasValue)
            {
                query = query.OfType<InsumoEstruturado>()
                    .Where(estruturado => estruturado.CategoriaInsumo.Id == filtro.CategoriaId);
            }

            if (filtro.TipoColetaId.HasValue)
            {
                query = query.OfType<InsumoEstruturado>()
                    .Where(estruturado => estruturado.TipoColeta.Id == filtro.TipoColetaId);
            }

            return query;
        }
    }
}
