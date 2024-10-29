using log4net;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class SGIService : ISGIService
    {
        private readonly static ILog log = LogManager.GetLogger(typeof(ISGIService));

        public IList<DadoColetaManutencao> ObterManutencoesPorChaves(string[] chavesUnidadesGeradoras, DateTime dataInicio, DateTime dataFim)
        {
            throw new NotImplementedException();
        }

        //private readonly IUnidadeGeradoraRepository unidadeGeradoraRepository;
        //private readonly SGIExternalServices.interfaceSGIPMO servicoSGI;

        //public SGIService(IUnidadeGeradoraRepository unidadeGeradoraRepository, interfaceSGIPMO servicoSGI)
        //{
        //    this.servicoSGI = servicoSGI;
        //    this.unidadeGeradoraRepository = unidadeGeradoraRepository;
        //}


    }
}
