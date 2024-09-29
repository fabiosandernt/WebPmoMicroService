
namespace ONS.WEBPMO.Domain.DTO
{
    public class DadosManutencaoGabaritoDTO
    {
        public DadosManutencaoGabaritoDTO()
        {
            Insumos = new List<ChaveDescricaoDTO<int>>();
            InsumosGabarito = new List<ChaveDescricaoDTO<int>>();

        }

        public string CodigoPerfilONS { get; set; }

        public ChaveDescricaoDTO<int> Agente { get; set; }
        public ChaveDescricaoDTO<string> OrigemColeta { get; set; }
        public ChaveDescricaoDTO<int> SemanaOperativa { get; set; }

        public IList<ChaveDescricaoDTO<int>> Insumos { get; set; }
        public IList<ChaveDescricaoDTO<int>> InsumosGabarito { get; set; }

        public IList<ChaveDescricaoDTO<string>> OrigensColeta { get; set; }
        public IList<ChaveDescricaoDTO<string>> OrigensColetaGabarito { get; set; }
    }
}
