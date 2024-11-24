using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Enumerations;
using ONS.WEBPMO.Domain.Repository.PMO;
using System.Globalization;

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

        public SemanaOperativa GerarSemanaOperativa(int ano, string nomeMes, DateTime dataInicioSemana, DateTime dataFimPMO, int revisao)
        {
            SemanaOperativa semanaOperativa = new SemanaOperativa
            {
                DataInicioSemana = dataInicioSemana,
                DataFimSemana = dataInicioSemana.AddDays(6),
                DataReuniao = ObterDataReuniao(dataInicioSemana, revisao),
                Revisao = revisao,
                DataInicioManutencao = dataInicioSemana,
                DataFimManutencao = dataFimPMO,
                Nome = ObterNomeSemanaOperativa(ano, nomeMes, revisao)
            };

            return semanaOperativa;
        }


        private string ObterNomeSemanaOperativa(int ano, string nomeMes, int revisao)
        {
            string nomeSemanaOperativa = revisao == 0 ?
                string.Format("PMO {0} {1}", nomeMes, ano) :
                string.Format("PMO {0} {1} - Revisão {2}", nomeMes, ano, revisao);
            return nomeSemanaOperativa;
        }
        private DateTime ObterDataReuniao(DateTime dataInicioSemana, int revisao)
        {
            Parametro parametro;
            int valorDiaReuniao = 0;
            if (revisao == 0)
            {
                parametro = parametroService.ObterParametro(ParametroEnum.DiaReuniaoPMO);
                if (parametro != null)
                {
                    valorDiaReuniao = int.Parse(parametro.Valor);
                }
            }
            else
            {
                parametro = parametroService.ObterParametro(ParametroEnum.DiaReuniaoRevisao);
                if (parametro != null)
                {
                    valorDiaReuniao = int.Parse(parametro.Valor);
                }
            }
            int valorDeDiasParaRetroceder = valorDiaReuniao == (int)DayOfWeek.Saturday
                ? -7
                : valorDiaReuniao - (int)DayOfWeek.Saturday;
            return dataInicioSemana.AddDays(valorDeDiasParaRetroceder);
        }




        public ISet<SemanaOperativa> GerarSugestaoSemanasOperativas(int ano, int mes)
        {
            ISet<SemanaOperativa> semanasOperativas = new SortedSet<SemanaOperativa>();

            var cultura = CultureInfo.CurrentCulture;
            string nomeMes = cultura.TextInfo.ToTitleCase(cultura.DateTimeFormat.GetMonthName(mes));

            DateTime dataInicioSemana = new DateTime(ano, mes, 1);
            DateTime ultimoDiaMes = dataInicioSemana.AddMonths(1).AddDays(-1);
            DateTime dataFimPMO = ultimoDiaMes;

            if (dataInicioSemana.DayOfWeek != DayOfWeek.Saturday)
            {
                // A semana operativa desve ser sempre de Sábado a Sexta
                // Se o primeiro dia do mês não for um Sábado é preciso obter a quantidade de dias 
                // que se deve retroceder para chegar ao Sábado
                int qtdDiasParaSabado = -(int)dataInicioSemana.DayOfWeek - 1;
                dataInicioSemana = dataInicioSemana.AddDays(qtdDiasParaSabado);
            }

            if (ultimoDiaMes.DayOfWeek != DayOfWeek.Friday)
            {
                // A semana operativa desve ser sempre de Sábado a Sexta
                // Se último dia do mês não for uma Sexta é preciso obter a quantidade de dias 
                // para chegar à Sexta
                int qtdDiasParaSexta = ultimoDiaMes.DayOfWeek == DayOfWeek.Saturday ?
                    6 : (int)DayOfWeek.Friday - (int)ultimoDiaMes.DayOfWeek;
                dataFimPMO = ultimoDiaMes.AddDays(qtdDiasParaSexta);
            }

            int revisao = 0;
            while (dataInicioSemana <= ultimoDiaMes)
            {
                SemanaOperativa semanaOperativa = GerarSemanaOperativa(ano, nomeMes, dataInicioSemana, dataFimPMO, revisao);
                if (semanaOperativa != null)
                {
                    semanasOperativas.Add(semanaOperativa);
                    revisao++;
                }
                dataInicioSemana = dataInicioSemana.AddDays(7);
            }
            return semanasOperativas;
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
