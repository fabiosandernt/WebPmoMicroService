namespace ONS.WEBPMO.Application.Models.GeracaoBlocos
{
    //[ModelBinder(typeof(EnumerableValueBinder))]
    public class PesquisaGeracaoBlocosModel : PesquisaColetaInsumoModel
    {
        public PesquisaGeracaoBlocosModel()
        {
            ArquivosDadoNaoEstruturado = new List<InsumoArquivoModel>();
            ArquivosGeracaoBlocos = new List<ArquivoModel>();
        }

        public IList<InsumoArquivoModel> ArquivosDadoNaoEstruturado { get; set; }
        public IList<ArquivoModel> ArquivosGeracaoBlocos { get; set; }
    }
}