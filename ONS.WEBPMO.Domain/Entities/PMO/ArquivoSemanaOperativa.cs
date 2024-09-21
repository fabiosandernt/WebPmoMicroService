
using ONS.WEBPMO.Domain.Entities.Base;

namespace ONS.WEBPMO.Domain.Entities.PMO
{
    public class ArquivoSemanaOperativa : BaseEntity<int>
    {
        public virtual Arquivo Arquivo { get; set; }
        public virtual SemanaOperativa SemanaOperativa { get; set; }
        public virtual SituacaoSemanaOperativa Situacao { get; set; }
        public bool IsPublicado { get; set; }
        public Guid ArquivoId { get; set; }
        public int SemanaOperativaId { get; set; }
        public int SituacaoId { get; set; }
    }
}
