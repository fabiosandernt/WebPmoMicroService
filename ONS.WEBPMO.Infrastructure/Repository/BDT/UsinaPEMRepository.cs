using ONS.WEBPMO.Domain.Entities.BDT;
using ONS.WEBPMO.Domain.Repository.BDT;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories.BDT
{
    //[UseDbContext(ConnectionStringsNames.BDTModel)]
    public class UsinaPEMRepository : Repository<UsinaPEM>, IUsinaPEMRepository
    {
        public UsinaPEMRepository(WEBPMODbContext context) : base(context)
        {
        }
        //verificar se usa outra conexão tal como o infomix

        public IList<UsinaPEM> ConsultarDadosUsinasVisaoPEM()
        {
            throw new NotImplementedException();
        }
    }
}
