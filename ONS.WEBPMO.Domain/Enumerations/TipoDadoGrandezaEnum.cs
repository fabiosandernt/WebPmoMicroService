using System.ComponentModel;

namespace ONS.WEBPMO.Domain.Enumerations
{
    [Flags]
    public enum TipoDadoGrandezaEnum
    {
        [Description("Numérico")]
        Numerico = 1,
        [Description("Texto")]
        Texto,
        [Description("Data")]
        Data,
        [Description("Manutenção")]
        Manutencao
    }
}
