
using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Models.ColetaInsumo;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Application.Models.ConvergenciaCCEE
{
    //[ModelBinder(typeof(EnumerableValueBinder))]
    public class ConvergenciaCCEEModel : PesquisaColetaInsumoModel
    {
        public ConvergenciaCCEEModel()
        {
            ArquivosInsumosProcessamento = new List<ArquivoDadoNaoEstruturadoConsultaInsumoDTO>();
            ArquivosEnviadosConvergenciaCcee = new List<UploadFileModel>();
        }

        public IList<ArquivoDadoNaoEstruturadoConsultaInsumoDTO> ArquivosInsumosProcessamento { get; set; }
        public IList<UploadFileModel> ArquivosEnviadosConvergenciaCcee { get; set; }
        public SituacaoSemanaOperativa SituacaoSemanaOperativa { get; set; }

    }
}