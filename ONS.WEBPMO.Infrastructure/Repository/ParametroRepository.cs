using ONS.Common.Repositories.Impl;
using ONS.Common.Util;
using ONS.SGIPMO.Domain.Entities;
using System.Linq;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories
{
    public class ParametroRepository : Repository<Parametro>, IParametroRepository
    {
        public Parametro ObterPorTipo(ParametroEnum parametro)
        {
            string nomeParametro = parametro.ToDescription();
            return EntitySet.FirstOrDefault(param => param.Nome == nomeParametro);
        }
    }
}
