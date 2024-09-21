
using ONS.WEBPMO.Domain.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace ONS.WEBPMO.Application.Models.ColetaInsumo
{
    //[ModelBinder(typeof(EnumerableValueBinder))]
    public class FiltroPesquisaColetaInsumoModel
    {
        public FiltroPesquisaColetaInsumoModel()
        {
            IdsAgentes = new List<int>();
            IdsInsumo = new List<int>();
            IdsSituacaoColeta = new List<int>();
        }

        public SituacaoSemanaOperativaEnum? SituacaoSemanaOperativa { get; set; }
        public EtapaMonitoramento EtapaSelecionada { get; set; }

        public string VersaoStringSemanaOperativa { get; set; }
        public string NomeSemanaOperativa { get; set; }

        [Display(Name = @"Estudo")]
        public virtual int? IdSemanaOperativa { get; set; }

        [Display(Name = @"Insumo(s)")]
        public IList<int> IdsInsumo { get; set; }

        [Display(Name = @"Agente(s)")]
        public IList<int> IdsAgentes { get; set; }

        [Display(Name = @"Situação Coleta")]
        public IList<int> IdsSituacaoColeta { get; set; }

        [Display(Name = @"Situação Semana Operativa ID")]
        public virtual int? IdSituacaoSemanaOperativa { get; set; }

        public bool? isGerarBlocoParaEncerrar { get; set; }

        public bool EstaEmColetaDeDados { get; set; }
    }

    public enum EtapaMonitoramento
    {
        ColetaDados = 0,
        GeracaoBlocos,
        ConvergenciaCCEE,
        PublicacaoResultados
    }
}