using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;

namespace ONS.WEBPMO.Domain.Entities.Filters
{
    public class PesquisaMonitorarColetaInsumoFilter : ICustomQueryable, IQueryPaging, IQuerySort
    {
        public PesquisaMonitorarColetaInsumoFilter()
        {
            IdsAgentes = new List<int>();
            IdsInsumo = new List<int>();
            IdsSituacaoColeta = new List<int>();
            PerfisONS = new List<string>();
        }

        public int IdSemanaOperativa { get; set; }
        public IList<int> IdsAgentes { get; set; }
        public IList<int> IdsInsumo { get; set; }
        public IList<int> IdsSituacaoColeta { get; set; }
        public IList<string> PerfisONS { get; set; }
        public int? Limit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? Offset { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Sort { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
