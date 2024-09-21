using ONS.WEBPMO.Domain.Entities.Base;

namespace ONS.WEBPMO.Domain.Entities.BDT
{
    public class UsinaPEM : BaseEntity<string>
    {        
        //############################################################
        //Dados de USINA
        //############################################################
        public string NomeCurto { get; set; } //usi.nomecurto
        public string NomeLongo { get; set; } //usi.nomelongo
        public string IdoUsina { get; set; } //usi.ido_usi
        public string IdUsina { get; set; } //usi.usi_id
        public string TipoUsina { get; set; } //usi.tpusina_id
        public Double? PotenciaInstalada { get; set; } //potencia.pot
        public string IdSubsistema { get; set; } //ins.id_subsistema
        public string CodAneel { get; set; } //usi.cod_aneel
        public string CodModalidadeOperUsiConj { get; set; } //tb_modalidadeoperusiconj.cod_modalidadeoperusiconj
        public DateTime? DataEntradaUsina { get; set; } //usi.dtentrada
        public DateTime? DataPrevistaUsina { get; set; } //usi.dtprevista 
        public DateTime? DataDesativacaoUsina { get; set; } //usi.dtdesativa
        public DateTime? DataEntradaCom { get; set; } //usi.dtentcom
        public Double? ValorPotenciaMinima { get; set; } //usi.val_potmin

        //############################################################
        //Dados do Ponto de Conexão GENÉRICO
        //############################################################
        public string IdPontoDeConexao { get; set; } //tb_niveltensao.id_niveltensao ou ntc.id_niveltensao
        public string IdoOnsPontoDeConexao { get; set; } //tb_niveltensao.ido_ons ou ntc.ido_ons
        public string NomeCurtoPontoDeConexao { get; set; } //tb_niveltensao.nom_curto ou ntc.nom_curto

        //############################################################
        //Dados INSTALAÇÃO
        //############################################################
        public Double? ValorLatitude { get; set; } //ins.val_latitude
        public Double? ValorLongitude { get; set; } //ins.val_longitude
        public string IdEstado { get; set; } //ins.estad_id
        public string IdSme { get; set; } //ins.sme_id
        public string IdCos { get; set; } //ins.cos_id
        public string ConjuntoUsina { get; set; } //tb_conjuntousina.cod_conjuntousina

    }
}
