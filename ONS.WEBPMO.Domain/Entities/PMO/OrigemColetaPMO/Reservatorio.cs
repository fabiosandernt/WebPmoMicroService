using ONS.WEBPMO.Domain.Enumerations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO
{
    public class Reservatorio : OrigemColeta, IConjuntoGerador
    {
        public virtual Subsistema Subsistema { get; set; }

        public int CodigoDPP { get; set; }
        
        public String NomeLongo { get; set; }

        public String NomeCurto { get; set; }

        public String IdSubsistema { get; set; }

        public override TipoOrigemColetaEnum TipoOrigemColeta
        {
            get { return TipoOrigemColetaEnum.Reservatorio; }
        }
        [NotMapped]
        public int? Cod_subsistemamodenerg { get; set; } //tb_subsistema.cod_subsistemamodenerg

        [NotMapped]
        public string Cod_reservatorioee { get; set; } //tb_reservatorioee.cod_reservatorioee

        [NotMapped]
        public string Nom_curto_reservatorioee { get; set; } //tb_reservatorioee.nom_curto        

        [NotMapped]
        public string CodUsiPlanejamentoJusante { get; set; } //uge.cod_usiplanejamento
    }
}
