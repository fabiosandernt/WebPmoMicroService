
namespace ONS.WEBPMO.Domain.DTO
{
    public class InicializacaoConvergenciaCceeDTO
    {
        public int IdSemanaOperativa { get; set; }
        public byte[] VersaoSemanaOperativa { get; set; }
        public ISet<ArquivoDadoNaoEstruturadoDTO> Arquivos { get; set; }
    }
}
