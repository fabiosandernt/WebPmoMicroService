
using ONS.WEBPMO.Domain.Entities.Base;

namespace ONS.WEBPMO.Domain.Entities.PMO
{   
    public class LogDadosInformados : BaseEntity<int>
    {
        public int? Id_agenteinstituicao { get; set; }
        public int Id_semanaoperativa { get; set; }
        public string Nom_usuario { get; set; }
        public string Mail_usuario { get; set; }
        public string Dsc_acao { get; set; }
        public DateTime Din_acao { get; set; }
        public virtual Agente Agente { get; set; }
    }
}
