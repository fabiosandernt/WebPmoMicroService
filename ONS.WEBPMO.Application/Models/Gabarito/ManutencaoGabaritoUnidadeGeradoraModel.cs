
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ONS.WEBPMO.Application.Models.Gabarito
{
    public class ManutencaoGabaritoUnidadeGeradoraModel : ManutencaoGabaritoModel
    {
        [Display(Name = @"Usina")]
        public override string NomeOrigemColeta { get; set; }

        [Display(Name = @"Unidades Geradoras")]
        [Required]
        public IList<string> IdsOrigemColeta { get; set; }
        public IList<SelectListItem> OrigensColeta { get; set; }
        public IList<SelectListItem> OrigensColetaGabarito { get; set; }
    }
}