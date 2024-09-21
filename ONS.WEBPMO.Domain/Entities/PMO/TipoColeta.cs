
namespace ONS.WEBPMO.Domain.Entities.PMO
{
    public class TipoColeta : IdDescricaoBaseEntity
    {
        public bool UsoPmo { get; set; }
        public bool BlocoMontador { get; set; }
        public bool MnemonicoMontador { get; set; }
    }
}
