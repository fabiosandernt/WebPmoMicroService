using ONS.WEBPMO.Domain.Entities.Base;

namespace ONS.WEBPMO.Domain.Entities.PMO
{
    public class HistoricoColetaInsumo : BaseEntity<int>
    {
        public DateTime DataHoraAlteracao { get; set; }
        public virtual ColetaInsumo ColetaInsumo { get; set; }
        public virtual SituacaoColetaInsumo Situacao { get; set; }
        public int? ColetaInsumoId { get; set; }
        public int? SituacaoId { get; set; }
    }
}
