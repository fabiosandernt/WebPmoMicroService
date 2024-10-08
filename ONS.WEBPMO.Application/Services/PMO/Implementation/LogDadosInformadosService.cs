using ONS.Common.Services.Impl;
using ONS.Common.Util.Pagination;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class LogDadosInformadosService : Service, ILogDadosInformadosService
    {

        private readonly ILogDadosInformadosRepository logDadosInformadosRepository;
        public LogDadosInformadosService(ILogDadosInformadosRepository logDadosInformadosRepository)
        {
            this.logDadosInformadosRepository = logDadosInformadosRepository;
        }

        public PagedResult<LogDadosInformados> obterLogDadosInformados(LogDadosInformadosFilter filter)
        {
            PagedResult<LogDadosInformados> logsDadosInformados = new PagedResult<LogDadosInformados>(new List<LogDadosInformados>(), 0, 0, 0);

            logsDadosInformados = logDadosInformadosRepository.ConsultarPorFiltro(filter);

            return logsDadosInformados;
        }
    }
}
