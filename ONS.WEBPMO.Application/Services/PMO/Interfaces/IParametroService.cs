

using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Domain.Filters;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    public interface IParametroService
    {
        ValueTask<ParametroDTO> ObterParametroPorFiltro(string filter);
    }
}
