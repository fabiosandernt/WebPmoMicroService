namespace ONS.WEBPMO.Domain.Entities.Usina.OrigemColeta
{
        public class Usina : OrigemColeta
    {
        
        public virtual string TipoUsina { get; set; }

        public virtual Subsistema Subsistema { get; set; }        

        public virtual IList<UnidadeGeradora> UnidadesGeradoras { get; set; }

        public String NomeLongo { get; set; }

        public String NomeCurto { get; set; }

        public int CodigoDPP { get; set; }

        public String IdSubsistema { get; set; }

        public int? Cod_subsistemamodenerg { get; set; } //tb_subsistema.cod_subsistemamodenerg

        public string Id_reservatorioee { get; set; } //tb_reservatorioee.id_reservatorioee

        public string Cod_reservatorioee { get; set; } //tb_reservatorioee.cod_reservatorioee

        public string Nom_curto_reservatorioee { get; set; } //tb_reservatorioee.nom_curto

        //public string CodDPPUsiJusante { get; set; } //res.usijusante
        public int? CodUsiPlanejamentoJusante { get; set; } //uge.cod_usiplanejamento

        public string IdUsina { get; set; } //tb_usi.usi_id

        public string NomeCurtoSubmercado { get; set; } //sme.nomecurto

        public string CodSubmercado { get; set; } //sme.sme_id

        public string CodigoTipoGeracao { get; set; } //usi.tpusina_id

    }
}
