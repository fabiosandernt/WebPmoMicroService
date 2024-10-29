using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Repository;

namespace ONS.WEBPMO.Domain.Presentations.Impl
{
    public class GabaritoPresentation : IGabaritoPresentation
    {
        private readonly IGabaritoRepository gabaritoRepository;
        private readonly ISemanaOperativaRepository semanaOperativaRepository;
        private readonly IInsumoService insumoService;
        private readonly IOrigemColetaService origemColetaService;
        private readonly IAgenteService agenteService;
        private readonly IInsumoRepository insumoRepository;
        private readonly IAgenteRepository agenteRepository;

        public GabaritoPresentation(
            IGabaritoRepository gabaritoRepository,
            ISemanaOperativaRepository semanaOperativaRepository,
            IInsumoService insumoService,
            IOrigemColetaService origemColetaService,
            IAgenteService agenteService,
            IInsumoRepository insumoRepository,
            IAgenteRepository agenteRepository)
        {
            this.gabaritoRepository = gabaritoRepository;
            this.semanaOperativaRepository = semanaOperativaRepository;
            this.insumoService = insumoService;
            this.origemColetaService = origemColetaService;
            this.agenteService = agenteService;
            this.insumoRepository = insumoRepository;
            this.agenteRepository = agenteRepository;
        }

        public DadosConfiguracaoGabaritoDTO ObterDadosConfiguracaoGabarito(GabaritoDadosFilter filter)
        {
            throw new NotImplementedException();
        }

        public DadosConfiguracaoGabaritoUnidadeGeradoraDTO ObterDadosConfiguracaoGabaritoUnidadeGeradora(GabaritoDadosFilter filter)
        {
            throw new NotImplementedException();
        }

        public DadosConfiguracaoGabaritoDTO ObterDadosConfiguracaoGabaritoUnidadeGeradoraPorUsina(string idUsina)
        {
            throw new NotImplementedException();
        }

        public DadosFiltroPesquisaGabaritoDTO ObterDadosFiltroPesquisaGabarito(int? idInsumo, int? idAgente, int? idSemanaOperativa)
        {
            throw new NotImplementedException();
        }

        public DadosManutencaoGabaritoDTO ObterDadosManutencaoGabarito(GabaritoOrigemColetaFilter filtro)
        {
            throw new NotImplementedException();
        }
    }
}
