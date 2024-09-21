using System.ComponentModel;

namespace ONS.WEBPMO.Domain.Enumerations
{
    [Flags] 
    public enum SituacaoSemanaOperativaEnum
    {
        [Description("Configuração")]
        Configuracao = 1,
        [Description("Coleta de dados")]
        ColetaDados,
        [Description("Geração de blocos")]
        GeracaoBlocos,
        [Description("Convergência CCEE")]
        ConvergenciaCCEE,
        [Description("Publicado")]
        Publicado,
        [Description("Convergência CCEE")]
        PLDConvergido
    }
}
