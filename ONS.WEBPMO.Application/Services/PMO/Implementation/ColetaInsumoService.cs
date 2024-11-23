using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.BDT;
using ONS.WEBPMO.Domain.Repository.PMO;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class ColetaInsumoService : IColetaInsumoService
    {
        private readonly IColetaInsumoRepository coletaInsumoRepository;
        private readonly ISemanaOperativaRepository semanaOperativaRepository;
        private readonly IParametroService parametroService;
        private readonly ISituacaoColetaInsumoRepository situacaoColetaInsumoRepository;
        private readonly IDadoColetaEstruturadoRepository dadoColetaEstruturadoRepository;
        private readonly IAgenteService agenteService;
        private readonly IDadoColetaManutencaoService dadoColetaManutencaoService;
        private readonly IGabaritoRepository gabaritoRepository;
        private readonly ITipoLimiteRepository tipoLimiteRepository;
        private readonly ITipoPatamarRepository tipoPatamarRepository;
        private readonly IGrandezaRepository grandezaRepository;
        private readonly INotificacaoService notificacaoService;
        private readonly ISituacaoSemanaOperativaRepository situacaoSemanaOperativaRepository;
        private readonly IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository;
        private readonly IArquivoRepository arquivoRepository;
        private readonly IHistoricoService historicoService;
        private readonly IArquivoSemanaOperativaRepository arquivoSemanaOperativaRepository;
        private readonly ILogNotificacaoService logNotificacaoService;
        private readonly IInstanteVolumeReservatorioRepository instanteVolumeReservatorioRepository;
        private readonly IDadoColetaManutencaoRepository dadoColetaManutencaoRepository;

        public ColetaInsumoService(
            IColetaInsumoRepository coletaInsumoRepository,
            ISemanaOperativaRepository semanaOperativaRepository,
            IParametroService parametroService,
            ISituacaoColetaInsumoRepository situacaoColetaInsumoRepository,
            IAgenteService agenteService,
            IGabaritoRepository gabaritoRepository,
            ITipoLimiteRepository tipoLimiteRepository,
            ITipoPatamarRepository tipoPatamarRepository,
            IGrandezaRepository grandezaRepository,
            IDadoColetaManutencaoService dadoColetaManutencaoService,
            IDadoColetaEstruturadoRepository dadoColetaEstruturadoRepository,
            INotificacaoService notificacaoService,
            ISituacaoSemanaOperativaRepository situacaoSemanaOperativaRepository,
            IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository,
            IArquivoRepository arquivoRepository,
            IHistoricoService historicoService,
            IArquivoSemanaOperativaRepository arquivoSemanaOperativaRepository,
            ILogNotificacaoService logNotificacaoService,
            IInstanteVolumeReservatorioRepository instanteVolumeReservatorioRepository,
            IDadoColetaManutencaoRepository _dadoColetaManutencaoRepository)
        {
            this.coletaInsumoRepository = coletaInsumoRepository;
            this.semanaOperativaRepository = semanaOperativaRepository;
            this.parametroService = parametroService;
            this.situacaoColetaInsumoRepository = situacaoColetaInsumoRepository;
            this.agenteService = agenteService;
            this.gabaritoRepository = gabaritoRepository;
            this.tipoLimiteRepository = tipoLimiteRepository;
            this.tipoPatamarRepository = tipoPatamarRepository;
            this.grandezaRepository = grandezaRepository;
            this.dadoColetaManutencaoService = dadoColetaManutencaoService;
            this.dadoColetaEstruturadoRepository = dadoColetaEstruturadoRepository;
            this.notificacaoService = notificacaoService;
            this.situacaoSemanaOperativaRepository = situacaoSemanaOperativaRepository;
            this.dadoColetaNaoEstruturadoRepository = dadoColetaNaoEstruturadoRepository;
            this.arquivoRepository = arquivoRepository;
            this.historicoService = historicoService;
            this.arquivoSemanaOperativaRepository = arquivoSemanaOperativaRepository;
            this.logNotificacaoService = logNotificacaoService;
            this.instanteVolumeReservatorioRepository = instanteVolumeReservatorioRepository;
            dadoColetaManutencaoRepository = _dadoColetaManutencaoRepository;
        }

        public void AbrirColeta(DadosSemanaOperativaDTO dadosSemanaOperativaDto)
        {
            throw new NotImplementedException();
        }

        public void AlterarDadoColetaManutencao(AlteracaoDadoColetaManutencaoDTO dto)
        {
            throw new NotImplementedException();
        }

        public void AprovarColetaDadosEstruturados(DadoColetaInsumoDTO dadoColetaInsumoDto, IList<ValorDadoColetaDTO> valoresDto)
        {
            throw new NotImplementedException();
        }

        public void AprovarColetaDadosEstruturadosEmLote(DadoColetaInsumoDTO dadoColetaInsumoDto, IList<ValorDadoColetaDTO> valoresDto)
        {
            throw new NotImplementedException();
        }

        public void AprovarColetaDadosManutencao(ColetaInsumoManutencaoFilter filter)
        {
            throw new NotImplementedException();
        }

        public void AprovarColetaDadosNaoEstruturados(DadosMonitoramentoColetaInsumoDTO dtoColeta, DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dtoDados)
        {
            throw new NotImplementedException();
        }

        public void CapturarColetaDados(DadosMonitoramentoColetaInsumoDTO dto)
        {
            throw new NotImplementedException();
        }

        public string ChecarSeVolumeInicialIgualAoDaSemanaAnterior(IList<ValorDadoColetaDTO> valorDadoColetaDTOs, int idColetaInsumo)
        {
            throw new NotImplementedException();
        }

        public ICollection<ColetaInsumo> ConsultarColetasInsumoParaInformarDadosPaginado(PesquisaMonitorarColetaInsumoFilter filter)
        {
            throw new NotImplementedException();
        }

        public ICollection<ColetaInsumo> ConsultarColetasInsumoParaMonitorarDadosPaginado(PesquisaMonitorarColetaInsumoFilter filter)
        {
            throw new NotImplementedException();
        }

        public ICollection<DadoColetaManutencaoDTO> ConsultarDadoColetaManutencaoPorColetaInsumoPaginado(DadoColetaInsumoFilter filter)
        {
            throw new NotImplementedException();
        }

        public IList<DadoColetaBloco> ConsultarDadosColetaParaGeracaoBloco(int idSemanaOperativa)
        {
            throw new NotImplementedException();
        }

        public void DeletarArquivos(DadosSemanaOperativaDTO dadosSemanaOperativaDto)
        {
            throw new NotImplementedException();
        }

        public void EnviarColetaDadosEstruturados(IList<ValorDadoColetaDTO> dtos, int idColetaInsumo, string versao)
        {
            throw new NotImplementedException();
        }

        public void EnviarDadosColetaInsumo(EnviarDadosColetaInsumoFilter filter)
        {
            throw new NotImplementedException();
        }

        public void EnviarDadosColetaInsumoManutencao(EnviarDadosColetaInsumoManutencaoFilter filter)
        {
            throw new NotImplementedException();
        }

        public void ExcluirDadoColetaManutencao(ExclusaoDadoColetaManutencaoDTO dto, int idColetaInsumo)
        {
            throw new NotImplementedException();
        }

        public void FecharColeta(DadosSemanaOperativaDTO dadosSemanaOperativaDto)
        {
            throw new NotImplementedException();
        }

        public void IncluirDadoColetaManutencao(InclusaoDadoColetaManutencaoDTO dto)
        {
            throw new NotImplementedException();
        }

        public void IncluirDadoColetaManutencaoImportacao(IList<InclusaoDadoColetaManutencaoDTO> dtos)
        {
            throw new NotImplementedException();
        }

        public Parametro MensagemAberturaColetaEditavel(DadosSemanaOperativaDTO dadosSemanaOperativaDto)
        {
            throw new NotImplementedException();
        }

        public ISet<Arquivo> ObterArquivosUpload(ISet<ArquivoDadoNaoEstruturadoDTO> arquivos, bool desconsiderarJaGravadosBancoDados = false)
        {
            throw new NotImplementedException();
        }

        public ColetaInsumo ObterColetaInsumoInformarDadosPorChave(int idColetaInsumo)
        {
            throw new NotImplementedException();
        }

        public DadoColetaNaoEstruturadoDTO ObterDadoColetaNaoEstruturado(DadoColetaInsumoNaoEstruturadoFilter filtro)
        {
            throw new NotImplementedException();
        }

        public DadosInformarColetaInsumoDTO ObterDadosParaInformarDadosPorChaveColetaInsumo(ColetaInsumoFilter filter)
        {
            throw new NotImplementedException();
        }

        public ColetaInsumo ObterPorChave(int chave)
        {
            throw new NotImplementedException();
        }

        public ColetaInsumo ObterValidarColetaInsumoInformarDadosPorChave(int idColetaInsumo, byte[] versaoColetaInsumo = null, bool atualizaParaAndamento = false)
        {
            throw new NotImplementedException();
        }

        public ColetaInsumo ObterValidarColetaInsumoMonitorarDadosPorChave(int idColetaInsumo, int idSituacaoColeta)
        {
            throw new NotImplementedException();
        }

        public void RejeitarColetaDadosEstruturados(DadoColetaInsumoDTO dadoColetaInsumoDto)
        {
            throw new NotImplementedException();
        }

        public void RejeitarColetaDadosManutencao(ColetaInsumoManutencaoFilter filter)
        {
            throw new NotImplementedException();
        }

        public void RejeitarColetaDadosNaoEstruturados(DadosMonitoramentoColetaInsumoDTO dtoColeta, DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dtoDados)
        {
            throw new NotImplementedException();
        }

        public void SalvarColetaDadosEstruturados(IList<ValorDadoColetaDTO> dtos, DadoColetaInsumoDTO dto)
        {
            throw new NotImplementedException();
        }

        public void SalvarColetaDadosManutencao(ColetaInsumoManutencaoFilter filter)
        {
            throw new NotImplementedException();
        }

        public void SalvarDadoColetaNaoEstruturada(DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dto, DadosMonitoramentoColetaInsumoDTO dtoDadosAnalise, ColetaInsumo coletaInusmo = null)
        {
            throw new NotImplementedException();
        }

        public bool situacaoBoolSemanaOperativa(DadosSemanaOperativaDTO dadosSemanaOperativaDto)
        {
            throw new NotImplementedException();
        }

        public bool VerificarPermissaoIncluirManutencao()
        {
            throw new NotImplementedException();
        }

        public bool VerificarSeDadosInsumoIguaisColetaAnterior(ColetaInsumo coletaInsumo)
        {
            throw new NotImplementedException();
        }
    }
}
