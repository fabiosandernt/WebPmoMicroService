using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ONS.WEBPMO.Application.Models.PMO
{
    public class SemanaOperativaModel
    {
        public int Id { get; set; }

        public int PMOMesReferencia { get; set; }
        public int PMOAnoReferencia { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataReuniao { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataInicioSemana { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataFimSemana { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataInicioManutencao { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataFimManutencao { get; set; }

        public string SituacaoDescricao { get; set; }

        public int? Revisao { get; set; }

        public byte[] Versao { get; set; }
    }
}