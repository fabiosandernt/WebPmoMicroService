
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ONS.WEBPMO.Application.Models.ColetaInsumo
{
    

    //[ModelBinder(typeof(EnumerableValueBinder))]
    public class PesquisaColetaInsumoModel : FiltroPesquisaColetaInsumoModel
    {
        public PesquisaColetaInsumoModel()
        {
            SemanasOperativas = new List<SelectListItem>();
            Agentes = new List<SelectListItem>();
            Insumos = new List<SelectListItem>();
            SituacoesColeta = new List<SelectListItem>();
        }

        [Required]
        [Display(Name = @"Estudo")]
        public override int? IdSemanaOperativa { get; set; }
        public string NomeSemanaOperativaSituacao { get; set; }
        public bool IsSemanaOperativaEmConfiguracao { get; set; }

        public string MensagemDaAberturaColetaEditavel { get; set; }

        public bool EnviarTodos { get; set; }

        public IList<SelectListItem> SemanasOperativas { get; set; }
        public IList<SelectListItem> Insumos { get; set; }
        public IList<SelectListItem> Agentes { get; set; }
        public IList<SelectListItem> SituacoesColeta { get; set; }

        public string ReenvioDeNotificacao { get; set; }

        public override int? IdSituacaoSemanaOperativa { get; set; }

        public string NomesGrandezasNaoEstagioAlteradas { get; set; }

        public bool EstaEmColetaDeDados { get; set; }
        public string Periodicidade { get; set; }
    }
}