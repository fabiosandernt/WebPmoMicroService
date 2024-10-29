using AutoMapper;
using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Models.Insumo;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Repository;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class PMOService : IPMOService
    {
        private readonly IPMORepository pmoRepository;
        private readonly ISemanaOperativaService semanaOperativaService;
        private readonly IParametroService parametroService;
        private readonly IHistoricoService historicoService;
        private readonly IMapper _mapper;

        public PMOService(IPMORepository pmoRepository,
            ISemanaOperativaService semanaOperativaService,
            IParametroService parametroService,
            IHistoricoService historicoService, IMapper mapper)
        {
            this.pmoRepository = pmoRepository;
            this.semanaOperativaService = semanaOperativaService;
            this.parametroService = parametroService;
            this.historicoService = historicoService;
            _mapper = mapper;
        }

        public Task AtualizarMesesAdiantePMOAsync(int idPMO, int? mesesAdiante, byte[] versao)
        {
            throw new NotImplementedException();
        }

        public Task ExcluirPMOAsync(DadosPMODTO dto)
        {
            throw new NotImplementedException();
        }

        public Task ExcluirUltimaSemanaOperativaAsync(int idPMO, byte[] versaoPMO)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.PMO.PMO> GerarPMOAsync(int ano, int mes)
        {
            throw new NotImplementedException();
        }

        public Task IncluirSemanaOperativaAsync(InclusaoSemanaOperativaDTO dto)
        {
            throw new NotImplementedException();
        }

        public  async ValueTask<Domain.Entities.PMO.PMO> ObterPMOPorChaveAsync(int chave)
        {
            var query = await  pmoRepository.GetByIdAsync(chave);
            var pmo = _mapper.Map<Domain.Entities.PMO.PMO>(query);
            return pmo;
        }

        public async Task<ICollection<PMODTO>> ObterPMOPorFiltroAsync(PMOFilter filter)
        {
          
           var query =  this.pmoRepository.GetByQueryable(filter);
           var pmoDto = _mapper.Map<List<PMODTO>>(query);
           return pmoDto;
        }

        public ValueTask<Domain.Entities.PMO.PMO> ObterPMOPorFiltroExternoAsync(PMOFilter filtro)
        {
            throw new NotImplementedException();
        }
    }
}
