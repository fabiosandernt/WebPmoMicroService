using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Repository.PMO;

namespace ONS.WEBPMO.Domain.Presentations.Impl
{
    public class LogNotificacaoPresentation : ILogNotificacaoPresentation
    {
        private readonly IAgenteService agenteService;
        private readonly ISemanaOperativaRepository semanaOperativaRepository;

        public LogNotificacaoPresentation(IAgenteService agenteService, ISemanaOperativaRepository semanaOperativaRepository)
        {
            this.agenteService = agenteService;
            this.semanaOperativaRepository = semanaOperativaRepository;
        }

        public LogNotificacaoDTO ObterDadosPesquisaLogNotificacao(int? idSemanaOperativa = null, bool isMonitorar = false, bool? ordernarListagens = true)
        {
            throw new NotImplementedException();
        }
    }
}
