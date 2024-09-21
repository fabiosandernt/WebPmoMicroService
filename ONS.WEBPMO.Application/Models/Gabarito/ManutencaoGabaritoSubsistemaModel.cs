namespace ONS.WEBPMO.Application.Models.Gabarito
{
    using System.ComponentModel.DataAnnotations;

    public class ManutencaoGabaritoSubsistemaModel : ManutencaoGabaritoModel
    {
        [Display(Name = @"Subsistema")]
        public override string NomeOrigemColeta { get; set; }
    }
}