using AutoMapper;
using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Models.Insumo;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Enumerations;
using ONS.WEBPMO.Domain.Repository.PMO;


namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class InsumoService : IInsumoService
    {
        private readonly IInsumoRepository insumoRepository;
        private readonly IOrigemColetaService origemColetaService;
        private readonly ICategoriaInsumoRepository categoriaInsumoRepository;
        private readonly ITipoColetaRepository tipoColetaRepository;
        private readonly IGrandezaRepository grandezaRepository;
        private readonly IParametroRepository parametroRepository;
        private readonly ITipoDadoGrandezaRepository tipoDadoGrandezaRepository;
        private readonly IMapper _mapper;

        public InsumoService(
            IInsumoRepository insumoRepository,
            IOrigemColetaService origemColetaService,
            ICategoriaInsumoRepository categoriaInsumoRepository,
            ITipoColetaRepository tipoColetaRepository,
            IGrandezaRepository grandezaRepository,
            IParametroRepository parametroRepository,
            ITipoDadoGrandezaRepository tipoDadoGrandezaRepository,
            IMapper mapper)
        {
            this.insumoRepository = insumoRepository;
            this.origemColetaService = origemColetaService;
            this.categoriaInsumoRepository = categoriaInsumoRepository;
            this.tipoColetaRepository = tipoColetaRepository;
            this.grandezaRepository = grandezaRepository;
            this.parametroRepository = parametroRepository;
            this.tipoDadoGrandezaRepository = tipoDadoGrandezaRepository;
            _mapper = mapper;
        }

        public ICollection<VisualizarInsumoModel> GetByQueryableAsync(InsumoFiltro filter)
        {
            var query = this.insumoRepository.GetByQueryable(filter).ToList();
            //var insumos = _mapper.Map<List<VisualizarInsumoModel>>(query);
            var insumosDtos = new List<VisualizarInsumoModel>();

            foreach (var insumo in query)
            {
                var insumoDto = new VisualizarInsumoModel
                {
                    Id = insumo.Id,
                    Nome = insumo.Nome,
                    OrdemExibicao = insumo.OrdemExibicao,
                    PreAprovado = insumo.PreAprovado.ToString(),
                    Reservado = insumo.Reservado.ToString(),
                    TipoInsumo = insumo.TipoInsumo,
                    SiglaInsumo = insumo.SiglaInsumo,
                    ExportarInsumo = insumo.ExportarInsumo.ToString(),
                    Ativo = insumo.Ativo.ToString()
                };

                insumosDtos.Add(insumoDto);
            }

            return insumosDtos;
        }


        public async Task<IList<VisualizarInsumoModel>> ConsultarTodosInsumos()
        {
            var query = await insumoRepository.GetAllAsync();

            var insumosDto = new List<VisualizarInsumoModel>();

            foreach (var insumo in query)
            {
                var insumoDto = new VisualizarInsumoModel
                {
                    Id = insumo.Id,
                    Nome = insumo.Nome,
                    OrdemExibicao = insumo.OrdemExibicao,
                    PreAprovado = insumo.PreAprovado.ToString(),
                    Reservado = insumo.Reservado.ToString(),
                    TipoInsumo = insumo.TipoInsumo,
                    SiglaInsumo = insumo.SiglaInsumo,
                    ExportarInsumo = insumo.ExportarInsumo.ToString(),
                    Ativo = insumo.Ativo.ToString()
                };

                insumosDto.Add(insumoDto);
            }

            return insumosDto;
        }

        public Task AlterarInsumoEstruturadoAsync(DadosInclusaoInsumoEstruturadoDTO dadosInsumoEstruturado)
        {
            throw new NotImplementedException();
        }

        public Task AlterarInsumoNaoEstruturadoAsync(InsumoNaoEstruturado insumo, byte[] versao)
        {
            throw new NotImplementedException();
        }

        public async Task<InsumoDto> ConsultarInsumoAsync(int id)
        {
            var insumo = await insumoRepository.GetByIdAsync(id);

            var insumoDto = new InsumoDto()
            {
                Id = insumo.Id,
                Nome = insumo.Nome,
                OrdemExibicao = insumo.OrdemExibicao,
                PreAprovado = insumo.PreAprovado,
                Reservado = insumo.Reservado,
                TipoInsumo = insumo.TipoInsumo,
                SiglaInsumo = insumo.SiglaInsumo,
                ExportarInsumo = insumo.ExportarInsumo,
                Ativo = insumo.Ativo
            };

            return insumoDto;

        }

        public Task<IList<InsumoEstruturado>> ConsultarInsumoEstruturadosPorUsinaAsync(string idUsina)
        {
            throw new NotImplementedException();
        }

        public Task<IList<InsumoNaoEstruturado>> ConsultarInsumoNaoEstruturadoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IList<InsumoEstruturado>> ConsultarInsumoPorTipoOrigemColetaCategoriaAsync(TipoOrigemColetaEnum tipoOrigemColeta, CategoriaInsumoEnum? categoria = null)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Insumo>> ConsultarInsumosNaoEstruturadoEEstruturadoComGrandezaAtivaAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Insumo>> ConsultarInsumosPorFiltroAsync(InsumoFiltro filtro)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Insumo>> ConsultarInsumosPorNomeAsync(string nomeInsumo)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Insumo>> ConsultarInsumosPorSemanaOperativaAgentesAsync(int idSemanaOperativa, params int[] idsAgente)
        {
            throw new NotImplementedException();
        }

        public Task<IList<VisualizarInsumoModel>> ConsultarTodosInsumosAsync()
        {
            throw new NotImplementedException();
        }

        public Task ExcluirInsumoPorChaveAsync(int idInsumo, byte[] versao)
        {
            throw new NotImplementedException();
        }

        public ICollection<VisualizarInsumoModel> GetByQueryable(InsumoFiltro filter)
        {
            var query = this.insumoRepository.GetByQueryable(filter);
            var insumos = _mapper.Map<List<VisualizarInsumoModel>>(query);

            return insumos;
        }



        public Task<int> InserirInsumoEstruturadoAsync(DadosInclusaoInsumoEstruturadoDTO insumo)
        {
            throw new NotImplementedException();
        }

        public Task<int> InserirInsumoNaoEstruturadoAsync(InsumoNaoEstruturado insumo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsumoBloqueadosAlteracaoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Grandeza> ObterGrandezaPorIdAsync(int idGrandeza)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Grandeza>> ObterGrandezasPorInsumoAsync(int idInsumo)
        {
            throw new NotImplementedException();
        }

        public Task<InsumoEstruturado> ObterInsumoEstruturadoPorChaveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<InsumoNaoEstruturado> ObterInsumoNaoEstruturadoPorChaveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TipoDadoGrandeza>> ObterTiposDadoGrandezaAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> PermitirAlteracaoGrandezaAsync(int idGrandeza)
        {
            throw new NotImplementedException();
        }

        public Task ValidarExclusaoGrandezaAsync(int idGrandeza)
        {
            throw new NotImplementedException();
        }

        public Task ValidarIncluirAlterarGrandezaAsync(Grandeza grandeza)
        {
            throw new NotImplementedException();
        }

        public Task VerificarInsumoReservadoAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
