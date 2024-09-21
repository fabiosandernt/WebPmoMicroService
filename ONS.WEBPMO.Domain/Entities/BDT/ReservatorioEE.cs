using ONS.WEBPMO.Domain.Entities.Base;

namespace ONS.WEBPMO.Domain.Entities.BDT
{
    public class ReservatorioEE : BaseEntity<string>
    {
        public string TipoOrigemColeta { get; set; }
        public string Cod_reservatorioee { get; set; }
        public string Id_reservatorioee { get; set; }
        public string Nom_curto_reservatorioee { get; set; }
    }
}
