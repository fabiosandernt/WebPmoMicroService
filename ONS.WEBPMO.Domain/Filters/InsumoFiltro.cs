using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using ONS.WEBPMO.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace ONS.WEBPMO.Domain.Entities.Filters
{
    public class InsumoFiltro : BaseFilter
    {
        [Display(Name = "Nome")]
        [QueryOperator(Operator = WhereOperator.Contains)]
        public string? Nome { get; set; }

        [Display(Name = "Sligla")]
        [QueryOperator(Operator = WhereOperator.Contains)]
        public string? SglInsumo { get; set; }

        [Display(Name = "Tipo")]
        [QueryOperator(Operator = WhereOperator.Contains)]
        public string? TipInsumopmo { get; set; }

    }
}
