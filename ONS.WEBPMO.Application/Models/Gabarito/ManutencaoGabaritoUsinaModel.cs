using System.ComponentModel.DataAnnotations;

namespace ONS.WEBPMO.Application.Models.Gabarito
{   

    public class ManutencaoGabaritoUsinaModel : ManutencaoGabaritoModel
    {
        [Display(Name = @"Usina")]
        public override string NomeOrigemColeta { get; set; }
    }
}