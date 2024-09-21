using ONS.Common.Repositories.Impl;
using ONS.SGIPMO.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class UnidadeGeradoraRepository : Repository<UnidadeGeradora>, IUnidadeGeradoraRepository
    {
        public IList<UnidadeGeradora> FindByKeys(params string[] ids)
        {
            return EntitySet.Where(unidade => ids.Contains(unidade.Id)).ToList();
        }
    }
}
