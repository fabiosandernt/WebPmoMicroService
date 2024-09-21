using ONS.WEBPMO.Domain.Entities.Base;

namespace ONS.WEBPMO.Domain.Entities.BDT
{
    public class InstanteVolumeReservatorio : BaseEntity<string>
    {

        public string OrigemId { get; set; }

        public string PeriodoId { get; set; }

        public string TipoGrandezaId { get; set; }

        public DateTime Instante { get; set; }

        public double Valor { get; set; }
    }
}
