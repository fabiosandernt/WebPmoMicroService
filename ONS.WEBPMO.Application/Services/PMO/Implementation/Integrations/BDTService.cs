using AutoMapper;
using ons.common.schemas.filters;
using ONS.Common.Instituicao.Services;
using ONS.Common.Services.Impl;
using ONS.WEBPMO.Application.Services.PMO.Interfaces.Integrations;
using ONS.WEBPMO.Domain.Entities.BDT;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;
using ONS.WEBPMO.Domain.Repository.BDT;
using MatchType = ons.common.schemas.filters.MatchType;


namespace ONS.WEBPMO.Application.Services.PMO.Implementation.Integrations
{   
    public class BDTService :  IBDTService
    {
        private readonly IUsinaPMORepository usinaPmoRepository;
        private readonly IUsinaPEMRepository usinaPemRepository;
        private readonly IReservatorioPMORepository reservatorioPmoRepository;
        private readonly IReservatorioEERepository reservatorioEERepository;
        private readonly IUnidadeGeradoraPMORepository unidadeGeradoraPmoRepository;
        private readonly ISubsistemaPMORepository subsistemaPmoRepository;
        private readonly ISubmercadoPMORepository submercadoPmoRepository;
        private readonly IMapper _mapper;

        public BDTService(
            IUsinaPMORepository usinaPmoRepository,
            IUsinaPEMRepository usinaPemRepository,
            IReservatorioPMORepository reservatorioPmoRepository,
            IReservatorioEERepository reservatorioEERepository,
            IUnidadeGeradoraPMORepository unidadeGeradoraPmoRepository,
            ISubsistemaPMORepository subsistemaPmoRepository,
            ISubmercadoPMORepository submercadoPmoRepository, IMapper mapper)
        {
            this.usinaPmoRepository = usinaPmoRepository;
            this.usinaPemRepository = usinaPemRepository;
            this.reservatorioPmoRepository = reservatorioPmoRepository;
            this.reservatorioEERepository = reservatorioEERepository;
            this.unidadeGeradoraPmoRepository = unidadeGeradoraPmoRepository;
            this.subsistemaPmoRepository = subsistemaPmoRepository;
            this.submercadoPmoRepository = submercadoPmoRepository;
            _mapper = mapper;
        }

        #region Reservatório

        public IList<Reservatorio> ConsultarReservatorioPorNomeExibicao(string nomeExibicaoContem = "")
        {
            IList<ReservatorioPMO> reservatoriosPmo = reservatorioPmoRepository
                .ConsultarPorNomeExibicao(nomeExibicaoContem)
                .OrderBy(r => r.NomeExibicao)
                .ToList();

            return ConvertToOrigemColeta<ReservatorioPMO, Reservatorio>(reservatoriosPmo);
        }

        public IList<Reservatorio> ConsultarReservatorioPorChaves(params string[] chaves)
        {
            IList<ReservatorioPMO> reservatoriosPmo = reservatorioPmoRepository.ConsultarPorChaves(chaves);
            return ConvertToOrigemColeta<ReservatorioPMO, Reservatorio>(reservatoriosPmo);
        }

        #endregion
        public IList<ReservatorioEE> ConsultarReservatoriosEquivalentesDeEnergiaAtivos()
        {
            IList<ReservatorioEE> reservatoriosEE = reservatorioEERepository.ConsultarReservatoriosEquivalentesDeEnergiaAtivos();

            return reservatoriosEE;
        }

        #region Reservatório

        #endregion

        #region Usina
        public IList<Usina> ConsultarUsinaPorNomeExibicao(string nomeExibicaoContem = "")
        {
            IList<UsinaPMO> usinasPmo = usinaPmoRepository
                .ConsultarPorNomeExibicao(nomeExibicaoContem)
                .OrderBy(r => r.NomeExibicao)
                .ToList();

            return ConvertToOrigemColeta<UsinaPMO, Usina>(usinasPmo);
        }

        public IList<Usina> ConsultarUsinaPorChaves(params string[] chaves)
        {
            IList<UsinaPMO> usinasPmo = usinaPmoRepository.ConsultarPorChaves(chaves);
            return ConvertToOrigemColeta<UsinaPMO, Usina>(usinasPmo);
        }

        public IList<Usina> ConsultarUsinas()
        {
            IList<UsinaPMO> usinasPmo = usinaPmoRepository.Consultar();
            return ConvertToOrigemColeta<UsinaPMO, Usina>(usinasPmo);
        }

