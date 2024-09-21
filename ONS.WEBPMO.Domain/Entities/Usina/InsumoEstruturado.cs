
namespace ONS.WEBPMO.Domain.Entities.Usina
{

    public class InsumoEstruturado : Insumo
    {

        public InsumoEstruturado()
        {
            Grandezas = new List<Grandeza>();
        }

        public int? QuantidadeMesesAdiante { get; set; }
        public string TipoBloco { get; set; }
        public virtual string CategoriaInsumo { get; set; }
        public virtual string TipoColeta { get; set; }
        public virtual IList<Grandeza> Grandezas { get; set; }
        public int? OrdemBlocoMontador { get; set; }
        public int CategoriaInsumoId { get; set; }
        public int? TipoColetaId { get; set; }
    }
}
