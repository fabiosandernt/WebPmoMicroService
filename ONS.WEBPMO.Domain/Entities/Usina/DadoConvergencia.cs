
namespace ONS.WEBPMO.Domain.Entities.Usina
{
    public class DadoConvergencia : BaseObject
    {
        public string LoginConvergenciaRepresentanteCCEE { get; set; }
        public string ObservacaoConvergenciaCCEE { get; set; }

        public virtual SemanaOperativa SemanaOperativa { get; set; }
    }
}
