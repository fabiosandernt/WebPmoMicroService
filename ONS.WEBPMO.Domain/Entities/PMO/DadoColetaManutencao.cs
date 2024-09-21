namespace ONS.WEBPMO.Domain.Entities.PMO
{
    public class DadoColetaManutencao : DadoColeta
    {
        public DadoColetaManutencao() { }

        public DadoColetaManutencao(DadoColetaManutencao dado)
        {
            DataInicio = dado.DataInicio;
            DataFim = dado.DataFim;
            TempoRetorno = dado.TempoRetorno;
            Justificativa = dado.Justificativa;
            Numero = dado.Numero;
            UnidadeGeradora = dado.UnidadeGeradora;
            TipoDadoColeta = dado.TipoDadoColeta;
            ColetaInsumo = dado.ColetaInsumo;
            Gabarito = dado.Gabarito;
            Grandeza = dado.Grandeza;
            Periodicidade = dado.Periodicidade;
        }


        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string TempoRetorno { get; set; }
        public string Justificativa { get; set; }
        public string Numero { get; set; }
        public virtual UnidadeGeradora UnidadeGeradora { get; set; }
        public string UnidadeGeradoraId { get; set; }
        //[NotMapped]
        public string Periodicidade { get; set; }
        public string Situacao { get; set; }
        public string ClassificacaoPorTipoEquipamento { get; set; }
    }
}
