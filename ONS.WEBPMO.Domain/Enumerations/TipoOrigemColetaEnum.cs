
namespace ONS.WEBPMO.Domain.Enumerations
{
    using System.ComponentModel;
    [Flags]
    public enum TipoOrigemColetaEnum
    {
        Usina = 'U',
        Subsistema = 'S',
        [Description("Unidade Geradora")]
        UnidadeGeradora = 'G',
        [Description("Reservatório")]
        Reservatorio = 'R',
        [Description("Geração Complementar")]
        GeracaoComplementar = 'C'

    }
}
