﻿using ONS.WEBPMO.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONS.WEBPMO.Domain.Filters
{
    public class DisponibilidadeFilter : BaseFilter
    {
        [Display(Name = "DataInicioSemana")]
        [Required]
        public DateTime? DataInicioSemana { get; set; }

        [Display(Name = "DataFimSemana")]
        [Required]
        public DateTime? DataFimSemana { get; set; }
        public override int? Limit { get; set; } = 10;
        public override int? Offset { get; set; } = 0;
        public int? CodDpp { get; set; }
    }
}