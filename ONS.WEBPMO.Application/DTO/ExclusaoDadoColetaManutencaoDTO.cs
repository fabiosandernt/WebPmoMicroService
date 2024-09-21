namespace ONS.WEBPMO.Application.DTO
{
    public class ExclusaoDadoColetaManutencaoDTO
    {
        public string ListaIdsDadoColeta { get; set; }
        public int IdDadoColeta { get; set; }
        public byte[] VersaoColetaInsumo { get; set; }
        public bool IsMonitorar { get; set; }
    }
}
