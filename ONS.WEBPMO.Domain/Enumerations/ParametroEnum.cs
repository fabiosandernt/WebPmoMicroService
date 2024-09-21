using System.ComponentModel;

namespace ONS.WEBPMO.Domain.Enumerations
{
    [Flags]
    public enum ParametroEnum
    {
        [Description("CODIGO_AGENTE_ONS")]
        CodigoAgenteONS = 1,

        [Description("DIA_REUNIAO_PMO")]
        DiaReuniaoPMO,

        [Description("DIA_REUNIAO_REVISAO")]
        DiaReuniaoRevisao,

        [Description("MENSAGEM_ABERTURA_COLETA")]
        MensagemAberturaColeta,

        [Description("MENSAGEM_REJEICAO_COLETA")]
        MensagemRejeicaoColeta,

        [Description("NOME_PERFIL_GMC_1")]
        NomePerfilGmc1,

        [Description("NOME_PERFIL_GPD_1")]
        NomePerfilGPD1,

        [Description("NOME_PERFIL_GPD_2")]
        NomePerfilGPD2,

        [Description("NOME_PERFIL_GPD_3")]
        NomePerfilGPD3,

        [Description("NOME_PERFIL_GPO_1")]
        NomePerfilGPO1,

        [Description("QTD_MAX_DECIMAIS_GRANDEZA")]
        QuantidadeMaximaDecimaisGrandeza,

        [Description("QTD_MAX_DIGITOS_GRANDEZA")]
        QuatidadeMaximaDigitosGrandeza,

        [Description("QTD_MESES_A_FRENTE")]
        QuantidadeMesesAFrente,

        [Description("MENSAGEM_INI_CONVERG_CCEE")]
        MensagemNotificacaoConvergenciaCcee,

        [Description("ACRESCIMO_RESTRICAO_ELETRICA_TERMICA")]
        AcrescimoRestricaoEletricaTermica
    }
}
