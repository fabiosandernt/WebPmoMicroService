using ONS.WEBPMO.Domain.Entities.Base;

namespace ONS.WEBPMO.Domain.Entities.PMO
{
    public class SemanaOperativa : BaseEntity<int>, IComparable<SemanaOperativa>, IConcurrencyEntity
    {
        public SemanaOperativa()
        {
            ColetasInsumos = new List<ColetaInsumo>();
            Gabaritos = new List<Gabarito>();
            Arquivos = new List<ArquivoSemanaOperativa>();
        }

        public string Nome { get; set; }
        public DateTime DataInicioSemana { get; set; }
        public DateTime DataFimSemana { get; set; }
        public DateTime DataReuniao { get; set; }
        public DateTime DataInicioManutencao { get; set; }
        public DateTime DataFimManutencao { get; set; }
        public DateTime? DataHoraAtualizacao { get; set; }

        public int Revisao { get; set; }
        public byte[] Versao { get; set; }

        public int? SituacaoId { get; set; }
        public virtual SituacaoSemanaOperativa Situacao { get; set; }
        public virtual IList<ColetaInsumo> ColetasInsumos { get; set; }
        public virtual IList<Gabarito> Gabaritos { get; set; }

        public int PMOId { get; set; }
        public virtual PMO PMO { get; set; }
        public virtual IList<ArquivoSemanaOperativa> Arquivos { get; set; }

        public virtual DadoConvergencia DadoConvergencia { get; set; }

        public int CompareTo(SemanaOperativa other)
        {
            return DataInicioSemana.CompareTo(other.DataInicioSemana);
        }

        public object Version
        {
            get
            {
                return Versao;
            }
        }
    }
}
