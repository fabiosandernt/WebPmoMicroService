using AspNetCore.IQueryable.Extensions;
using AutoMapper;
using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Filters;
using ONS.WEBPMO.Domain.Repository;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class ParametroService : IParametroService
    {
        private readonly IMapper _mapper;
        private readonly IParametroRepository _parametroRepository;

        public ParametroService(IMapper mapper, IParametroRepository parametroRepository)
        {
            _mapper = mapper;
            _parametroRepository = parametroRepository;
        }

        public async ValueTask<ParametroDTO> ObterParametroPorFiltro(ParametroFilter filter)
        {
            var parametro = _parametroRepository.FindOneByCriterioAsync(x=>x.Nome == filter.Nome);            
            return _mapper.Map<ParametroDTO>(parametro);
           
        }
    }
}
