namespace ONS.WEBPMO.Domain.Entities.Usina
{
    public class ArquivoSemanaOperativa : BaseObject
    {
        public virtual Arquivo Arquivo { get; set; }
        public virtual SemanaOperativa SemanaOperativa { get; set; }
        public virtual string Situacao { get; set; }
        public bool IsPublicado { get; set; }
        public string ArquivoId { get; set; }
        public int SemanaOperativaId { get; set; }
        public int SituacaoId { get; set; }
    }
}
