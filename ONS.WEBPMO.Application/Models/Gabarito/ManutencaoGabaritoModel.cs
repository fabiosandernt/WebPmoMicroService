namespace ONS.WEBPMO.Application.Models.Gabarito
{
    using System.ComponentModel.DataAnnotations;

    public class ManutencaoGabaritoModel : BaseGabaritoModel
    {
        public string IdOrigemColeta { get; set; }

        [Display(Name = @"Origem Coleta")]
        public virtual string NomeOrigemColeta { get; set; }

        [Display(Name = @"Agente")]
        public string NomeAgente { get; set; }
    }
}