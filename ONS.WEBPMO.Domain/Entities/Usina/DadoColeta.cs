namespace ONS.WEBPMO.Domain.Entities.Usina
{
    
    public class DadoColeta : BaseObject
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
