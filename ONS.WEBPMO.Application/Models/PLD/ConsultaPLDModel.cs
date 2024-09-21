using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ONS.WEBPMO.Application.Models.PLD
{
    public class ConsultaPLDModel
    {

        public ConsultaPLDModel()
        {
            SemanasOperativas = new List<SelectListItem>();
        }

        [Required]
        [Display(Name = @"Estudo")]
        public int? IdSemanaOperativa { get; set; }

        public IList<SelectListItem> SemanasOperativas { get; set; }

    }
}