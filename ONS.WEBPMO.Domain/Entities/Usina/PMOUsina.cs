
namespace ONS.WEBPMO.Domain.Entities.Usina
{
    public partial class PMOUsina : BaseObject
    {

        public PMOUsina()
        {
            this.SemanasOperativas = new SortedSet<SemanaOperativa>();
        }

        public int AnoReferencia { get; set; }
        public int MesReferencia { get; set; }
        public int? QuantidadeMesesAdiante { get; set; }
        public byte[] Versao { get; set; }

        public object Version
        {
            get
            {
                return Versao;
            }
        }

        public virtual ISet<SemanaOperativa> SemanasOperativas { get; set; }
    }
}
