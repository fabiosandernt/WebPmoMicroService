using System.ComponentModel;

namespace ONS.WEBPMO.Domain.Enumerations
{   
    [Flags] 
    public enum AcaoLogNotificacaoEnum
    {
        Abertura = 1,

        Reabertura = 2,

        [Description("Rejeição")]
        Rejeicao = 3
    }
}
