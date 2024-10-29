using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class SemanaOperativaService : ISemanaOperativaService
    {
        private readonly ISemanaOperativaRepository semanaOperativaRepository;
        private readonly IParametroService parametroService;
        private readonly ISituacaoSemanaOperativaRepository situacaoSemanaOperativaRepository;
        private readonly ISituacaoColetaInsumoRepository situacaoColetaInsumoRepository;
        private readonly IGabaritoRepository gabaritoRepository;
        private readonly IColetaInsumoRepository coletaInsumoRepository;
        private readonly IPMORepository pmoRepository;
        private readonly INotificacaoService notificacaoService;
        private readonly IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository;
        private readonly IColetaInsumoService coletaInsumoService;
        private readonly IArquivoRepository arquivoRepository;
        private readonly ISharePointService sharePointService;
        private readonly IHistoricoService historicoService;
        private readonly IArquivoSemanaOperativaRepository arquivoSemanaOperativaRepository;

        public SemanaOperativaService(
            ISemanaOperativaRepository semanaOperativaRepository,
            IParametroService parametroService,
            ISituacaoSemanaOperativaRepository situacaoSemanaOperativaRepository,
            ISituacaoColetaInsumoRepository situacaoColetaInsumoRepository,
            IGabaritoRepository gabaritoRepository,
            IColetaInsumoRepository coletaInsumoRepository,
            IPMORepository pmoRepository,
            INotificacaoService notificacaoService,
            IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository,
            IColetaInsumoService coletaInsumoService,
            IArquivoRepository arquivoRepository,
            ISharePointService sharePointService,
            IHistoricoService historicoService,
            IArquivoSemanaOperativaRepository arquivoSemanaOperativaRepository)
        {
            this.semanaOperativaRepository = semanaOperativaRepository;
            this.parametroService = parametroService;
            this.situacaoSemanaOperativaRepository = situacaoSemanaOperativaRepository;
            this.situacaoColetaInsumoRepository = situacaoColetaInsumoRepository;
            this.gabaritoRepository = gabaritoRepository;
            this.coletaInsumoRepository = coletaInsumoRepository;
            this.pmoRepository = pmoRepository;
            this.notificacaoService = notificacaoService;
            this.dadoColetaNaoEstruturadoRepository = dadoColetaNaoEstruturadoRepository;
            this.coletaInsumoService = coletaInsumoService;
            this.arquivoRepository = arquivoRepository;
            this.sharePointService = sharePointService;
            this.historicoService = historicoService;
            this.arquivoSemanaOperativaRepository = arquivoSemanaOperativaRepository;
        }

        public Task AbrirEstudoAsync(AberturaEstudoDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task AlterarSemanaOperativaAsync(DadosAlteracaoSemanaOperativaDTO dadosAlteracao)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarSemanasOperativasInclusaoAsync(IEnumerable<SemanaOperativa> semanasOperativas, int ano, string nomeMes)
        {
            throw new NotImplementedException();
        }

        public Task<ArquivosSemanaOperativaDTO> ConsultarArquivosSemanaOperativaConvergenciaCceeAsync(ArquivosSemanaOperativaFilter filtro)
        {
            throw new NotImplementedException();
        }

        public Task<ArquivosSemanaOperativaConvergirPldDTO> ConsultarArquivosSemanaOperativaConvergenciaPLDAsync(ArquivosSemanaOperativaFilter filtro)
        {
            throw new NotImplementedException();
        }

        public Task<ArquivosSemanaOperativaDTO> ConsultarArquivosSemanaOperativaPublicacaoResultadosAsync(ArquivosSemanaOperativaFilter filtro)
        {
            throw new NotImplementedException();
        }

        public Task<IList<SemanaOperativa>> ConsultarEstudoConvergenciaPldPorNomeAsync(string nomeEstudo)
        {
            throw new NotImplementedException();
        }

        public Task<IList<SemanaOperativa>> ConsultarEstudoPorNomeAsync(string nomeEstudo)
        {
            throw new NotImplementedException();
        }

        public Task<IList<SemanaOperativa>> ConsultarSemanasOperativasComGabaritoAsync()
        {
            throw new NotImplementedException();
        }

        public Task ConvergirPLDAsync(ConvergirPLDDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task ExcluirSemanaAsync(SemanaOperativa semanaOperativa)
        {
            throw new NotImplementedException();
        }

        public ValueTask<SemanaOperativa> GerarSemanaOperativaAsync(int ano, string nomeMes, DateTime dataInicioSemana, DateTime dataFimPMO, int revisao)
        {
            throw new NotImplementedException();
        }

        public Task<ISet<SemanaOperativa>> GerarSugestaoSemanasOperativasAsync(int ano, int mes)
        {
            throw new NotImplementedException();
        }

        public Task IniciarConvergenciaCCEEAsync(InicializacaoConvergenciaCceeDTO dto)
        {
            throw new NotImplementedException();
        }

        public ValueTask<SemanaOperativa> ObterSemanaOperativaPorChaveAsync(int chave)
        {
            throw new NotImplementedException();
        }

        public ValueTask<SemanaOperativa> ObterSemanaOperativaPorChaveParaInformarDadosAsync(int chave)
        {
            throw new NotImplementedException();
        }

        public Task<SemanaOperativa> ObterSemanaOperativaValidaParaAbrirEstudoAsync(DadosSemanaOperativaDTO dto)
        {
            throw new NotImplementedException();
        }

        public ValueTask<SemanaOperativa> ObterSemanaOperativaValidaParaResetarGabaritoAsync(int idSemanaOperativa)
        {
            throw new NotImplementedException();
        }

        public Task PublicarResultadosAsync(PublicacaoResultadosDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task ReprocessarPMOAsync(ReprocessamentoPMODTO dto)
        {
            throw new NotImplementedException();
        }

        public Task ResetarGabaritoAsync(ResetGabaritoDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
