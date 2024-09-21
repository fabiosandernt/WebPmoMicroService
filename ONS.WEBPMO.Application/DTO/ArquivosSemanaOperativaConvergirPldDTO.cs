
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Application.DTO
{
    public class ArquivosSemanaOperativaConvergirPldDTO //: ICloneablePath
    {
        public ArquivosSemanaOperativaConvergirPldDTO()
        {
            Arquivos = new List<ArquivoDadoNaoEstruturadoDTO>();
        }

        public SemanaOperativa SemanaOperativa { get; set; }

        public IList<ArquivoDadoNaoEstruturadoDTO> Arquivos { get; set; }

        public Type GetRealType()
        {
            return this.GetType();
        }
    }
}
