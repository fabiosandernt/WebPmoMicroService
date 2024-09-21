using ONS.WEBPMO.Domain.Entities.Base;

namespace ONS.WEBPMO.Domain.Entities.BDT
{
    public class UsinaPMO : BaseEntity<string>
    {        
        public int CodUsinaPlanejamento { get; set; }
        public string NomeLongo { get; set; }
        public string NomeCurto { get; set; }
        public string NomeExibicao { get; set; }
        public string TipoGeracao { get; set; }
        public string SiglaSubsistema { get; set; }
        public int QuantidadeSubsistema { get; set; }
        public int? Cod_subsistemamodenerg { get; set; } //tb_subsistema.cod_subsistemamodenerg
        public string Id_reservatorioee { get; set; } //tb_reservatorioee.id_reservatorioee
        public string Cod_reservatorioee { get; set; } //tb_reservatorioee.cod_reservatorioee
        public string Nom_curto_reservatorioee { get; set; } //tb_reservatorioee.nom_curto        
        public int? CodUsiPlanejamentoJusante { get; set; } //uge.cod_usiplanejamento
        //public string CodDPPUsiJusante { get; set; } //res.usijusante
        public string IdUsina { get; set; } //usi.usi_id
        public string NomeCurtoSubmercado { get; set; } //sme.nomecurto
        public string CodSubmercado { get; set; } //sme.sme_id
        public string CodigoTipoGeracao { get; set; } //usi.tpusina_id

    }
}
