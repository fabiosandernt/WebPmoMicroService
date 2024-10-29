using ONS.WEBPMO.Domain.Entities.BDT;
using ONS.WEBPMO.Domain.Repository.BDT;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories.BDT
{
    //[UseDbContext(ConnectionStringsNames.BDTModel)]
    public class UsinaPMORepository : Repository<UsinaPMO>, IUsinaPMORepository
    {
        public UsinaPMORepository(WEBPMODbContext context) : base(context)
        {
        }

        public UsinaPMO Consultar(int CodUsinaPlanejamento, string codTipoGeracao)
        {
            throw new NotImplementedException();
        }

        public IList<UsinaPMO> Consultar()
        {
            throw new NotImplementedException();
        }

        public IList<UsinaPMO> ConsultarPorChaves(string[] chaves)
        {
            throw new NotImplementedException();
        }

        public IList<UsinaPMO> ConsultarPorNomeExibicao(string nomeExibicaoContem)
        {
            throw new NotImplementedException();
        }
    }
}
