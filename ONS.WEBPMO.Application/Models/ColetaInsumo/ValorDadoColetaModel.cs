
using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Application.Models.ColetaInsumo
{
    public class ValorDadoColetaModel
    {
        public int Id { get; set; }
        public string Valor { get; set; }
        public string Estagio { get; set; }
        public string TipoDadoColeta { get; set; }
        public int TipoLimiteId { get; set; }
        public int TipoPatamarId { get; set; }
        public int GrandezaId { get; set; }
        public string GrandezaNome { get; set; }
        public string OrigemColetaId { get; set; }
        public string PeriodoSemana { get; set; }
        public string ValorEstudoAnterior { get; set; }
        public bool IsColetaPorEstagio { get; set; }
        public bool IsRecuperaValor { get; set; }
        public bool IsDestacaDiferenca { get; set; }
        public bool AceitaValorNegativo { get; set; }
        public bool IsObrigatorio { get; set; }
        public TipoDadoGrandezaEnum TipoDadoGrandeza { get; set; }
        public int QuantidadeCasasInteira { get; set; }
        public int QuantidadeCasasDecimais { get; set; }
        public bool? DestacaModificacao { get; set; }
        public string OrigemColetaNome { get; set; }
        public string ClasseTipoDado
        {
            get
            {
                return TipoDadoGrandeza.ToString();
            }
        }
    }
}