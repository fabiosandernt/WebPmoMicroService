using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository.PMO
{
    public interface IUnidadeGeradoraRepository : IRepository<UnidadeGeradora>
    {
        IList<UnidadeGeradora> FindByKeys(params string[] ids);
    }
}
