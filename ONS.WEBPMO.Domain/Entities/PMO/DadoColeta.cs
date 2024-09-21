using ONS.WEBPMO.Domain.Entities.Base;

namespace ONS.WEBPMO.Domain.Entities.PMO
{
    
    public class DadoColeta : BaseEntity<int>
    {
        public string TipoDadoColeta { get; set; }
        public virtual ColetaInsumo ColetaInsumo { get; set; }
        public virtual Gabarito Gabarito { get; set; }
        public virtual Grandeza Grandeza { get; set; }
        public int ColetaInsumoId { get; set; }
        public int GabaritoId { get; set; }
        public int? GrandezaId { get; set; }
    }
}
