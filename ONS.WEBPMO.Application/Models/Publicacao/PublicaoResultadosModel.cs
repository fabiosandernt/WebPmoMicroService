
using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Models.ColetaInsumo;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Application.Models.Publicacao
{
    //[ModelBinder(typeof(EnumerableValueBinder))]
    public class PublicaoResultadosModel : PesquisaColetaInsumoModel
    {
        public PublicaoResultadosModel()
        {
            ArquivosInsumos = new List<ArquivoDadoNaoEstruturadoConsultaInsumoDTO>();
            ArquivosEnviadosPublicacao = new List<UploadFileModel>();
        }

        public IList<ArquivoDadoNaoEstruturadoConsultaInsumoDTO> ArquivosInsumos { get; set; }
        public IList<UploadFileModel> ArquivosEnviadosPublicacao { get; set; }
        public SituacaoSemanaOperativa SituacaoSemanaOperativa { get; set; }

    }
}