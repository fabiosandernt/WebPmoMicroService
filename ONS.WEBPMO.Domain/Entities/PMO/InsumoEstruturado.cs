
using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Domain.Entities.PMO
{
    public class InsumoEstruturado : Insumo
    {

        public InsumoEstruturado()
        {
            Grandezas = new List<Grandeza>();
            TipoInsumo = TipoInsumoEnum.Estruturado.ToString();
        }

        public int? QuantidadeMesesAdiante { get; set; }
        public string TipoBloco { get; set; }
        public virtual CategoriaInsumo CategoriaInsumo { get; set; }
        public virtual TipoColeta TipoColeta { get; set; }
        public virtual IList<Grandeza> Grandezas { get; set; }
        public int? OrdemBlocoMontador { get; set; }
        public int CategoriaInsumoId { get; set; }
        public int? TipoColetaId { get; set; }
    }
}
