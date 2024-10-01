using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class PMORepository : Repository<PMO>, IPMORepository
    {
        public PMORepository(WEBPMODbContext context) : base(context)
        {
        }

        public PMO ObterPorFiltro(PMOFilter filtro)
        {
            throw new NotImplementedException();
        }

        public PMO ObterPorFiltroExterno(PMOFilter filtro)
        {
            throw new NotImplementedException();
        }

        public int ObterQuantidadeSemanasPMO(int idSemanaOperativa)
        {
            throw new NotImplementedException();
        }
    }
}
