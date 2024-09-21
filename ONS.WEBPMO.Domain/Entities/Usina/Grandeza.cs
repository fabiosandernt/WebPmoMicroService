namespace ONS.WEBPMO.Domain.Entities.Usina
{  
    public class Grandeza : BaseObject
    {
        public Grandeza()
        {
            this.DadosColeta = new HashSet<DadoColeta>();
        }

        public string Nome { get; set; }
        public short OrdemExibicao { get; set; }
        public bool IsColetaPorPatamar { get; set; }
        public bool IsColetaPorLimite { get; set; }
        public bool IsColetaPorEstagio { get; set; }
        public bool AceitaValorNegativo { get; set; }
        public bool PodeRecuperarValor { get; set; }
        public bool DestacaDiferenca { get; set; }
        public bool IsObrigatorio { get; set; }
        public bool Ativo { get; set; }
        public bool ParticipaBlocoMontador { get; set; }
        public int? OrdemBlocoMontador { get; set; }

        public virtual string TipoDadoGrandeza { get; set; }
        public virtual ISet<DadoColeta> DadosColeta { get; set; }
        public virtual InsumoEstruturado Insumo { get; set; }

        public int? QuantidadeCasasInteira { get; set; }
        public int? QuantidadeCasasDecimais { get; set; }
        public int TipoDadoGrandezaId { get; set; }
        public int InsumoId { get; set; }
    }
}
