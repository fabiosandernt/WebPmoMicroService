
namespace ONS.WEBPMO.Domain.DTO
{
    public class DadosConfiguracaoGabaritoNaoEstruturadoDTO
    {
        public DadosConfiguracaoGabaritoNaoEstruturadoDTO()
        {
            Agentes = new List<ChaveDescricaoDTO<int>>();
            Insumos = new List<ChaveDescricaoDTO<int>>();
        }

        public ChaveDescricaoDTO<int> SemanaOperativa { get; set; }
        public IList<ChaveDescricaoDTO<int>> Agentes { get; set; }
        public IList<ChaveDescricaoDTO<int>> Insumos { get; set; }

    }
}
