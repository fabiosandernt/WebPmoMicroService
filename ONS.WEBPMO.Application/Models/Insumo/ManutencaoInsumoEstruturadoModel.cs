using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace ONS.WEBPMO.Application.Models.Insumo
{
    public class ManutencaoInsumoEstruturadoModel : InclusaoInsumoModelBase
    {
        public ManutencaoInsumoEstruturadoModel()
        {
            Categorias = new List<SelectListItem>();
            TiposColeta = new List<SelectListItem>();
            Grandezas = new List<ManutencaoGrandezaModel>();
        }

        public string VersaoStringInsumo { get; set; }

        public bool PermiteAlteracao { get; set; }

        [Display(Name = @"Categoria")]
        public int CategoriaId { get; set; }
        public IList<SelectListItem> Categorias { get; set; }
        public string CategoriaInsumoDescricao { get; set; }

        [Display(Name = @"Tipo de Coleta")]
        public int TipoColetaId { get; set; }
        public IList<SelectListItem> TiposColeta { get; set; }

        public IList<ManutencaoGrandezaModel> Grandezas { get; set; }

    }
}