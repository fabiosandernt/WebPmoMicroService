using ONS.WEBPMO.Domain.Entities.Base;

namespace ONS.WEBPMO.Domain.Entities.BDT
{
    public class SubmercadoPMO : BaseEntity<string>
    {        
        public string SmeId { get; set; }
        public string NomeCurto { get; set; }
        
    }
}
