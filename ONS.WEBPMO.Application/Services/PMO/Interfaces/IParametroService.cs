using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Enumerations;


namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    public interface IParametroService
    {
        Parametro ObterParametro(ParametroEnum paramento);
    }
}
