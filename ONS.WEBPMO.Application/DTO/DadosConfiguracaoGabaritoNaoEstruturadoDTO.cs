using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ONS.WEBPMO.Application.DTO
{
    public class DadosConfiguracaoGabaritoNaoEstruturadoDTO
    {
        public DadosConfiguracaoGabaritoNaoEstruturadoDTO()
        {
            Agentes = new List<ChaveDescricaoDTO<int>>();
            Insumos = new List<ChaveDescricaoDTO<int>>();
        }

        public ChaveDescricaoDTO<int> SemanaOperativa { get; set; }
        public IList<ChaveDescricaoDTO<int>> Agentes { get; set; }
        public IList<ChaveDescricaoDTO<int>> Insumos { get; set; }

    }
}
