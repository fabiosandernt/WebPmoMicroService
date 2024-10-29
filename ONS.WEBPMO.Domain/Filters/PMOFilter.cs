using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;
using System.ComponentModel.DataAnnotations;

namespace ONS.WEBPMO.Domain.Entities.Filters
{

    [Serializable]
    public class PMOFilter : ICustomQueryable, IQueryPaging, IQuerySort
    {
        [Display(Name = "Mês")]
        [QueryOperator(Operator = WhereOperator.Contains)]
        public int? Mes { get; set; }
        [Display(Name = "Ano")]
        [QueryOperator(Operator = WhereOperator.Contains)]
        public int? Ano { get; set; }       
        public int? Limit { get; set; } = 10;
        public int? Offset { get; set; }
        public string? Sort { get; set; }
    }
}
