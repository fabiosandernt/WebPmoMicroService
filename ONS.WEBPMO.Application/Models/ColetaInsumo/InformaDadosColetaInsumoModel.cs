namespace ONS.WEBPMO.Application.Models.ColetaInsumo
{
    public class InformaDadosColetaInsumoModel : FiltroPesquisaColetaInsumoModel
    {
        public int IdColetaInsumo { get; set; }
        public string VersaoColetaInsumo { get; set; }
        public int IdSituacaoColeta { get; set; }
    }
}