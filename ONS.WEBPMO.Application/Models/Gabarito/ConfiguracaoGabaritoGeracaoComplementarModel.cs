﻿using System.ComponentModel.DataAnnotations;

namespace ONS.WEBPMO.Application.Models.Gabarito
{
    public class ConfiguracaoGabaritoGeracaoComplementarModel : BaseGabaritoModel
    {
        [Required]
        [Display(Name = @"Seleção de Insumo(s)")]
        public override IList<int> IdsInsumo { get; set; }
    }
}