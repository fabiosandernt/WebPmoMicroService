
namespace ONS.WEBPMO.Application.DTO
{
    public class DadosInclusaoDadoColetaManutencaoDTO
    {
        public int IdColetaInsumo { get; set; }
        public IList<ChaveDescricaoDTO<string>> Usinas { get; set; }
        public byte[] VersaoColetaInsumo { get; set; }
    }
}
