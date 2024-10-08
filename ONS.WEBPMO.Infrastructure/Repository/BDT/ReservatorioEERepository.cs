using ONS.WEBPMO.Domain.Entities.BDT;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;
using ONS.WEBPMO.Domain.Repository.BDT;
using System.Text;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories.BDT
{
    //[UseDbContext(ConnectionStringsNames.BDTModel)]
    public class ReservatorioEERepository : Repository<ReservatorioEE>, IReservatorioEERepository
    {
        public ReservatorioEERepository(WEBPMODbContext context) : base(context)
        {
        }
        //verificar se usa outra conexão tal como o infomix

        public IList<ReservatorioEE> ConsultarReservatoriosEquivalentesDeEnergiaAtivos()
        {
            throw new NotImplementedException();
        }
    }
}
