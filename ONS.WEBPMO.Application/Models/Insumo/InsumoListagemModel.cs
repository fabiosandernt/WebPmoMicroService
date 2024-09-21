
namespace ONS.WEBPMO.Application.Models.Insumo
{
    public class InsumoListagemModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public short OrdemExibicao { get; set; }
        public string PreAprovado { get; set; }
        public string NomeTipoInsumo { get; set; }
        public string ValorTipoInsumo { get; set; }
        public string VersaoStringInsumo { get; set; }
        public string SiglaInsumo { get; set; }
        public string ExportarInsumo { get; set; }
        public string Ativo { get; set; }
    }
}