using ONS.WEBPMO.Domain.Entities.BDT;
using ONS.WEBPMO.Domain.Repository.BDT;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;
using System.Text;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories.BDT
{
    //[UseDbContext(ConnectionStringsNames.BDTModel)]
    public class UnidadeGeradoraPMORepository : Repository<UnidadeGeradoraPMO>, IUnidadeGeradoraPMORepository
    {
        public UnidadeGeradoraPMORepository(WEBPMODbContext context) : base(context)
        {
        }
        //verificar se usa outra conexão tal como o infomix

        public IList<UnidadeGeradoraPMO> Consultar()
        {
            throw new NotImplementedException();
        }

        public IList<UnidadeGeradoraPMO> ConsultarPorChaves(string[] chaves)
        {
            throw new NotImplementedException();
        }

        public IList<UnidadeGeradoraPMO> ConsultarPorUsina(string chaveUsina)
        {
            throw new NotImplementedException();
        }
    }
}
