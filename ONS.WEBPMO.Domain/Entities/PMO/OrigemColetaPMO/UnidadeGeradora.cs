using ONS.WEBPMO.Domain.Enumerations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO
{
    public class UnidadeGeradora : OrigemColeta
    {
        public int CodigoDPP { get; set; }
        public double PotenciaNominal { get; set; }
        public virtual Usina Usina { get; set; }
        public override TipoOrigemColetaEnum TipoOrigemColeta
        {
            get { return TipoOrigemColetaEnum.UnidadeGeradora; }
        }

        public int NumeroConjunto { get; set; }

        public int NumeroMaquina { get; set; }
        public string UsinaId { get; set; }

        [NotMapped]
        public int? NumGrUge { get; set; }
        [NotMapped]
        public int? Gruge_id { get; set; }
        [NotMapped]
        public int? Cod_subsistemamodenerg { get; set; }
        [NotMapped]
        public double? Val_potcalcindisp { get; set; }
        [NotMapped]
        public string Cod_tppotcalcindisp { get; set; }
        [NotMapped]
        public DateTime? Din_fim { get; set; }
        [NotMapped]
        public string Age_id_oper { get; set; }
    }
}
