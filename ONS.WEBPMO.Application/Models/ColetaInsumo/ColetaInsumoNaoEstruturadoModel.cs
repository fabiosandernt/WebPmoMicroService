using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using ONS.SGIPMO.WebSite.Adapters;

namespace ONS.WEBPMO.Application.Models.ColetaInsumo
{
    public class ColetaInsumoNaoEstruturadoModel : ColetaInsumoModel
    {
        public ColetaInsumoNaoEstruturadoModel()
        {
            UploadFileModels = new List<UploadFileModel>();
        }

        public int? IdDadoColetaNaoEstruturado { get; set; }

        public List<UploadFileModel> UploadFileModels { get; set; }

        [StringLength(800, ErrorMessage = "Campo 'Observação' deve possuir no máximo 800 caracteres.")]
        public string ObservacaoColetaNaoEstruturada { get; set; }

        [StringLength(800, ErrorMessage = "Campo 'Motivo de Rejeição' deve possuir no máximo 800 caracteres.")]
        public string MotivoRejeicaoColetaNaoEstruturado { get; set; }

        [StringLength(800, ErrorMessage = "Campo 'Motivo de Alteração' deve possuir no máximo 800 caracteres.")]
        public string MotivoAlteracaoColetaNaoEstruturado { get; set; }

        public bool EnviarDadosAoSalvar { get; set; }

        public bool AprovarDadosAoAnalisar { get; set; }

        public bool RejeitarDadosAoAnalisar { get; set; }

    }
}