        public IList<UsinaPEM> ConsultarDadosUsinasVisaoPEM()
        {
            IList<UsinaPEM> usinasPem = usinaPemRepository.ConsultarDadosUsinasVisaoPEM();

            return usinasPem;
        }

        #endregion

        #region Unidade Geradora
        public IList<UnidadeGeradora> ConsultarUnidadesGeradoras()
        {
            IList<UnidadeGeradoraPMO> unidadesGeradorasPmo = unidadeGeradoraPmoRepository.Consultar();
            return ConvertToOrigemColeta<UnidadeGeradoraPMO, UnidadeGeradora>(unidadesGeradorasPmo);
        }

        public IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorChaves(params string[] chaves)
        {
            IList<UnidadeGeradoraPMO> unidadesGeradorasPmo = unidadeGeradoraPmoRepository.ConsultarPorChaves(chaves);
            return ConvertToOrigemColeta<UnidadeGeradoraPMO, UnidadeGeradora>(unidadesGeradorasPmo);
        }

        public IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorUsina(string chaveUsina)
        {
            IList<UnidadeGeradoraPMO> unidadesGeradorasPmo = unidadeGeradoraPmoRepository.ConsultarPorUsina(chaveUsina);
            return ConvertToOrigemColeta<UnidadeGeradoraPMO, UnidadeGeradora>(unidadesGeradorasPmo);
        }
        #endregion

        #region Subsistema
        public IList<Subsistema> ConsultarTodosSubsistemas()
        {
            IList<SubsistemaPMO> subsistemasPmo = subsistemaPmoRepository
                .ConsultarTodos().Union(subsistemaPmoRepository.ConsultarOutros())
                .OrderBy(subsistema => subsistema.NomeCurto)
                .ToList();

            return ConvertToOrigemColeta<SubsistemaPMO, Subsistema>(subsistemasPmo);
        }

        public IList<Subsistema> ConsultarSubsistemasAtivos()
        {
            IList<SubsistemaPMO> subsistemasPmo = subsistemaPmoRepository
                .ConsultarAtivos().Union(subsistemaPmoRepository.ConsultarOutros())
                .OrderBy(subsistema => subsistema.NomeCurto)
                .ToList();

            return ConvertToOrigemColeta<SubsistemaPMO, Subsistema>(subsistemasPmo);
        }
        #endregion

        #region Agentes

        public IList<Agente> ConsultarAgentesPorNome(string nomeParcial, int top = int.MaxValue)
        {
            FiltroInstituicao filtro = new FiltroInstituicao
            {
                nomeParcial = new FilterField<string>(nomeParcial, MatchType.Equals),
                top = 20
            };

            return ConsultarAgentes(filtro);
        }
        public IList<Agente> ConsultarAgentesPorChaves(params int[] chaves)
        {
            FiltroInstituicao filtro = new FiltroInstituicao
            {
                idAgente = chaves.Cast<int?>().ToList(),
                top = -1
            };
            return ConsultarAgentes(filtro);
        }

        private IList<Agente> ConsultarAgentes(FiltroInstituicao filtro)
        {
            IList<InstituicaoDTO> instituicoes = InstituicaoServiceHelper.ConsultarInstituicoes(filtro);
            return instituicoes.Select(instituicaoDto => new Agente
            {
                Id = instituicaoDto.idAgente,
                Nome = instituicaoDto.siglaAgente,
                NomeLongo = string.IsNullOrWhiteSpace(instituicaoDto.razaoSocial)
                    ? instituicaoDto.siglaAgente
                    : instituicaoDto.razaoSocial
            })
            .OrderBy(a => a.Nome)
            .ToList();
        }

        #endregion

        #region Submercado
        public IList<SubmercadoPMO> ConsultarSubmercados()
        {
            IList<SubmercadoPMO> SubmercadosPmo = submercadoPmoRepository
                .ConsultarTodos()
                .OrderBy(Submercado => Submercado.NomeCurto)
                .ToList();

            return SubmercadosPmo;
        }

        #endregion

        private IList<TDestination> ConvertToOrigemColeta<TSource, TDestination>(IEnumerable<TSource> origensColetaPmo)
            where TDestination : OrigemColeta
        {
            return origensColetaPmo.Select(origemColetaPmo => _mapper.Map<TDestination>(origemColetaPmo)).ToList();
        }
    }
}
