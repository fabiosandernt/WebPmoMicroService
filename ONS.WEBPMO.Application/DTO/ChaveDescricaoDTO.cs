using ONS.Common.Util.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ONS.WEBPMO.Application.DTO
{
    public class ChaveDescricaoDTO<T>
    {
        public ChaveDescricaoDTO() { }
        public ChaveDescricaoDTO(T chave, string descricao)
        {
            this.Chave = chave;
            this.Descricao = descricao;
        }

        public T Chave { get; set; }
        public string Descricao { get; set; }
    }
}
