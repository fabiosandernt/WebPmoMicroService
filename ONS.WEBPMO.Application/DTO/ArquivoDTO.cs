using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ONS.WEBPMO.Application.DTO
{
    public class ArquivoDTO
    {
        public Guid IdArquivo { get; set; }
        public string NomeArquivo { get; set; }
        public int Tamanho { get; set; }
    }
}
