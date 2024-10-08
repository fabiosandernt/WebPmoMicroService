using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;
using System.ComponentModel.DataAnnotations;

namespace ONS.WEBPMO.Domain.Entities.Filters
{
    public class InsumoFiltro : ICustomQueryable, IQueryPaging, IQuerySort
    {
        [Display(Name = "Nome")]
        [QueryOperator(Operator = WhereOperator.Contains)]
        public string? NomInsumopmo { get; set; }

        [Display(Name = "Sligla")]
        [QueryOperator(Operator = WhereOperator.Contains)]
        public string? SglInsumo { get; set; }

        [Display(Name = "Tipo")]
        [QueryOperator(Operator = WhereOperator.Contains)]
        public string? TipInsumopmo { get; set; }
        public int? Limit { get; set; } = 10;
        public int? Offset { get; set; }
        public string? Sort { get; set; }
    }
}
