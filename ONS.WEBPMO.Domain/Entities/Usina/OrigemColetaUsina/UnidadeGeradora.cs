namespace ONS.WEBPMO.Domain.Entities.Usina.OrigemColetaUsina
{  
    public class UnidadeGeradora : OrigemColeta
    {
        
        public int CodigoDPP { get; set; }
        public double PotenciaNominal { get; set; }
        public virtual Usina Usina { get; set; }
        public int NumeroConjunto { get; set; }

        public int NumeroMaquina { get; set; }
        public string UsinaId { get; set; }
        
        public int? NumGrUge { get; set; }        
        public int? Gruge_id { get; set; }
        public int? Cod_subsistemamodenerg { get; set; }
        public double? Val_potcalcindisp { get; set; }
        public string Cod_tppotcalcindisp { get; set; }
        public DateTime? Din_fim { get; set; }
        public string Age_id_oper { get; set; }
        
    }
}
