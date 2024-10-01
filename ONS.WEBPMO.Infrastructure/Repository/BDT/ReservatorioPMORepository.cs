using ONS.WEBPMO.Domain.Entities.BDT;
using ONS.WEBPMO.Domain.Repository.BDT;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;
using System.Text;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories.BDT
{
    //[UseDbContext(ConnectionStringsNames.BDTModel)]
    public class ReservatorioPMORepository : Repository<ReservatorioPMO>, IReservatorioPMORepository
    {
        public ReservatorioPMORepository(WEBPMODbContext context) : base(context)
        {
        }
        //verificar se usa outra conexão tal como o infomix

        public IList<ReservatorioPMO> ConsultarPorChaves(string[] chaves)
        {
            throw new NotImplementedException();
        }

        public IList<ReservatorioPMO> ConsultarPorNomeExibicao(string nomeExibicaoContem)
        {
            throw new NotImplementedException();
        }
    }
}
