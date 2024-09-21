using ONS.WEBPMO.Domain.Entities.Base;

namespace ONS.WEBPMO.Domain.Entities.BDT
{
    public class SubsistemaPMO : BaseEntity<string>
    {
        public string NomeLongo { get; set; }
        public string NomeCurto { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime? DataDesativacao { get; set; }
        public int CodigoModeloEnergia { get; set; }
    }
}
