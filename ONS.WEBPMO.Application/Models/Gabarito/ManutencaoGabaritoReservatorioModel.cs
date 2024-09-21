namespace ONS.WEBPMO.Application.Models.Gabarito
{
    using System.ComponentModel.DataAnnotations;

    public class ManutencaoGabaritoReservatorioModel : ManutencaoGabaritoModel
    {
        [Display(Name = @"Reservatório")]
        public override string NomeOrigemColeta { get; set; }
    }
}