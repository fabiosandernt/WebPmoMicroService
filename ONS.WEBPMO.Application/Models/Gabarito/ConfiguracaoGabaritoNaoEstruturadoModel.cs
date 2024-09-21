using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ONS.WEBPMO.Application.Models.Gabarito
{
    public class ConfiguracaoGabaritoNaoEstruturadoModel : BaseGabaritoModel
    {
        [Required]
        [Display(Name = @"Seleção de Insumo(s)")]
        public override IList<int> IdsInsumo { get; set; }
    }
}