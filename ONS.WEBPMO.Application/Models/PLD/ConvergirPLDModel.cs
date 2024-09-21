using ONS.WEBPMO.Application.DTO;
using System.ComponentModel.DataAnnotations;

namespace ONS.WEBPMO.Application.Models.PLD
{
    public class ConvergirPLDModel : ConsultaPLDModel
    {
        public ConvergirPLDModel()
        {
            Arquivos = new List<ArquivoDadoNaoEstruturadoDTO>();
        }

        public IList<ArquivoDadoNaoEstruturadoDTO> Arquivos { get; set; }
        public string DescricaoSemanaOperativa { get; set; }
        public string VersaoStringSemanaOperativa { get; set; }

        [StringLength(1000, ErrorMessage = "Campo 'Observação' deve possuir no máximo 1000 caracteres.")]
        public string ObservacoesConvergenciaPld { get; set; }

        public bool IsConvergirPLD { get; set; }

    }
}