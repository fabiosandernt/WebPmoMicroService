using ONS.WEBPMO.Domain.Entities.BDT;
using ONS.WEBPMO.Domain.Repository.BDT;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories.BDT
{
    //[UseDbContext(ConnectionStringsNames.BDTModel)]
    public class SubsistemaPMORepository : Repository<SubsistemaPMO>, ISubsistemaPMORepository
    {
        public SubsistemaPMORepository(WEBPMODbContext context) : base(context)
        {
        }

        public IList<SubsistemaPMO> ConsultarAtivos()
        {
            throw new NotImplementedException();
        }

        public IList<SubsistemaPMO> ConsultarOutros()
        {
            throw new NotImplementedException();
        }

        public IList<SubsistemaPMO> ConsultarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
