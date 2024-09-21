
namespace ONS.WEBPMO.Application.DTO
{
    public class DadosInsumoConsultaDTO
    {
        public DadosInsumoConsultaDTO()
        {
            Categorias = new List<ChaveDescricaoDTO<int>>();
            TiposColeta = new List<ChaveDescricaoDTO<int>>();
        }

        public IList<ChaveDescricaoDTO<int>> Categorias { get; set; }
        public IList<ChaveDescricaoDTO<int>> TiposColeta { get; set; }
    }
}
