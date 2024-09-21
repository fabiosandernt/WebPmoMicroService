using ONS.SGIPMO.WebSite.Binders;

namespace ONS.WEBPMO.Application.Models.Gabarito
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    

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