
namespace ONS.WEBPMO.Application.DTO
{
    public class DadosManutencaoGabaritoNaoEstruturadoDTO
    {
        public DadosManutencaoGabaritoNaoEstruturadoDTO()
        {
            Insumos = new List<ChaveDescricaoDTO<int>>();
            InsumosGabarito = new List<ChaveDescricaoDTO<int>>();

        }

        public ChaveDescricaoDTO<int> Agente { get; set; }
        public ChaveDescricaoDTO<int> SemanaOperativa { get; set; }

        public IList<ChaveDescricaoDTO<int>> Insumos { get; set; }
        public IList<ChaveDescricaoDTO<int>> InsumosGabarito { get; set; }
    }
}
