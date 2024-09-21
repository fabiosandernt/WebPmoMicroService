using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;

namespace ONS.WEBPMO.Domain.Entities.Filters
{

    public class GrandezaFilter : ICustomQueryable, IQueryPaging, IQuerySort
    {
        public IList<int> IdsGrandeza { get; set; }
        public int? IdInsumo { get; set; }
        public bool IsOrdenacaoPadrao { get; set; }
        public int? Limit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? Offset { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Sort { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public GrandezaFilter()
        {
            IdsGrandeza = new List<int>();
        }
    }
}
