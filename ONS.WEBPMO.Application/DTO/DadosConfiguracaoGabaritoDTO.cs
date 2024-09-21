using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ONS.WEBPMO.Application.DTO
{
    public class DadosConfiguracaoGabaritoDTO
    {
        public DadosConfiguracaoGabaritoDTO()
        {
            Agentes = new List<ChaveDescricaoDTO<int>>();
            OrigensColeta = new List<ChaveDescricaoDTO<string>>();
            Insumos = new List<ChaveDescricaoDTO<int>>();
        }

        public ChaveDescricaoDTO<int> SemanaOperativa { get; set; }

        public IList<ChaveDescricaoDTO<int>> Agentes { get; set; }
        public IList<ChaveDescricaoDTO<string>> OrigensColeta { get; set; }
        public IList<ChaveDescricaoDTO<int>> Insumos { get; set; }

    }
}
