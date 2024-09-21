using ONS.WEBPMO.Domain.Entities.Base;

namespace ONS.WEBPMO.Domain.Entities.BDT
{
    public class UnidadeGeradoraPMO : BaseEntity<string>
    {        
        public int? IdDPP { get; set; }
        public string UsiBDTId { get; set; }
        public string Nome { get; set; }
        public string NumeroConjunto { get; set; }
        public string NumeroMaquina { get; set; }
        public double PotenciaNominal { get; set; }
        public UsinaPMO UsinaPMO { get; set; }
        public int CodUsinaPlanejamento { get; set; }
        public string TipoGeracao { get; set; }
        public int? NumGrUge { get; set; }
        public int? Gruge_id { get; set; }
        public int? Cod_subsistemamodenerg { get; set; }
        public double? Val_potcalcindisp { get; set; }
        public string Cod_tppotcalcindisp { get; set; }
        public DateTime? Din_fim { get; set; }
        public string Age_id_oper { get; set; }

    }
}