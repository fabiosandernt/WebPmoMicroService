using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Enumerations;
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository.PMO
{
    public interface IParametroRepository : IRepository<Parametro>
    {
        Parametro ObterPorTipo(ParametroEnum parametro);
    }
}
