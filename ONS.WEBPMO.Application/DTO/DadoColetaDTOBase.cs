using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ONS.WEBPMO.Application.DTO
{
    public class DadoColetaDTOBase
    {
        public string TipoDadoColeta { get; set; }
        public int TipoLimiteId { get; set; }
        public int TipoPatamarId { get; set; }
        public int GrandezaId { get; set; }
        public string GrandezaNome { get; set; }
        public string OrigemColetaId { get; set; }
    }
}
