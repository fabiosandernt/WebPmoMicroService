using System.Collections.Generic;

namespace ONS.WEBPMO.Application.DTO
{
    public class PublicacaoResultadosDTO
    {
        public PublicacaoResultadosDTO()
        {
            Arquivos = new HashSet<ArquivoDadoNaoEstruturadoDTO>();
        }

        public int IdSemanaOperativa { get; set; }
        public byte[] VersaoSemanaOperativa { get; set; }
        public ISet<ArquivoDadoNaoEstruturadoDTO> Arquivos { get; set; }
        public bool IsEncerradoDiretamente { get; set; } = false;

    }
}
