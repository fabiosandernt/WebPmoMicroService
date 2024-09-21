using System.ComponentModel.DataAnnotations;

namespace ONS.WEBPMO.Application.Models.Gabarito
{        

    [ModelBinder(typeof(ConfiguracaoGabaritoModelBinder))]
    public class BaseGabaritoModel
    {
        public BaseGabaritoModel()
        {
            IdsInsumo = new List<int>();
            Agentes = new List<SelectListItem>();
            Insumos = new List<SelectListItem>();
            InsumosGabarito = new List<SelectListItem>();
        }

        [Display(Name = @"Seleção de Insumo(s)")]
        public virtual IList<int> IdsInsumo { get; set; }
        public IList<SelectListItem> Insumos { get; set; }
        public IList<SelectListItem> InsumosGabarito { get; set; }

        public int? IdSemanaOperativa { get; set; }
        public bool IsPadrao { get; set; }
        public TipoOrigemColetaEnum? TipoOrigemColeta { get; set; }
        public TipoInsumoEnum TipoInsumo { get; set; }

        [Display(Name = @"Gabarito")]
        public string NomeGabarito { get; set; }

        [Display(Name = @"Agente")]
        [Required]
        public int? IdAgente { get; set; }
        public IList<SelectListItem> Agentes { get; set; }

        [Display(Name = @"Perfil")]
        public string CodigoPerfilONS { get; set; }
    }
}