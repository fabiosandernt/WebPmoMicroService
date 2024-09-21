
namespace ONS.WEBPMO.Application.Models.Gabarito
{
    using Domain.Entities;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    


    public class GabaritoConsultaModel
    {
        [Display(Name = "Gabarito")]
        [Required]
        public bool IsPadrao { get; set; }

        public int? IdSemanaOperativa { get; set; }
        public int? IdAgente { get; set; }
        public int? IdInsumo { get; set; }
        public string IdOrigemColeta { get; set; }
        public string CodigoPerfilONS { get; set; }

        public TipoOrigemColetaEnum? TipoOrigemColeta { get; set; }
        public TipoInsumoEnum TipoInsumo { get; set; }

        public SituacaoSemanaOperativaEnum? SituacaoSemanaOperativa { get; set; }

        public string NomeGabarito { get; set; }

        public IList<SelectListItem> SemanasOperativas { get; set; }
        public IList<SelectListItem> Agentes { get; set; }
        public IList<SelectListItem> Insumos { get; set; }

        public bool GabaritoBloqueado
        {
            get
            {
                return SituacaoSemanaOperativa.HasValue
                    && SituacaoSemanaOperativa.Value != SituacaoSemanaOperativaEnum.Configuracao
                    && SituacaoSemanaOperativa.Value != SituacaoSemanaOperativaEnum.ColetaDados;
            }
        }
    }
}