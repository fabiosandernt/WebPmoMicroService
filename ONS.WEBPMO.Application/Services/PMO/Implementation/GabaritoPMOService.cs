using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.PMO;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class GabaritoPMOService : IGabaritoPMOService
    {
        private readonly IGabaritoRepository gabaritoRepository;
        private readonly ISemanaOperativaRepository semanaOperativaRepository;
        private readonly IInsumoRepository insumoRepository;
        private readonly IColetaInsumoRepository coletaInsumoRepository;
        private readonly ISituacaoColetaInsumoRepository situacaoColetaInsumoRepository;
        private readonly IDadoColetaEstruturadoRepository dadoColetaEstruturadoRepository;
        private readonly IDadoColetaManutencaoRepository dadoColetaManutencaoRepository;
        private readonly IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository;
        private readonly IDadoColetaRepository dadoColetaRepository;
        private readonly IArquivoRepository arquivoRepository;
        private readonly IAgenteService agenteService;
        private readonly IOrigemColetaService origemColetaService;
        private readonly IHistoricoService historicoService;

        public GabaritoPMOService(
            IGabaritoRepository gabaritoRepository,
            ISemanaOperativaRepository semanaOperativaRepository,
            IInsumoRepository insumoRepository,
            IColetaInsumoRepository coletaInsumoRepository,
            ISituacaoColetaInsumoRepository situacaoColetaInsumoRepository,
            IAgenteService agenteService,
            IOrigemColetaService origemColetaService,
            IDadoColetaEstruturadoRepository dadoColetaEstruturadoRepository,
            IDadoColetaManutencaoRepository dadoColetaManutencaoRepository,
            IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository,
            IDadoColetaRepository dadoColetaRepository,
            IArquivoRepository arquivoRepository,
            IHistoricoService historicoService)
        {
            this.gabaritoRepository = gabaritoRepository;
            this.semanaOperativaRepository = semanaOperativaRepository;
            this.insumoRepository = insumoRepository;
            this.coletaInsumoRepository = coletaInsumoRepository;
            this.situacaoColetaInsumoRepository = situacaoColetaInsumoRepository;
            this.agenteService = agenteService;
            this.origemColetaService = origemColetaService;
            this.dadoColetaEstruturadoRepository = dadoColetaEstruturadoRepository;
            this.dadoColetaManutencaoRepository = dadoColetaManutencaoRepository;
            this.dadoColetaNaoEstruturadoRepository = dadoColetaNaoEstruturadoRepository;
            this.dadoColetaRepository = dadoColetaRepository;
            this.arquivoRepository = arquivoRepository;
            this.historicoService = historicoService;
        }

        public void AlterarGabarito(GabaritoConfiguracaoDTO dto)
        {
            throw new NotImplementedException();
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoGeracaoComplementar>> ConsultarAgentesComGeracaoComplementar(bool isPadrao, string nomeRevisao = "")
        {
            throw new NotImplementedException();
        }

        public IList<GabaritoParticipantesBaseDTO<AgenteParticipanteGabaritoDTO>> ConsultarAgentesParticipantesGabarito(bool isPadrao, string nomeRevisao = "")
        {
            throw new NotImplementedException();
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoInsumoNaoEstruturadoDTO>> ConsultarAgentesPodemEnviarDadosNaoEstruturadosPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            throw new NotImplementedException();
        }

        public ICollection<GabaritoAgrupadoAgenteOrigemColetaDTO> ConsultarGabaritosAgrupadoPorAgenteTipoOrigemPaginado(GabaritoOrigemColetaFilter filter)
        {
            throw new NotImplementedException();
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoReservatorioDTO>> ConsultarReservatorioPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            throw new NotImplementedException();
        }

        public IList<GabaritoParticipantesBaseDTO<ReservatorioParticipanteGabaritoDTO>> ConsultarReservatoriosParticipantesGabarito(bool isPadrao, string nomeRevisao = "")
        {
            throw new NotImplementedException();
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoSubsistemaDTO>> ConsultarSubsistemaPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            throw new NotImplementedException();
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoUGEDTO>> ConsultarUGEPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            throw new NotImplementedException();
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoUsinaDTO>> ConsultarUsinaPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            throw new NotImplementedException();
        }

        public IList<GabaritoParticipantesBaseDTO<UsinaParticipanteGabaritoDTO>> ConsultarUsinasParticipantesGabarito(bool isPadrao, string nomeRevisao = "")
        {
            throw new NotImplementedException();
        }

        public Gabarito ObterPorChave(int chave)
        {
            throw new NotImplementedException();
        }

        public Gabarito ObterPorColetaInsumoNaoEstruturado(GabaritoDadoColetaNaoEstruturadoFilter filtro)
        {
            throw new NotImplementedException();
        }

        public void SalvarGabarito(GabaritoConfiguracaoDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
