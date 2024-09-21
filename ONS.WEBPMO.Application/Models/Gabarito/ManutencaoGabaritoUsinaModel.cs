namespace ONS.WEBPMO.Application.Models.Gabarito
{
    using System.ComponentModel.DataAnnotations;

    public class ManutencaoGabaritoUsinaModel : ManutencaoGabaritoModel
    {
        [Display(Name = @"Usina")]
        public override string NomeOrigemColeta { get; set; }
    }
}