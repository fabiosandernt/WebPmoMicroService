using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.PMO;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class LogNotificacaoRepository : Repository<LogNotificacao>, ILogNotificacaoRepository
    {
        public LogNotificacaoRepository(WEBPMODbContext context) : base(context)
        {
        }

        public ICollection<LogNotificacao> ObterLogNotificacao(LogNotificacaoFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
