namespace ONS.WEBPMO.Application.Models.Gabarito
{
    using Common.Resources;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    

    public class ConfiguracaoGabaritoSubsistemaModel : BaseGabaritoModel
    {
        public ConfiguracaoGabaritoSubsistemaModel()
        {
            OrigensColeta = new List<SelectListItem>();
        }

        [Required]
        [Display(Name = @"Seleção de Insumo(s)")]
        public override IList<int> IdsInsumo { get; set; }

        [Display(Name = @"Subsistema")]
        [Required]
        public string IdOrigemColeta { get; set; }
        public IList<SelectListItem> OrigensColeta { get; set; }
    }
}