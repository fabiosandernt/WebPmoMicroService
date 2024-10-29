
using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;
using System.ComponentModel.DataAnnotations;

namespace ONS.WEBPMO.Domain.Entities.Filters
{
    public class DadoColetaInsumoFilter : ICustomQueryable, IQueryPaging, IQuerySort
    {
        [Display(Name = "Id")]
        [QueryOperator(Operator = WhereOperator.Contains)]
        public int IdColetaInsumo { get; set; }
        [Display(Name = "IdList")]
        [QueryOperator(Operator = WhereOperator.Contains)]
        public IList<int>? IdsDadoColeta { get; set; }
        public int? Limit { get; set; } = 10;
        public int? Offset { get; set; }
        public string? Sort { get; set; }
    }
}
