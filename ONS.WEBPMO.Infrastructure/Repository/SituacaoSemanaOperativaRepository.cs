using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.PMO;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories
{
    public class SituacaoSemanaOperativaRepository : Repository<SituacaoSemanaOperativa>, ISituacaoSemanaOperativaRepository
    {
        public SituacaoSemanaOperativaRepository(WEBPMODbContext context) : base(context)
        {
        }
    }
}
