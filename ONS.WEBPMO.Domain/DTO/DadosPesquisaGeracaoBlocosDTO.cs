
using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Domain.DTO
{
    public class DadosPesquisaGeracaoBlocosDTO
    {
        public DadosPesquisaGeracaoBlocosDTO()
        {
            ArquivosDadoNaoEstruturado = new List<InsumoArquivoDTO>();
            ArquivosGeracaoBlocos = new List<ArquivoDTO>();
        }

        public IList<InsumoArquivoDTO> ArquivosDadoNaoEstruturado { get; set; }
        public IList<ArquivoDTO> ArquivosGeracaoBlocos { get; set; }
        public SituacaoSemanaOperativaEnum SituacaoSemanaOperativa { get; set; }
    }
}
