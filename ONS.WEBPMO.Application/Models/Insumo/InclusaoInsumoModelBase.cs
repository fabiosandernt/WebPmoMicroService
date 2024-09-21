using ONS.WEBPMO.Domain.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace ONS.WEBPMO.Application.Models.Insumo
{
    public class InclusaoInsumoModelBase
    {
        public InclusaoInsumoModelBase()
        {
            Ativo = true;
        }

        public int Id { get; set; }

        [Required, Display(Name = "Nome"), StringLength(150)]
        public string Nome { get; set; }

        [Required, Display(Name = "Ordem Exibição")]
        public short? OrdemExibicao { get; set; }

        [Required, Display(Name = "Pré-Aprovado")]
        public bool? IsPreAprovado { get; set; }
        public string Reservado { get; set; }
        public string TipoInsumoNome
        {
            get { return TipoInsumo.ToDescription(); }
        }
        public TipoInsumoEnum TipoInsumo { get; set; }

        [Required, Display(Name = "Sigla"), StringLength(50)]
        public string SiglaInsumo { get; set; }

        [Required, Display(Name = "Pode ser exportado para Excel")]
        public bool? ExportarInsumo { get; set; }

        [Required, Display(Name = "Ativo")]
        public bool Ativo { get; set; }
    }
}