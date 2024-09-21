using System;
using System.ComponentModel.DataAnnotations;
using System.Web;



namespace ONS.WEBPMO.Application.Models.ColetaInsumo
{
    public class DadoColetaManutencaoModel
    {
        [Display(Name = @"Data de Início")]
        [Required]
        [DataType(DataType.DateTime)]
        [DateTimeValidator]
        public DateTime? DataInicio { get; set; }

        [Display(Name = @"Data de Término")]
        [Required]
        [DataType(DataType.DateTime)]
        [DateTimeValidator]
        public DateTime? DataFim { get; set; }

        [Display(Name = @"Tempo para Retorno")]
        public string TempoRetorno { get; set; }

        [Display(Name = @"Justificativa")]
        [StringLength(1000, ErrorMessage = "Campo 'Justificativa' não pode ultrapassar 1000 caracteres.")]
        [AllowHtml]
        [Required]
        public string Justificativa { get; set; }

        public string JustificativaEncoded
        {
            get { return HttpUtility.HtmlEncode(Justificativa); }
        }

        public string Numero { get; set; }

        public string VersaoColetaInsumo { get; set; }
    }
}