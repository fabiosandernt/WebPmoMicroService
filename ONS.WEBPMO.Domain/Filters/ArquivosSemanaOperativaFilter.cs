using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;

namespace ONS.WEBPMO.Domain.Entities.Filters
{

    public class ArquivosSemanaOperativaFilter : ICustomQueryable, IQueryPaging, IQuerySort
    {
        public int IdSemanaOperativa { get; set; }
        public bool? IsConsiderarInsumosProcessamento { get; set; }
        public bool? IsConsiderarInsumosDECOMP { get; set; }
        public bool? IsConsiderarInsumosConvergenciaCCEE { get; set; }
        public bool? IsConsiderarInsumosPublicacao { get; set; }
        public int? Limit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? Offset { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Sort { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
