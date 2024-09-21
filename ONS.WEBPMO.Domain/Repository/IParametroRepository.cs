
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository
{
    public interface IParametroRepository : IRepository<Parametro>
    {
        Parametro ObterPorTipo(ParametroEnum parametro);
    }
}
