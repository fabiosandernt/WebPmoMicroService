using System.ComponentModel.DataAnnotations;

namespace ONS.WEBPMO.Application.Models.LogNotificacao
{  
    

    //[ModelBinder(typeof(EnumerableValueBinder))]
    public class PesquisaLogNotificacaoModel
    {
        public PesquisaLogNotificacaoModel()
        {
            SemanasOperativas = new List<SelectListItem>();
            Agentes = new List<SelectListItem>();
            IdsAgentes = new List<int>();
            Acoes = (from AcaoLogNotificacaoEnum d in Enum.GetValues(typeof(AcaoLogNotificacaoEnum))
                     select new SelectListItem
                     {
                         Value = ((int)d).ToString(),
                         Text = d.ToDescription()
                     }).ToList();
        }

        [Required]
        [Display(Name = @"Estudo")]
        public int? IdSemanaOperativa { get; set; }

        public string NomeSemanaOperativaSituacao { get; set; }

        public int? IdSituacaoSemanaOperativa { get; set; }

        public IList<SelectListItem> SemanasOperativas { get; set; }

        public IList<SelectListItem> Agentes { get; set; }

        [Display(Name = @"Agente(s)")]
        public IList<int> IdsAgentes { get; set; }

        [Display(Name = @"Acões")]
        public IList<int> IdsAcoes { get; set; }

        public IList<SelectListItem> Acoes { get; set; }

        public IList<int> IdsLogNotificacao { get; set; }


    }
}