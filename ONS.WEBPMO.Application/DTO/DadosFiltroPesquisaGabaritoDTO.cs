
namespace ONS.WEBPMO.Application.DTO
{
    public class DadosFiltroPesquisaGabaritoDTO
    {
        public DadosFiltroPesquisaGabaritoDTO()
        {
            this.SemanasOperativas = new List<ChaveDescricaoDTO<int>>();
            this.Agentes = new List<ChaveDescricaoDTO<int>>();
            this.Insumos = new List<ChaveDescricaoDTO<int>>();
        }

        public IList<ChaveDescricaoDTO<int>> SemanasOperativas { get; set; }
        public IList<ChaveDescricaoDTO<int>> Agentes { get; set; }
        public IList<ChaveDescricaoDTO<int>> Insumos { get; set; }
    }
}
