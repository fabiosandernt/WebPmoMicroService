using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.PMO;

namespace ONS.WEBPMO.Domain.Presentations.Impl
{
    public class ColetaInsumoPresentation : IColetaInsumoPresentation
    {
        private readonly IAgenteService agenteService;
        private readonly IInsumoService insumoService;
        private readonly ISituacaoColetaInsumoRepository situacaoColetaInsumoRepository;
        private readonly IOrigemColetaService origemColetaService;
        private readonly IDadoColetaManutencaoService dadoColetaManutencaoService;
        private readonly ISGIService SGIService;
        private readonly IColetaInsumoRepository coletaInsumoRepository;
        private readonly ISemanaOperativaRepository semanaOperativaRepository;
        private readonly IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository;
        private readonly ISGIService sgiService;
        private readonly IColetaInsumoService coletaInsumoService;
        private readonly IDadoColetaManutencaoService dadoColetaManutencao;
        private readonly IGabaritoRepository gabaritoRepository;

        public ColetaInsumoPresentation(
            IAgenteService agenteService,
            IInsumoService insumoService,
            ISituacaoColetaInsumoRepository situacaoColetaInsumoRepository,
            IOrigemColetaService origemColetaService,
            IDadoColetaManutencaoService dadoColetaManutencaoService,
            ISGIService SGIService,
            IColetaInsumoRepository coletaInsumoRepository,
            ISemanaOperativaRepository semanaOperativaRepository,
            IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository,
            ISGIService sgiService, IColetaInsumoService coletaInsumoService,
            IDadoColetaManutencaoService dadoColetaManutencao,
            IGabaritoRepository gabaritoRepository)
        {
            this.agenteService = agenteService;
            this.insumoService = insumoService;
            this.situacaoColetaInsumoRepository = situacaoColetaInsumoRepository;
            this.origemColetaService = origemColetaService;
            this.dadoColetaManutencaoService = dadoColetaManutencaoService;
            this.SGIService = SGIService;
            this.coletaInsumoRepository = coletaInsumoRepository;
            this.semanaOperativaRepository = semanaOperativaRepository;
            this.dadoColetaNaoEstruturadoRepository = dadoColetaNaoEstruturadoRepository;
            this.sgiService = sgiService;
            this.coletaInsumoService = coletaInsumoService;
            this.dadoColetaManutencao = dadoColetaManutencao;
            this.gabaritoRepository = gabaritoRepository;
        }

        public IList<DadoColetaManutencao> ConsultarManutencaoSGI(int idColetaInsumo)
        {
            throw new NotImplementedException();
        }

        public void ImportarCronogramaManutencaoHidraulicaTermica(int idSemanaOperativa, IList<int> idsInsumo)
        {
            throw new NotImplementedException();
        }

        public DadosAlteracaoDadoColetaManutencaoDTO ObterDadosAlteracaoDadoColetaManutencao(int idDadoColeta)
        {
            throw new NotImplementedException();
        }

        public DadosInclusaoDadoColetaManutencaoDTO ObterDadosInclusaoDadoColetaManutencao(int idColetaInsumo)
        {
            throw new NotImplementedException();
        }

        public DadosPesquisaColetaInsumoDTO ObterDadosPesquisaColetaInsumo(int? idSemanaOperativa = null, bool isMonitorar = false, bool? ordernarListagens = true)
        {
            throw new NotImplementedException();
        }

        public DadosPesquisaGeracaoBlocosDTO ObterDadosPesquisaGeracaoBloco(int idSemanaOperativa)
        {
            throw new NotImplementedException();
        }
    }
}
