using System.ComponentModel;

namespace ONS.WEBPMO.Domain.Enumerations
{
    [Flags]
    public enum TipoInsumoDisponibilidadeEnum
    {
        /* comentado devido a replicacao de codigo da branche sprint18_Web-PMO_Bug-76601 */

        [Description("Disponibilidade, Inflexibilidade e Custo de Usinas Térmicas")]
        Termicas = 142,
        [Description("Disponibilidade, Inflexibilidade e Custo de Térmicas GNL")]
        Termicas_GNL = 143,
        [Description("Disponibilidade, Inflexibilidade e Custo de Usinas Térmicas - Eletrobras")]
        Eletrobras = 150
    }
}
