
namespace ONS.WEBPMO.Domain.DTO
{
    public class AlteracaoDadoColetaManutencaoDTO
    {
        public int IdDadoColeta { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string TempoRetorno { get; set; }
        public string Justificativa { get; set; }
        public string Numero { get; set; }
        public byte[] VersaoColetaInsumo { get; set; }
        public bool IsMonitorar { get; set; }
    }
}
