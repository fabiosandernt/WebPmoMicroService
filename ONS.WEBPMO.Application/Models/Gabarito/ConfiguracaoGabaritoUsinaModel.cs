namespace ONS.WEBPMO.Application.Models.Gabarito
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    

    public class ConfiguracaoGabaritoUsinaModel : BaseGabaritoModel
    {
        public ConfiguracaoGabaritoUsinaModel()
        {
            OrigensColeta = new List<SelectListItem>();
        }

        [Required]
        [Display(Name = @"Seleção de Insumo(s)")]
        public override IList<int> IdsInsumo { get; set; }

        [Display(Name = @"Usina")]
        [Required]
        public string IdOrigemColeta { get; set; }
        public IList<SelectListItem> OrigensColeta { get; set; }
    }
}