using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class ArquivoSemanaOperativaRepository : Repository<ArquivoSemanaOperativa>, IArquivoSemanaOperativaRepository
    {
        public ArquivoSemanaOperativaRepository(WEBPMODbContext context) : base(context)
        {
        }
    }
}
