
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository
{
    public interface IUnidadeGeradoraRepository : IRepository<UnidadeGeradora>
    {
        IList<UnidadeGeradora> FindByKeys(params string[] ids);
    }
}
