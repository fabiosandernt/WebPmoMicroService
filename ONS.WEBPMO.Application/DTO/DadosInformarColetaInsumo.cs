

using ONS.Infra.Core.Pagination;

namespace ONS.WEBPMO.Application.DTO
{
    public class DadosInformarColetaInsumoDTO
    {
        public PagedResult<DadoColetaDTO> DadosColetaInsumoPaginado { get; set; }
    }
}
