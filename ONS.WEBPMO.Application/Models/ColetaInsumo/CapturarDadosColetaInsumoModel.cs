
namespace ONS.WEBPMO.Application.Models.ColetaInsumo
{
    [ModelBinder(typeof(EnumerableValueBinder))]
    public class CapturarDadosColetaInsumoModel : FiltroPesquisaColetaInsumoModel
    {
        public CapturarDadosColetaInsumoModel()
        {
            IdsColetaInsumo = new List<int>();
            VersoesColetaInsumoString = new List<string>();
        }

        public IList<int> IdsColetaInsumo { get; set; }
        public IList<string> VersoesColetaInsumoString { get; set; }
    }

}