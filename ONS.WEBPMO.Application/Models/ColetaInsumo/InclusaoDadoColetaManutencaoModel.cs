using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ONS.WEBPMO.Application.Models.ColetaInsumo
{
    public class InclusaoDadoColetaManutencaoModel : DadoColetaManutencaoModel
    {
        public InclusaoDadoColetaManutencaoModel()
        {
            Usinas = new List<SelectListItem>();
            UnidadesGeradoras = new List<SelectListItem>();
        }

        public int IdColetaInsumo { get; set; }

        [Display(Name = @"Usina")]
        [Required]
        public string IdUsina { get; set; }

        [Display(Name = @"Unidade Geradora")]
        [Required]
        public string IdUnidadeGeradora { get; set; }

        public IList<SelectListItem> Usinas { get; set; }
        public IList<SelectListItem> UnidadesGeradoras { get; set; }

    }
}