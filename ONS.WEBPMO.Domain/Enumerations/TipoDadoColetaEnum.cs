using System.ComponentModel;

namespace ONS.WEBPMO.Domain.Enumerations
{
    [Flags]
    public enum TipoDadoColetaEnum
    {
        [Description("Estruturado")]
        Estruturado = 'E',

        [Description("Não Estruturado")]
        NaoEstruturado = 'L',

        [Description("Manutenção")]
        Manutencao = 'M'
    }
}
