using ONS.Infra.Core.Pagination;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class LogNotificacaoRepository : Repository<LogNotificacao>, ILogNotificacaoRepository
    {
        public LogNotificacaoRepository(WEBPMODbContext context) : base(context)
        {
        }

        public PagedResult<LogNotificacao> ObterLogNotificacao(LogNotificacaoFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
