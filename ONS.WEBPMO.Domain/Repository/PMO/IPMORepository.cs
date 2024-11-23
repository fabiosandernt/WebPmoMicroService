

using ONS.WEBPMO.Domain.Entities.Base;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository.PMO
{
    public interface IPMORepository : IRepository<ONS.WEBPMO.Domain.Entities.PMO.PMO>
    {
        ONS.WEBPMO.Domain.Entities.PMO.PMO ObterPorFiltro(IBaseFilter filtro);
        ONS.WEBPMO.Domain.Entities.PMO.PMO ObterPorFiltroExterno(PMOFilter filtro);
        int ObterQuantidadeSemanasPMO(int idSemanaOperativa);
    }
}
