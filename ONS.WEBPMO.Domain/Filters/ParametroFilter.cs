using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using System.ComponentModel.DataAnnotations;

namespace ONS.WEBPMO.Domain.Filters
{
    public class ParametroFilter : ICustomQueryable
    {
        [Display(Name = "Nome")]
        [QueryOperator(Operator = WhereOperator.Contains)]
        public string Nome { get; set; }

    }
}
