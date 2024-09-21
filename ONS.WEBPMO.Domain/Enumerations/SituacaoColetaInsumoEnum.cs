using System.ComponentModel;

namespace ONS.WEBPMO.Domain.Enumerations
{
    [Flags] 
    public enum SituacaoColetaInsumoEnum
    {
        [Description("Não Iniciado")]
        NaoIniciado = 1,
        [Description("Em Andamento")]
        EmAndamento,
        Informado,
        Capturado,
        Aprovado,
        Rejeitado,
        [Description("Pré-Aprovado")]
        PreAprovado
    }
}
