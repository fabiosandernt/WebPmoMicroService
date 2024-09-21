using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ONS.WEBPMO.Application.Models.Gabarito
{
    //[ModelBinder(typeof(ConfiguracaoGabaritoModelBinder))]
    public class ConfiguracaoGabaritoUnidadeGeradoraModel : BaseGabaritoModel
    {
        public ConfiguracaoGabaritoUnidadeGeradoraModel()
        {
            IdsOrigemColeta = new List<string>();
            OrigensColeta = new List<SelectListItem>();
            OrigensColetaGabarito = new List<SelectListItem>();
            Usinas = new List<SelectListItem>();
        }

        [Required]
        [Display(Name = @"Seleção de Insumo(s)")]
        public override IList<int> IdsInsumo { get; set; }

        [Display(Name = @"Usina")]
        [Required]
        public string IdUsina { get; set; }
        public IList<SelectListItem> Usinas { get; set; }

        [Display(Name = @"Unidades Geradoras")]
        [Required]
        public IList<string> IdsOrigemColeta { get; set; }
        public IList<SelectListItem> OrigensColeta { get; set; }
        public IList<SelectListItem> OrigensColetaGabarito { get; set; }
    }
}