
using ONS.Common.Services.Impl;
using ONS.Common.Util.Pagination;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class LogNotificacaoService : Service, ILogNotificacaoService
    {
        public const string LOG_NOTIFICACAO_ABERTURA = "Abertura";
        public const string LOG_NOTIFICACAO_REABERTURA = "Reabertura";
        public const string LOG_NOTIFICACAO_REJEICAO = "Rejeição";

        private readonly ILogNotificacaoRepository logNotificacaoRepository;
        public LogNotificacaoService(ILogNotificacaoRepository logNotificacaoRepository)
        {
            this.logNotificacaoRepository = logNotificacaoRepository;
        }

        public bool Apagar(List<int> idsLogs)
        {
            throw new NotImplementedException();
        }

        public void LogarNotificacao(SemanaOperativa semanaOperativa, List<Agente> agentes, DateTime? dataHoraAcao, string nomeUsuario, string acao)
        {
            throw new NotImplementedException();
        }

        public PagedResult<LogNotificacao> ObterLogNotificacao(LogNotificacaoFilter filter)
        {
            throw new NotImplementedException();
        }
    }

}
