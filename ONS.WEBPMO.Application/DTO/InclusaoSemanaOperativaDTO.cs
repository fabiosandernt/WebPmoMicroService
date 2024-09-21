using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ONS.WEBPMO.Application.DTO
{
    public class InclusaoSemanaOperativaDTO
    {
        public int IdPMO { get; set; }
        public bool IsInicioPMO { get; set; }
        public byte[] VersaoPMO { get; set; }
    }
}
