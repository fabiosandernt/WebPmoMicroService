

namespace ONS.WEBPMO.Application.Models.LogInformarDados
{
    //[ModelBinder(typeof(EnumerableValueBinder))]
    public class PesquisaLogInformarDadosModel
    {
        public PesquisaLogInformarDadosModel()
        {
            Nome = "";
            Empresa = "";
            DataInicioAbrangencia = DateTime.MinValue;
            DataFimAbrangencia = DateTime.Now;
        }

        public string Nome { get; set; }
        public string Empresa { get; set; }
        public DateTime DataInicioAbrangencia { get; set; }
        public DateTime DataFimAbrangencia { get; set; }
    }
}