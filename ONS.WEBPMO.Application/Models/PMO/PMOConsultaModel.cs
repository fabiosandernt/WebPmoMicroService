using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ONS.WEBPMO.Application.Models.PMO
{
    public class PMOConsultaModel
    {
        [Required]
        [Display(Name = @"Ano")]
        [Range(1900, 2079, ErrorMessage = @"{0} inválido.")]
        public int? Ano { get; set; }

        [Required]
        [Display(Name = @"Mês")]
        public int? Mes { get; set; }

        public IList<SelectListItem> Meses
        {
            get
            {
                IList<SelectListItem> lista = new List<SelectListItem>
                {
                    new SelectListItem {Text = "Selecione", Value = ""},
                    new SelectListItem {Text = "Janeiro", Value = "1"},
                    new SelectListItem {Text = "Fevereiro", Value = "2"},
                    new SelectListItem {Text = "Março", Value = "3"},
                    new SelectListItem {Text = "Abril", Value = "4"},
                    new SelectListItem {Text = "Maio", Value = "5"},
                    new SelectListItem {Text = "Junho", Value = "6"},
                    new SelectListItem {Text = "Julho", Value = "7"},
                    new SelectListItem {Text = "Agosto", Value = "8"},
                    new SelectListItem {Text = "Setembro", Value = "9"},
                    new SelectListItem {Text = "Outubro", Value = "10"},
                    new SelectListItem {Text = "Novembro", Value = "11"},
                    new SelectListItem {Text = "Dezembro", Value = "12"}
                };
                return lista;
            }
        }
    }
}