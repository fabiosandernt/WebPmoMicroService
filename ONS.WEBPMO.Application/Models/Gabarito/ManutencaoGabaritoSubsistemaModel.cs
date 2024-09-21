using System.ComponentModel.DataAnnotations;

namespace ONS.WEBPMO.Application.Models.Gabarito
{
    public class ManutencaoGabaritoSubsistemaModel : ManutencaoGabaritoModel
    {
        [Display(Name = @"Subsistema")]
        public override string NomeOrigemColeta { get; set; }
    }
}