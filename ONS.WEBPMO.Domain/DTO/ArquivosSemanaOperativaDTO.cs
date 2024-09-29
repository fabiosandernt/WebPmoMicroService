
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.DTO
{
    public class ArquivosSemanaOperativaDTO //: ICloneablePath
    {

        public ArquivosSemanaOperativaDTO()
        {
            ArquivosInsumos = new List<ArquivoDadoNaoEstruturadoConsultaInsumoDTO>();
            ArquivosEnviados = new List<ArquivoDadoNaoEstruturadoDTO>();
        }

        public SituacaoSemanaOperativa SituacaoSemanaOperativa { get; set; }

        public IList<ArquivoDadoNaoEstruturadoConsultaInsumoDTO> ArquivosInsumos { get; set; }

        public IList<ArquivoDadoNaoEstruturadoDTO> ArquivosEnviados { get; set; }

        public Type GetRealType()
        {
            return this.GetType();
        }

    }
}
