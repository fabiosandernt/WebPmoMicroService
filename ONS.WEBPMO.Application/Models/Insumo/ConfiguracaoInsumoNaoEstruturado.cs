
using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Application.Models.Insumo
{
    public class ConfiguracaoInsumoNaoEstruturado : InclusaoInsumoModelBase
    {
        public ConfiguracaoInsumoNaoEstruturado()
        {
            TipoInsumo = TipoInsumoEnum.NaoEstruturado;
        }

        public bool IsProcessamentoPMO { get; set; }
        public bool IsBlocoMontador { get; set; }
        public bool IsConvergenciaCCEE { get; set; }
        public bool IsPublicacaoResultados { get; set; }
        public string VersaoInsumoString { get; set; }
    }
}