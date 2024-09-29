
namespace ONS.WEBPMO.Domain.DTO
{
    public class DadoColetaInsumoDTO
    {
        public int IdColetaInsumo { get; set; }
        public string VersaoString { get; set; }
        public byte[] Versao { get; set; }
        public string MotivoAlteracaoONS { get; set; }
        public string MotivoRejeicaoONS { get; set; }
        public bool IsMonitorar { get; set; }
    }
}
