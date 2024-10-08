
namespace ONS.WEBPMO.Domain.Entities.PMO
{
    public class SituacaoColetaInsumo : IdDescricaoBaseEntity
    {
        public IList<ColetaInsumo> ColetaInsumos = new List<ColetaInsumo>();
    }
}
