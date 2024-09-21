
using ONS.WEBPMO.Domain.Entities.Base;

namespace ONS.WEBPMO.Domain.Entities.BDT
{
    public class ReservatorioPMO : BaseEntity<string>
    {
        public int Codigo { get; set; }
        public string NomeLongo { get; set; }
        public string NomeCurto { get; set; }
        public string NomeExibicao { get; set; }
        public string SiglaSubsistema { get; set; }
        public int QuantidadeSubsistema { get; set; }
        public int? Cod_subsistemamodenerg { get; set; } //tb_subsistema.cod_subsistemamodenerg
        public string Cod_reservatorioee { get; set; } //tb_reservatorioee.cod_reservatorioee
        public string Nom_curto_reservatorioee { get; set; } //tb_reservatorioee.nom_curto        
        public string CodUsiPlanejamentoJusante { get; set; } //uge.cod_usiplanejamento
    }
}
