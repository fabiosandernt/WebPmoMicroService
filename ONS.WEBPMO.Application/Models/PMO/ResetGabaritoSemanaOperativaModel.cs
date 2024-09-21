using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONS.WEBPMO.Application.Models.PMO
{
    public class ResetGabaritoSemanaOperativaModel
    {
        public int IdPMO { get; set; }
        public int? IdSemanaOperativa { get; set; }
        public string VersaoPmoString { get; set; }
    }
}