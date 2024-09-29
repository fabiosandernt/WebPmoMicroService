
using ONS.WEBPMO.Domain.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ONS.WEBPMO.Application.Models.Insumo
{
    public class ManutencaoGrandezaModel
    {
        public ManutencaoGrandezaModel()
        {
            TiposDado = new List<SelectListItem>();
        }

        public int Id { get; set; }
        public int InsumoId { get; set; }
        public int RowId { get; set; }

        [Required, Display(Name = @"Ordem Exibição")]
        public short? OrdemExibicao { get; set; }

        [Required, Display(Name = @"Nome")]
        [StringLength(150)]
        public string Nome { get; set; }

        [Display(Name = @"Estágio")]
        public bool IsColetaPorEstagio { get; set; }

        [Display(Name = @"Patamar")]
        public bool IsColetaPorPatamar { get; set; }

        [Display(Name = @"Limite")]
        public bool IsColetaPorLimite { get; set; }

        [Display(Name = @"Pré-aprovado com alteração")]
        public bool IsPreAprovadoComAlteracao { get; set; }

        [Display(Name = @"Aceita valor negativo")]
        public bool AceitaValorNegativo { get; set; }

        [Display(Name = @"Recupera valor anterior")]
        public bool PodeRecuperarValor { get; set; }

        [Display(Name = @"Destaca diferença")]
        public bool DestacaDiferenca { get; set; }

        [Display(Name = @"Comportamento obrigatório")]
        public bool IsObrigatorio { get; set; }

        private string tipoDadoGrandezaDescricao;
        public string TipoDadoGrandezaDescricao
        {
            get
            {
                return ((TipoDadoGrandezaEnum)TipoDadoGrandezaId).ToDescription();
            }
            set
            {
                tipoDadoGrandezaDescricao = value;
            }
        }
        [Required, Display(Name = @"Tipo de Dados")]
        public int TipoDadoGrandezaId { get; set; }
        public IList<SelectListItem> TiposDado { get; set; }

        [Display(Name = @"Qtd Dígitos")]
        //[RequiredIf("IsCasasInteirasRequired", true)]
        public int? QuantidadeCasasInteira { get; set; }

        [Display(Name = @"Qtd Decimais")]
        //[RequiredIf("IsCasasInteirasRequired", true)]
        public int? QuantidadeCasasDecimais { get; set; }

        [Required, Display(Name = @"Ativo")]
        public bool Ativo { get; set; }

        public bool PermiteAlteracao { get; set; }

        public bool IsCasasInteirasRequired
        {
            get
            {
                return (TipoDadoGrandezaId == (int)TipoDadoGrandezaEnum.Numerico);
            }
        }
    }
}