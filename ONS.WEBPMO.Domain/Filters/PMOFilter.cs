using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using ONS.WEBPMO.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace ONS.WEBPMO.Domain.Entities.Filters
{

    [Serializable]
    public class PMOFilter : BaseFilter
    {
        [Display(Name = "Mês")]
        [QueryOperator(Operator = WhereOperator.Equals)]
        public int? Mes { get; set; }

        [Display(Name = "Ano")]
        [QueryOperator(Operator = WhereOperator.Equals)]
        public int? Ano { get; set; }

        [Display(Name = "Id")]
        [QueryOperator(Operator = WhereOperator.Equals)]
        public int? Id { get; set; }

    }
}
