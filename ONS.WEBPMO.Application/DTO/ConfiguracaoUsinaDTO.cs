namespace ONS.WEBPMO.Application.DTO
{
    using System;

    //Campos nas tabelas: [tb_aux_usina]  u; [tb_agenteinstituicao] a;
    //a.dsc_razaosocial,
    //u.nom_longo,
    //u.cod_dpp,
    //u.cod_tpgeracao
    public class ConfiguracaoUsinaDTO : ConfiguracaoDTOBase
    {
        public String CodDPP { get; set; }
        public String TipoGeracao { get; set; }
    }
}
