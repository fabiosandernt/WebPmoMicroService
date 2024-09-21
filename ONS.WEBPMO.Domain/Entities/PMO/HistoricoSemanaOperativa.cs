using ONS.WEBPMO.Domain.Entities.Base;

namespace ONS.WEBPMO.Domain.Entities.PMO
{
    public class HistoricoSemanaOperativa : BaseEntity<int>
    {
        public DateTime DataHoraAlteracao { get; set; }
        public virtual SemanaOperativa SemanaOperativa { get; set; }
        public virtual SituacaoSemanaOperativa Situacao { get; set; }
        public int SemanaOperativaId { get; set; }
        public int? SituacaoId { get; set; }
    }
}
