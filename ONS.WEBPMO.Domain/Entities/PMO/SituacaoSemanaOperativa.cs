namespace ONS.WEBPMO.Domain.Entities.PMO
{
    public partial class SituacaoSemanaOperativa 
    {
        public int IdTpsituacaosemanaoper { get; set; }

        public string DscSituacaosemanaoper { get; set; }

        public virtual ICollection<ArquivoSemanaOperativa> ArquivosSemanaOperativas { get; set; } = new List<ArquivoSemanaOperativa>();

        public virtual ICollection<HistoricoSemanaOperativa> TbHistmodifsemanaopers { get; set; } = new List<HistoricoSemanaOperativa>();

        public virtual ICollection<SemanaOperativa> TbSemanaoperativas { get; set; } = new List<SemanaOperativa>();
    }
}
