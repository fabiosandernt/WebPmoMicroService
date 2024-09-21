namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    using Common.Exceptions;
    using Common.Services.Impl;
    using Common.Util.Pagination;
    using Entities;
    using Entities.Filters;
    using ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta;
    using Repositories;
    using Services.Integrations;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public class OrigemColetaService : Service, IOrigemColetaService
    {
        private readonly IOrigemColetaRepository origemColetaRepository;
        private readonly IBDTService BDTService;

        public OrigemColetaService(
            IOrigemColetaRepository origemColetaRepository,
            IBDTService BDTService)
        {
            this.origemColetaRepository = origemColetaRepository;
            this.BDTService = BDTService;
        }

        public PagedResult<OrigemColeta> ConsultarOrigemColetasGabaritoPaginado(GabaritoParticipantesFilter filter)
        {
            return origemColetaRepository.ConsultarOrigemColetasParticipamGabaritoPaginado(filter);
        }

        public T ObterOrigemColetaPorChaveOnline<T>(string id) where T : OrigemColeta
        {
            var @switch = new Dictionary<Type, Func<OrigemColeta>>{
                { typeof(Reservatorio), () => BDTService.ConsultarReservatorioPorChaves(id).FirstOrDefault() },
                { typeof(Usina), () => BDTService.ConsultarUsinaPorChaves(id).FirstOrDefault() },
                { typeof(UnidadeGeradora), () => BDTService.ConsultarUnidadeGeradoraPorChaves(id).FirstOrDefault() },
                { typeof(Subsistema), () => BDTService.ConsultarTodosSubsistemas().FirstOrDefault(s => s.Id == id) }
            };

            return (T)@switch[typeof(T)]();
        }

        public T ObterOrigemColetaPorChave<T>(string id) where T : OrigemColeta
        {
            return origemColetaRepository.FindByKey<T>(id);
        }

        public OrigemColeta ObterOuCriarOrigemColetaPorId(string idOrigemColeta, TipoOrigemColetaEnum tipoOrigemColeta)
        {
            OrigemColeta origemColeta = origemColetaRepository.FindByKey(idOrigemColeta);

            if (origemColeta == null)
            {
                switch (tipoOrigemColeta)
                {
                    case TipoOrigemColetaEnum.Usina:
                        Usina usina = BDTService.ConsultarUsinaPorChaves(idOrigemColeta).First();
                        AtualizarSubsistema(usina);
                        origemColeta = usina;
                        break;
                    case TipoOrigemColetaEnum.Subsistema:
                        origemColeta = BDTService.ConsultarTodosSubsistemas().First(s => s.Id == idOrigemColeta);
                        break;
                    case TipoOrigemColetaEnum.UnidadeGeradora:
                        UnidadeGeradora unidadeGeradora = BDTService.ConsultarUnidadeGeradoraPorChaves(idOrigemColeta).First();
                        Usina usinaUnidade = (Usina)ObterOuCriarOrigemColetaPorId(unidadeGeradora.Usina.Id, TipoOrigemColetaEnum.Usina);
                        AtualizarSubsistema(usinaUnidade);
                        unidadeGeradora.Usina = usinaUnidade;
                        origemColeta = unidadeGeradora;
                        break;
                    case TipoOrigemColetaEnum.Reservatorio:
                        Reservatorio reservatorio = BDTService.ConsultarReservatorioPorChaves(idOrigemColeta).First();
                        AtualizarSubsistema(reservatorio);
                        origemColeta = reservatorio;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("tipoOrigemColeta");
                }

                ValidarSubsistemaPseudoUsinas(origemColeta);

                origemColetaRepository.Add(origemColeta, false);
            }

            return origemColeta;
        }

        private void AtualizarSubsistema(IConjuntoGerador origemColeta)
        {
            if (!string.IsNullOrWhiteSpace(origemColeta.IdSubsistema))
            {
                OrigemColeta subsistema = origemColetaRepository.FindByKey(origemColeta.IdSubsistema);
                if (subsistema == null)
                {
                    subsistema = BDTService.ConsultarTodosSubsistemas().First(s => s.Id == origemColeta.IdSubsistema);
                    origemColetaRepository.Add(subsistema);
                }
            }
        }

        public IList<OrigemColeta> ConsultarOuCriarOrigemColetaPorIds(IList<string> idsOrigemColeta,
            TipoOrigemColetaEnum tipoOrigemColeta)
        {
            IList<OrigemColeta> origensColeta = origemColetaRepository.ConsultarPorIds(idsOrigemColeta);

            IList<string> idsOrigemColetaCriar = idsOrigemColeta
                .Where(idOrigemColeta => origensColeta.All(origemColeta => origemColeta.Id != idOrigemColeta))
                .ToList();

            if (idsOrigemColetaCriar.Any())
            {
                IList<OrigemColeta> origensColetaOnline;

                switch (tipoOrigemColeta)
                {
                    case TipoOrigemColetaEnum.Usina:
                        origensColetaOnline = BDTService
                            .ConsultarUsinaPorChaves(idsOrigemColeta.ToArray())
                            .ToList<OrigemColeta>();
                        break;
                    case TipoOrigemColetaEnum.Subsistema:
                        origensColetaOnline = BDTService
                            .ConsultarTodosSubsistemas()
                            .Where(s => idsOrigemColeta.Contains(s.Id))
                            .ToList<OrigemColeta>();
                        break;
                    case TipoOrigemColetaEnum.UnidadeGeradora:
                        var unidadesGeradoras = BDTService
                            .ConsultarUnidadeGeradoraPorChaves(idsOrigemColetaCriar.ToArray());
                        if (unidadesGeradoras.Any())
                        {
                            Usina usina = (Usina)ObterOuCriarOrigemColetaPorId(unidadesGeradoras.First().Usina.Id, TipoOrigemColetaEnum.Usina);
                            foreach (UnidadeGeradora unidadesGeradora in unidadesGeradoras)
                            {
                                unidadesGeradora.Usina = usina;
                            }
                        }
                        origensColetaOnline = unidadesGeradoras.ToList<OrigemColeta>();
                        break;
                    case TipoOrigemColetaEnum.Reservatorio:
                        origensColetaOnline = BDTService
                            .ConsultarReservatorioPorChaves(idsOrigemColeta.ToArray())
                            .ToList<OrigemColeta>();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("tipoOrigemColeta");
                }

                ValidarSubsistemaPseudoUsinas(origensColetaOnline);

                origemColetaRepository.Add(origensColetaOnline);
                origensColetaOnline.ToList().ForEach(origensColeta.Add);

            }

            return origensColeta;
        }

        public IList<OrigemColeta> ConsultarOrigemColetaPorTipoNomeOnline(TipoOrigemColetaEnum tipoOrigemColeta, string nome)
        {
            IList<OrigemColeta> origensColeta = null;
            switch (tipoOrigemColeta)
            {
                case TipoOrigemColetaEnum.Usina:
                    origensColeta = BDTService
                        .ConsultarUsinaPorNomeExibicao(nome)
                        .ToList<OrigemColeta>();
                    break;
                case TipoOrigemColetaEnum.Reservatorio:
                    origensColeta = BDTService
                        .ConsultarReservatorioPorNomeExibicao(nome)
                        .ToList<OrigemColeta>();
                    break;
                case TipoOrigemColetaEnum.Subsistema:
                    origensColeta = BDTService
                        .ConsultarTodosSubsistemas()
                        .Where(s => s.Nome.ToLower().Contains(nome.ToLower()))
                        .ToList<OrigemColeta>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        string.Format("Integração BDT não possui consulta por Nome do tipo {0}", tipoOrigemColeta));
            }


            return origensColeta;
        }

        public IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorUsinaOnline(string idOrigemColeta)
        {
            return BDTService.ConsultarUnidadeGeradoraPorUsina(idOrigemColeta);
        }

        public IList<Usina> ConsultarUsinaParticipanteGabaritoPorColetaInsumo(int idColetaInsumo)
        {
            return origemColetaRepository.ConsultarUsinaParticipanteGabaritoPorColetaInsumo(idColetaInsumo);
        }

        public IList<UnidadeGeradora> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumoUsina(
            int idColetaInsumo, string idUsina)
        {
            return origemColetaRepository.ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumoUsina(
                idColetaInsumo, idUsina);
        }

        public IList<UnidadeGeradora> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumo(int idColetaInsumo)
        {
            return origemColetaRepository.ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumo(idColetaInsumo);
        }

        public IList<UnidadeGeradoraManutencaoSGIDTO> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumos(List<int> idColetaInsumos)
        {
            return origemColetaRepository.ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumos(idColetaInsumos);
        }

        #region Validações

        private void ValidarSubsistemaPseudoUsinas(IList<OrigemColeta> origensColeta)
        {
            foreach (var origemColeta in origensColeta)
            {
                ValidarSubsistemaPseudoUsinas(origemColeta);
            }
        }

        private void ValidarSubsistemaPseudoUsinas(OrigemColeta origemColeta)
        {
            if (origemColeta != null)
            {
                if (origemColeta is Usina)
                {
                    Usina usina = origemColeta as Usina;
                    if (string.IsNullOrWhiteSpace(usina.IdSubsistema))
                    {
                        throw new ONSBusinessException("Não é possível salvar o gabarito sem identificação do subsistema. Será necessário analisar o código do subsistema da usina.");
                    }
                }
                else if (origemColeta is Reservatorio)
                {
                    Reservatorio reservatorio = origemColeta as Reservatorio;
                    if (string.IsNullOrWhiteSpace(reservatorio.IdSubsistema))
                    {
                        throw new ONSBusinessException("Não é possível salvar o gabarito sem identificação do subsistema. Será necessário analisar o código do subsistema do reservatório.");
                    }
                }
            }
        }

        #endregion

        #region Sincronização de origens coleta com BDT

        public void SincronizarOrigensColetaComBDT()
        {
            var origensPorTipoGroup = origemColetaRepository.All()
                .GroupBy(origem => origem.TipoOrigemColeta);

            foreach (var origensPorTipo in origensPorTipoGroup)
            {
                var idsOrigem = origensPorTipo.Select(origem => origem.Id).ToArray();
                var origensBdt = ConsultarOrigemColetaPorTipoChavesOnline(origensPorTipo.Key, idsOrigem);
                var origensSgipmo = origensPorTipo.ToList();

                switch (origensPorTipo.Key)
                {
                    case TipoOrigemColetaEnum.Usina:
                        MergeOrigemColetaComBDT<Usina, object>(origensSgipmo, origensBdt,
                            usina => usina.Nome,
                            usina => usina.NomeCurto,
                            usina => usina.NomeLongo);
                        break;
                    case TipoOrigemColetaEnum.Subsistema:
                        MergeOrigemColetaComBDT<Subsistema, object>(origensSgipmo, origensBdt, subsistema => subsistema.Nome);
                        break;
                    case TipoOrigemColetaEnum.UnidadeGeradora:
                        MergeOrigemColetaComBDT<UnidadeGeradora, object>(origensSgipmo, origensBdt,
                            unidadeGeradora => unidadeGeradora.Nome,
                            unidadeGeradora => unidadeGeradora.NumeroConjunto,
                            unidadeGeradora => unidadeGeradora.NumeroMaquina,
                            unidadeGeradora => unidadeGeradora.PotenciaNominal);
                        break;
                    case TipoOrigemColetaEnum.Reservatorio:
                        MergeOrigemColetaComBDT<Reservatorio, object>(origensSgipmo, origensBdt,
                            reservatorio => reservatorio.Nome,
                            reservatorio => reservatorio.NomeCurto,
                            reservatorio => reservatorio.NomeLongo);
                        break;
                }
            }
        }

        private void MergeOrigemColetaComBDT<TClass, TProperty>(
            IList<OrigemColeta> origensSgipmo, IList<OrigemColeta> origensBdt,
            params Expression<Func<TClass, TProperty>>[] expressions)
        {
            foreach (var origemBdt in origensBdt)
            {
                var origem = origensSgipmo.FirstOrDefault(o => o.Id == origemBdt.Id);
                if (origem != null)
                {
                    foreach (var expression in expressions)
                    {
                        MemberExpression memberExpression = expression.Body as MemberExpression;

                        if (memberExpression == null)
                        {
                            UnaryExpression unaryExpression = expression.Body as UnaryExpression;
                            if (unaryExpression != null)
                            {
                                memberExpression = (MemberExpression)unaryExpression.Operand;
                            }
                        }

                        if (memberExpression != null)
                        {
                            var propertyName = memberExpression.Member.Name;
                            PropertyInfo propertyInfo = typeof(TClass).GetProperty(propertyName);
                            var valorNovo = propertyInfo.GetValue(origemBdt, null);
                            var valorAntigo = propertyInfo.GetValue(origem, null);

                            if (!valorAntigo.Equals(valorNovo))
                            {
                                propertyInfo.SetValue(origem, valorNovo, null);
                            }
                        }
                    }
                }
            }
        }

        private IList<OrigemColeta> ConsultarOrigemColetaPorTipoChavesOnline(
            TipoOrigemColetaEnum tipoOrigemColeta, params string[] ids)
        {
            switch (tipoOrigemColeta)
            {
                case TipoOrigemColetaEnum.Usina:
                    //return BDTService.ConsultarUsinaPorChaves(ids).Cast<OrigemColeta>().ToList();
                    return BDTService.ConsultarUsinaPorNomeExibicao().Cast<OrigemColeta>().ToList();
                case TipoOrigemColetaEnum.Subsistema:
                    //return BDTService.ConsultarTodosSubsistemas().Where(sub => ids.Contains(sub.Id)).Cast<OrigemColeta>().ToList();
                    return BDTService.ConsultarTodosSubsistemas().Cast<OrigemColeta>().ToList();
                case TipoOrigemColetaEnum.UnidadeGeradora:
                    //return BDTService.ConsultarUnidadeGeradoraPorChaves(ids).Cast<OrigemColeta>().ToList();
                    return BDTService.ConsultarUnidadeGeradoraPorChaves(ids).Cast<OrigemColeta>().ToList();
                case TipoOrigemColetaEnum.Reservatorio:
                    //return BDTService.ConsultarReservatorioPorChaves(ids).Cast<OrigemColeta>().ToList();
                    return BDTService.ConsultarReservatorioPorNomeExibicao().Cast<OrigemColeta>().ToList();
                default:
                    return new List<OrigemColeta>();
            }
        }

        #endregion

    }
}
