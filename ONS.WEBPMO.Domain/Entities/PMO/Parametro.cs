using ONS.WEBPMO.Domain.Entities.Base;

namespace ONS.WEBPMO.Domain.Entities.PMO
{
    public class Parametro : BaseEntity<int>
    {
        public string Nome { get; set; }
        public string Valor { get; set; }
        public string Descricao { get; set; }
    }
}
