
namespace ONS.WEBPMO.Domain.Entities.Usina
{   
    public class Arquivo : BaseObject
    {
        public string Nome { get; set; }
        public string MimeType { get; set; }
        public string HashVerificacao { get; set; }
        public int Tamanho { get; set; }
        public bool Deleted { get; set; }
    }
}
