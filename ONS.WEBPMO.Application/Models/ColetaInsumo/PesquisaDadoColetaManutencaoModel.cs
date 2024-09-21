namespace ONS.WEBPMO.Application.Models.ColetaInsumo
{
    public class PesquisaDadoColetaManutencaoModel : DadoColetaManutencaoModel
    {
        public int IdDadoColeta { get; set; }
        public string NomeUsina { get; set; }
        public string NomeUnidade { get; set; }

        public string SituacaoColetaInsumoDescricao { get; set; }

        public string ClassificacaoPorTipoEquipamento { get; set; }
        public string Periodicidade { get; set; }

    }
}