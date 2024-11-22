
using ONS.WEBPMO.Domain.Entities.Base;

namespace ONS.WEBPMO.Domain.Entities.PMO
{
    public class Arquivo : BaseEntity<Guid>, ILogicalDeletion
    {
        public virtual BinaryData Content { get; set; }
        public string Nome { get; set; }
        public string MimeType { get; set; }
        public string HashVerificacao { get; set; }
        public int Tamanho { get; set; }
        public bool Deleted { get; set; }
    }
}