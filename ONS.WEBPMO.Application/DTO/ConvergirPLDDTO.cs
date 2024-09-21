using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ONS.WEBPMO.Application.DTO
{
    public class ConvergirPLDDTO
    {
        public int IdSemanaOperativa { get; set; }
        public bool Convergir { get; set; }
        public byte[] VersaoSemanaOperativa { get; set; }
        public string ObservacoesConvergenciaPld { get; set; }

    }
}
