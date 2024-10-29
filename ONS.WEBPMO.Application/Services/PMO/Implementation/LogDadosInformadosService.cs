using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class LogDadosInformadosService : ILogDadosInformadosService
    {

        private readonly ILogDadosInformadosRepository logDadosInformadosRepository;
        public LogDadosInformadosService(ILogDadosInformadosRepository logDadosInformadosRepository)
        {
            this.logDadosInformadosRepository = logDadosInformadosRepository;
        }

        public ICollection<LogDadosInformados> obterLogDadosInformados(LogDadosInformadosFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
