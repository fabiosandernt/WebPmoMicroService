
using ONS.WEBPMO.Domain.Enumerations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO
{    
    public class Usina : OrigemColeta, IConjuntoGerador
    {
        public Usina()
        {
            UnidadesGeradoras = new List<UnidadeGeradora>();
        }

        public virtual string TipoUsina { get; set; }

        public virtual Subsistema Subsistema { get; set; }

        public override TipoOrigemColetaEnum TipoOrigemColeta
        {
            get { return TipoOrigemColetaEnum.Usina; }
        }

        public virtual IList<UnidadeGeradora> UnidadesGeradoras { get; set; }

        public String NomeLongo { get; set; }

        public String NomeCurto { get; set; }

        public int CodigoDPP { get; set; }

        public String IdSubsistema { get; set; }

        [NotMapped]
        public int? Cod_subsistemamodenerg { get; set; } //tb_subsistema.cod_subsistemamodenerg

        [NotMapped]
        public string Id_reservatorioee { get; set; } //tb_reservatorioee.id_reservatorioee

        [NotMapped]
        public string Cod_reservatorioee { get; set; } //tb_reservatorioee.cod_reservatorioee

        [NotMapped]
        public string Nom_curto_reservatorioee { get; set; } //tb_reservatorioee.nom_curto

        [NotMapped]
        //public string CodDPPUsiJusante { get; set; } //res.usijusante
        public int? CodUsiPlanejamentoJusante { get; set; } //uge.cod_usiplanejamento

        [NotMapped]
        public string IdUsina { get; set; } //tb_usi.usi_id

        [NotMapped]
        public string NomeCurtoSubmercado { get; set; } //sme.nomecurto

        [NotMapped]
        public string CodSubmercado { get; set; } //sme.sme_id

        [NotMapped]
        public string CodigoTipoGeracao { get; set; } //usi.tpusina_id
    }
}
