
namespace ONS.WEBPMO.Application.Models.Insumo
{
    public class VisualizarInsumoNaoEstruturadoModel : VisualizarInsumoModel
    {
        public bool IsProcessamentoPMO { get; set; }
        public bool IsBlocoMontador { get; set; }
        public bool IsConvergenciaCCEE { get; set; }
        public bool IsPublicacaoResultados { get; set; }
    }
}