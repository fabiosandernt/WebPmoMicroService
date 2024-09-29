namespace ONS.WEBPMO.Domain.DTO
{
    public class DadosAlteracaoDadoColetaManutencaoDTO : DadoColetaManutencaoDTO
    {
        public int IdColetaInsumo { get; set; }
        public byte[] VersaoColetaInsumo { get; set; }
    }
}
