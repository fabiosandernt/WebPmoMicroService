
using System.ComponentModel.DataAnnotations;


namespace ONS.WEBPMO.Application.Models.PMO
{
    public class AlteracaoSemanaOperativaModel
    {
        public int Id { get; set; }
        public int IdPMO { get; set; }

        [Display(Name = "Data da Reunião")]
        [Required]
        [DataType(DataType.Date)]
        //[DateTimeValidator]
        public DateTime DataReuniao { get; set; }

        [Display(Name = "Início Manutenções")]
        [Required]
        [DataType(DataType.Date)]
        //[DateTimeValidator]
        public DateTime DataInicioManutencao { get; set; }

        [Display(Name = "Término Manutenções")]
        [Required]
        [DataType(DataType.Date)]
        //[DateTimeValidator]
        //[CompareDate(CompareDateAttribute.TypeEnum.MaiorIgual, "DataInicioManutencao", ErrorMessage = "Data de término das manutenções não pode ser inferior à data de início.")]
        public DateTime DataFimManutencao { get; set; }

        public DateTime DataInicioSemana { get; set; }
        public DateTime DataFimSemana { get; set; }
    }
}