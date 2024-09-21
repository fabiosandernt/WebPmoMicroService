
namespace ONS.WEBPMO.Application.Models.ColetaInsumo
{
    //[ModelBinder(typeof(EnumerableValueBinder))]
    public class EnviarDadosColetaInsumoModel : FiltroPesquisaColetaInsumoModel
    {
        public EnviarDadosColetaInsumoModel()
        {
            IdsColetaInsumo = new List<int>();
            VersoesString = new List<string>();
        }

        public IList<int> IdsColetaInsumo { get; set; }

        public IList<string> VersoesString { get; set; }
    }
}