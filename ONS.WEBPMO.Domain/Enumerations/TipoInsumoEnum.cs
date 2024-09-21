
namespace ONS.WEBPMO.Domain.Enumerations
{
    using System.ComponentModel;
    [Flags]
    public enum TipoInsumoEnum
    {
        [Description("Estruturado")]
        Estruturado = 'E',

        [Description("Não Estruturado")]
        NaoEstruturado = 'L'

    }
}
