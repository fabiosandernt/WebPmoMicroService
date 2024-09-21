

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ONS.WEBPMO.Application.Models.Gabarito
{
    public class ConfiguracaoGabaritoReservatorioModel : BaseGabaritoModel
    {
        public ConfiguracaoGabaritoReservatorioModel()
        {
            OrigensColeta = new List<SelectListItem>();
        }

        [Required]
        [Display(Name = @"Seleção de Insumo(s)")]
        public override IList<int> IdsInsumo { get; set; }

        [Display(Name = @"Reservatório")]
        [Required]
        public string IdOrigemColeta { get; set; }
        public IList<SelectListItem> OrigensColeta { get; set; }
    }
}