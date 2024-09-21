using System.ComponentModel;

namespace ONS.WEBPMO.Domain.Enumerations
{
    [Flags] 
    public enum TipoUsinaEnum
    {
        [Description("Hidráulica")]
        Hidraulica = 1,
        [Description("Térmica")]
        Termica
    }
}
