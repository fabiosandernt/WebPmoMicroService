using AutoMapper;
using AutoMapper.QueryableExtensions;
using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Models.PMO;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Repository.PMO;
using System.Data.Entity;

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

        public async Task<PMOManterModel> GetByIdAsync(int id)
        {
           
            var pmo = await pmoRepository.GetByIdAsync(id);

            // Retorna null se não encontrar
            if (pmo == null)
                return null;
            var pmoModel = _mapper.Map<PMOManterModel>(pmo);

            // Mapeamento manual da entidade para o modelo
            //var pmoModel = new PMOManterModel
            //{
            //    Id = pmo.Id,
            //    AnoReferencia = pmo.AnoReferencia,
            //    MesReferencia = pmo.MesReferencia,
            //    QuantidadeMesesAdiante = pmo.QuantidadeMesesAdiante,
            //    Versao = pmo.Versao,
            //    VersaoPmoString = pmo.Versao != null ? Convert.ToBase64String(pmo.Versao) : null,
            //    SemanasOperativas = pmo.SemanasOperativas?.Select(so => new SemanaOperativaModel
            //    {
            //        Id = so.Id,
            //        PMOAnoReferencia = so.PMO.AnoReferencia,
            //        PMOMesReferencia = so.PMO.MesReferencia,
            //        DataReuniao = so.DataReuniao,
            //        DataInicioSemana = so.DataInicioSemana,
            //        DataFimSemana = so.DataFimSemana,
            //        DataInicioManutencao = so.DataInicioManutencao,
            //        DataFimManutencao = so.DataFimManutencao,
            //        SituacaoDescricao = so.Situacao != null ? so.Situacao.Descricao : "Não definida",
            //        Revisao = so.Revisao,
            //        Versao = so.Versao
            //    }).ToList() ?? new List<SemanaOperativaModel>()
            //};

            return pmoModel;
        }

        public Task IncluirSemanaOperativaAsync(InclusaoSemanaOperativaDTO dto)
        {
            throw new NotImplementedException();
        }

        
               

        public async Task<PMOManterModel> ObterPMOPorFiltroAsync(PMOFilter filter)
        {

            var pmo = await this.pmoRepository.GetSingleByQueryableAsync(filter);


            var pmoModel = _mapper.Map<PMOManterModel>(pmo);
            return pmoModel;

            //var query = pmoRepository.GetByQueryable(filter);
            //// Projeta diretamente para o DTO
            //var pmoDto = await query.ProjectTo<PMODTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            //return pmoDto;
        }

        public ValueTask<Domain.Entities.PMO.PMO> ObterPMOPorFiltroExternoAsync(PMOFilter filtro)
        {
            throw new NotImplementedException();
        }

        ValueTask<Domain.Entities.PMO.PMO> IPMOService.ObterPMOPorChaveAsync(int chave)
        {
            throw new NotImplementedException();
        }
    }
}
