using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ONS.WEBPMO.Application.DTO.DTO
{
    public class LogNotificacaoDTO
    {
        public LogNotificacaoDTO()
        {
            Agentes = new List<ChaveDescricaoDTO<int>>();
            SemanasOperativas = new List<ChaveDescricaoDTO<int>>();
        }

        public string NomeSemanaOperativaSituacao { get; set; }
        public IList<ChaveDescricaoDTO<int>> SemanasOperativas { get; set; }
        public IList<ChaveDescricaoDTO<int>> Agentes { get; set; }
    }
}
