

using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository
{
    public interface ILogNotificacaoRepository : IRepository<LogNotificacao>
    {
        PagedResult<LogNotificacao> ObterLogNotificacao(LogNotificacaoFilter filter);
    }
}
