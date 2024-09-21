
namespace ONS.WEBPMO.Application.Models.ColetaInsumo
{
    public class DadoColetaModelBase
    {
        public string TipoDadoColeta { get; set; }
        public int TipoLimiteId { get; set; }
        public string TipoLimiteDescricao { get; set; }
        public int TipoPatamarId { get; set; }
        public string TipoPatamarDescricao { get; set; }
        public int GrandezaId { get; set; }
        public string GrandezaNome { get; set; }
        public string OrigemColetaId { get; set; }
        public string OrigemColetaNome { get; set; }
    }
}