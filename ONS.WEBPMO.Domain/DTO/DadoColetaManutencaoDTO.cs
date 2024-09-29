
namespace ONS.WEBPMO.Domain.DTO
{
    public class DadoColetaManutencaoDTO
    {
        public int IdDadoColeta { get; set; }
        public string NomeUsina { get; set; }
        public string NomeUnidade { get; set; }
        public string Numero { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string TempoRetorno { get; set; }
        public string Justificativa { get; set; }
        public string SituacaoColetaInsumoDescricao { get; set; }
        public string ClassificacaoPorTipoEquipamento { get; set; }
        public string Periodicidade { get; set; }
    }
}
