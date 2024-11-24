using AutoMapper;
using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Models.PMO;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.Resources;
using ONS.WEBPMO.Domain.Enumerations;
using ONS.WEBPMO.Domain.Repository.PMO;
using ONS.WEBPMO.Domain.Resources;

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

        public Domain.Entities.PMO.PMO GerarPMO(IncluirPMODto dto)
        {
            ValidarInclusaoPMO(dto.AnoReferencia, dto.MesReferencia);
            Domain.Entities.PMO.PMO pmo = new Domain.Entities.PMO.PMO
            {
                AnoReferencia = dto.AnoReferencia,
                MesReferencia = dto.MesReferencia,
                SemanasOperativas = semanaOperativaService.GerarSugestaoSemanasOperativas(dto.AnoReferencia, dto.MesReferencia)
            };
            var parametroQtdMeses = parametroService.ObterParametro(ParametroEnum.QuantidadeMesesAFrente);
            if (parametroQtdMeses != null)
            {
                pmo.QuantidadeMesesAdiante = int.Parse(parametroQtdMeses.Valor);
            }
            pmoRepository.AddAsync(pmo);
            return pmo;
        }

        public async Task<PMOManterModel> GetByIdAsync(int id)
        {
            var pmo = await pmoRepository.GetByIdAsync(id);
            if (pmo == null)
                return null;
            var pmoModel = _mapper.Map<PMOManterModel>(pmo);
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

            //Outra forma de fazer em casos de erro no mapeamento quando vem direto de consulta QueryAble
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

        private void ValidarInclusaoPMO(int ano, int mes)
        {
            IList<string> mensagens = new List<string>();

            DateTime dataCorrente = DateTime.Now.Date;
            if (dataCorrente.Year > ano
                || (dataCorrente.Year == ano && dataCorrente.Month > mes))
            {
                mensagens.Add(SGIPMOMessages.MS003);
            }

            PMOFilter filtro = new PMOFilter()
            {
                Ano = ano,
                Mes = mes
            };

            Domain.Entities.PMO.PMO pmo =  pmoRepository.ObterPorFiltroExterno(filtro);

            var pmoModel = _mapper.Map<PMOManterModel>(pmo);
            
            if (pmoModel != null)
            {
                var msg = BusinessMessage.Get("MS002");
                mensagens.Add(msg.Value);
            }
            if (mensagens.Any())
            {
                throw new BusinessValidationException(mensagens);
            }
        }
    }
}
