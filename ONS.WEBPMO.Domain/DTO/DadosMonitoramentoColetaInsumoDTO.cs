
namespace ONS.WEBPMO.Domain.DTO
{
    public class DadosMonitoramentoColetaInsumoDTO
    {
        public DadosMonitoramentoColetaInsumoDTO()
        {
            this.IdsColetaInsumoCapturaVersaoString = new List<KeyValuePair<int, string>>();
        }

        public int IdColetaInsumo { get; set; }
        public string MotivoAlteracaoONS { get; set; }
        public string MotivoRejeicaoONS { get; set; }
        public byte[] VersaoColetaInsumo { get; set; }
        public IList<KeyValuePair<int, string>> IdsColetaInsumoCapturaVersaoString { get; set; }
    }
}
