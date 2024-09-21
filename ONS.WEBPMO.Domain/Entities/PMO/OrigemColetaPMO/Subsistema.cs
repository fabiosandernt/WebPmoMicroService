using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO
{
    public class Subsistema : OrigemColeta
    {
        public String Codigo { get; set; }
        public override TipoOrigemColetaEnum TipoOrigemColeta
        {
            get { return TipoOrigemColetaEnum.Subsistema; }
        }
    }
}
