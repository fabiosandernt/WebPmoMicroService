
namespace ONS.WEBPMO.Application.Models.Insumo
{
    public class VisualizarInsumoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public short? OrdemExibicao { get; set; }
        public string PreAprovado { get; set; }
        public string Reservado { get; set; }
        public string TipoInsumo { get; set; }
        public string SiglaInsumo { get; set; }
        public string ExportarInsumo { get; set; }
        public string Ativo { get; set; }
    }
}