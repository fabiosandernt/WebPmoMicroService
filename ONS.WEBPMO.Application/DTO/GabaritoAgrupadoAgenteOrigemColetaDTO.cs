using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ONS.WEBPMO.Application.DTO
{
    public class GabaritoAgrupadoAgenteOrigemColetaDTO
    {
        public GabaritoAgrupadoAgenteOrigemColetaDTO()
        {
            Insumos = new List<ChaveDescricaoDTO<int>>();
        }
        public ChaveDescricaoDTO<int> Agente { get; set; }
        public ChaveDescricaoDTO<string> OrigemColeta { get; set; }
        public List<ChaveDescricaoDTO<int>> Insumos { get; set; }

        public string CodigoPerfilONS { get; set; }

        public string AgentePerfilONS
        {
            get
            {
                return string.IsNullOrWhiteSpace(CodigoPerfilONS)
                    ? Agente.Descricao
                    : string.Format("{0}/{1}", Agente.Descricao, CodigoPerfilONS);
            }
        }
    }
}
