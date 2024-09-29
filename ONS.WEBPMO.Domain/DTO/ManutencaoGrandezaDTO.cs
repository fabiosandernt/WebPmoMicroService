namespace ONS.WEBPMO.Domain.DTO
{
    public class ManutencaoGrandezaDTO
    {
        public int Id { get; set; }
        public int IdInsumo { get; set; }
        public short? OrdemExibicao { get; set; }
        public string Nome { get; set; }
        public bool IsColetaPorEstagio { get; set; }
        public bool IsColetaPorPatamar { get; set; }
        public bool IsColetaPorLimite { get; set; }
        public bool AceitaValorNegativo { get; set; }
        public bool PodeRecuperarValor { get; set; }
        public bool DestacaDiferenca { get; set; }
        public bool IsObrigatorio { get; set; }
        public int TipoDadoGrandezaId { get; set; }
        public int? QuantidadeCasasInteira { get; set; }
        public int? QuantidadeCasasDecimais { get; set; }
        public bool Ativo { get; set; }
        public bool IsPreAprovadoComAlteracao { get; set; }
    }
}
