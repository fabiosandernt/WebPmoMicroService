using ONS.WEBPMO.Domain.Entities.Base;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository
{
    public interface IPMORepository : IRepository<PMO>
    {
        PMO ObterPorFiltro(IBaseFilter filtro);
        PMO ObterPorFiltroExterno(PMOFilter filtro);
        int ObterQuantidadeSemanasPMO(int idSemanaOperativa);
    }
}
