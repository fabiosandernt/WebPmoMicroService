using ONS.Common.Repositories.Impl;
using ONS.SGIPMO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    using Common.Util.Pagination;
    using Entities.Filters;

    public class OrigemColetaRepository : Repository<OrigemColeta>, IOrigemColetaRepository
    {
        public IList<OrigemColeta> ConsultarPorTipo(TipoOrigemColetaEnum tipo)
        {
            return EntitySet
                .WhereIsOfType(tipo)
                .OrderBy(origemColeta => origemColeta.Nome)
                .ToList();
        }

        public IList<OrigemColeta> ConsultarPorTipoNome(TipoOrigemColetaEnum tipo, string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return EntitySet
                .WhereIsOfType(tipo).OrderBy(oc => oc.Nome).Take(10).ToList();
            }
            return EntitySet.WhereIsOfType(tipo).Where(oc => oc.Nome.ToLower().Contains(nome.ToLower()))
                .OrderBy(oc => oc.Nome).ToList();
        }

        public IList<OrigemColeta> ConsultarPorIds(IList<string> ids)
        {
            return EntitySet.Where(origemColeta => ids.Any(id => id == origemColeta.Id)).OrderBy(origemColeta => origemColeta.Nome).ToList();
        }

        public IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorUsina(string idUsina)
        {
            return EntitySet
                .OfType<UnidadeGeradora>()
                .Where(unidadeGeradora => unidadeGeradora.Usina.Id == idUsina)
                .OrderBy(unidadeGeradora => unidadeGeradora.Nome)
                .ToList();
        }

        public T FindByKey<T>(object key) where T : OrigemColeta
        {
            return EntitySet.OfType<T>().FirstOrDefault(origem => origem.Id == (string)key);
        }

        public PagedResult<OrigemColeta> ConsultarOrigemColetasParticipamGabaritoPaginado(GabaritoParticipantesFilter filter)
        {

            if (filter.TipoOrigemColeta == TipoOrigemColetaEnum.Usina)
            {
                var usinasParticipantes = this.CriarFiltroOrigemColetaParticipanteGabarito(filter).OfType<Usina>();

                filter.TipoOrigemColeta = TipoOrigemColetaEnum.UnidadeGeradora;
                var usinasUgesParticipantes =
                    this.CriarFiltroOrigemColetaParticipanteGabarito(filter).OfType<UnidadeGeradora>().Select(uge => uge.Usina);

                var usinas = usinasParticipantes.Union(usinasUgesParticipantes);

                return this.FindPaged(usinas, filter.PageIndex, filter.PageSize, coletas => coletas.OrderBy(coleta => coleta.Nome));

            }

            var origensColetas = this.CriarFiltroOrigemColetaParticipanteGabarito(filter);

            if (filter.TipoOrigemColeta == TipoOrigemColetaEnum.UnidadeGeradora)
            {
                var uges = origensColetas.OfType<UnidadeGeradora>().Where(uge => uge.Usina.Id == filter.IdUsinaPai);

                return this.FindPaged(uges, filter.PageIndex, filter.PageSize, coletas => coletas.OrderBy(coleta => coleta.Nome));
            }
            return this.FindPaged(origensColetas, filter.PageIndex, filter.PageSize, coletas => coletas.OrderBy(coleta => coleta.Nome));
        }

        private IQueryable<OrigemColeta> CriarFiltroOrigemColetaParticipanteGabarito(GabaritoParticipantesFilter filter)
        {
            IQueryable<OrigemColeta> origemColetas = EntitySet.Where(
                    origemColeta => origemColeta.Gabaritos.Any(gabarito => gabarito.IsPadrao == filter.IsPadrao));

            if (filter.IdAgente.HasValue)
            {
                origemColetas = origemColetas.Where(
                        origemColeta => origemColeta.Gabaritos.Any(gabarito => gabarito.Agente.Id == filter.IdAgente.Value));
            }

            if (filter.IdSemanaOperativa.HasValue)
            {
                origemColetas =
                    origemColetas.Where(
                        agente => agente.Gabaritos.Any(gabarito => gabarito.SemanaOperativa.Id == filter.IdSemanaOperativa));
            }

            if (!String.IsNullOrWhiteSpace(filter.CodigoPerfilONS))
            {
                origemColetas =
                    origemColetas.Where(
                        agente => agente.Gabaritos.Any(gabarito => gabarito.CodigoPerfilONS == filter.CodigoPerfilONS));
            }

            return origemColetas.WhereIsOfType(filter.TipoOrigemColeta);
        }


        public IList<Usina> ConsultarUsinaParticipanteGabaritoPorColetaInsumo(int idColetaInsumo)
        {
            var query = from coleta in Context.Set<ColetaInsumo>()
                        join gabarito in Context.Set<Gabarito>()
                            on new
                                {
                                    idInsumo = coleta.Insumo.Id,
                                    idAgente = coleta.Agente.Id,
                                    codigoPerfil = coleta.CodigoPerfilONS,
                                    idSemana = coleta.SemanaOperativa.Id
                                }
                            equals
                            new
                                {
                                    idInsumo = gabarito.Insumo.Id,
                                    idAgente = gabarito.Agente.Id,
                                    codigoPerfil = gabarito.CodigoPerfilONS,
                                    idSemana = gabarito.SemanaOperativa.Id
                                }
                        join unidadeGeradora in Context.Set<UnidadeGeradora>()
                            on gabarito.OrigemColeta.Id equals unidadeGeradora.Id
                        where coleta.Id == idColetaInsumo
                        orderby unidadeGeradora.Usina.Nome
                        select unidadeGeradora.Usina;

            return query.Distinct().ToList();
        }

        public IList<UnidadeGeradora> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumoUsina(int idColetaInsumo, string idUsina)
        {
            var query = from coleta in Context.Set<ColetaInsumo>()
                        join gabarito in Context.Set<Gabarito>()
                            on new
                            {
                                idInsumo = coleta.Insumo.Id,
                                idAgente = coleta.Agente.Id,
                                codigoPerfil = coleta.CodigoPerfilONS,
                                idSemana = coleta.SemanaOperativa.Id
                            }
                            equals
                            new
                            {
                                idInsumo = gabarito.Insumo.Id,
                                idAgente = gabarito.Agente.Id,
                                codigoPerfil = gabarito.CodigoPerfilONS,
                                idSemana = gabarito.SemanaOperativa.Id
                            }
                        join unidadeGeradora in Context.Set<UnidadeGeradora>()
                            on gabarito.OrigemColeta.Id equals unidadeGeradora.Id
                        where coleta.Id == idColetaInsumo && unidadeGeradora.Usina.Id == idUsina
                        orderby unidadeGeradora.Nome
                        select unidadeGeradora;

            return query.Distinct().ToList();
        }

        public IList<UnidadeGeradora> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumo(int idColetaInsumo)
        {
            var query = from coleta in Context.Set<ColetaInsumo>()
                        join gabarito in Context.Set<Gabarito>()
                            on new
                            {
                                idInsumo = coleta.Insumo.Id,
                                idAgente = coleta.Agente.Id,
                                codigoPerfil = coleta.CodigoPerfilONS,
                                idSemana = coleta.SemanaOperativa.Id
                            }
                            equals
                            new
                            {
                                idInsumo = gabarito.Insumo.Id,
                                idAgente = gabarito.Agente.Id,
                                codigoPerfil = gabarito.CodigoPerfilONS,
                                idSemana = gabarito.SemanaOperativa.Id
                            }
                        join unidadeGeradora in Context.Set<UnidadeGeradora>()
                            on gabarito.OrigemColeta.Id equals unidadeGeradora.Id
                        where coleta.Id == idColetaInsumo
                        orderby unidadeGeradora.Nome
                        select unidadeGeradora;

            return query.Distinct().ToList();
        }

        public IList<UnidadeGeradoraManutencaoSGIDTO> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumos(List<int> idColetaInsumos)
        {
            var query = from coleta in Context.Set<ColetaInsumo>()
                        join gabarito in Context.Set<Gabarito>()
                            on new
                            {
                                idInsumo = coleta.Insumo.Id,
                                idAgente = coleta.Agente.Id,
                                codigoPerfil = coleta.CodigoPerfilONS,
                                idSemana = coleta.SemanaOperativa.Id
                            }
                            equals
                            new
                            {
                                idInsumo = gabarito.Insumo.Id,
                                idAgente = gabarito.Agente.Id,
                                codigoPerfil = gabarito.CodigoPerfilONS,
                                idSemana = gabarito.SemanaOperativa.Id
                            }
                        join unidadeGeradora in Context.Set<UnidadeGeradora>()
                            on gabarito.OrigemColeta.Id equals unidadeGeradora.Id
                        where idColetaInsumos.Contains(coleta.Id)
                        orderby unidadeGeradora.Nome
                        select new UnidadeGeradoraManutencaoSGIDTO() {
                            NomeUnidadeGeradora = unidadeGeradora.Nome,
                            IdUnidadeGeradora = unidadeGeradora.Id,
                            IdColetaInsumo = coleta.Id,
                            IdGabarito = gabarito.Id,
                            Versao = coleta.Versao
                        };

            return query.Distinct().ToList();
        }
    }
}
