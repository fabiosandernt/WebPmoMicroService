
using ONS.WEBPMO.Domain.Entities.Base;

namespace ONS.WEBPMO.Domain.Entities.PMO
{
    public class DadoConvergencia : BaseEntity<int>
    {
        public string LoginConvergenciaRepresentanteCCEE { get; set; }
        public string ObservacaoConvergenciaCCEE { get; set; }

        public virtual SemanaOperativa SemanaOperativa { get; set; }
    }
}
