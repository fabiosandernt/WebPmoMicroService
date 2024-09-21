using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;

namespace ONS.WEBPMO.Domain.Entities.Filters
{
    public class LogNotificacaoFilter : ICustomQueryable, IQueryPaging, IQuerySort
    {
        public LogNotificacaoFilter()
        {
            IdsAgentes = new List<int>();
        }

        public int IdSemanaOperativa { get; set; }
        public IList<int> IdsAgentes { get; set; }
        public bool Abertura { get; set; }
        public bool Reabertura { get; set; }
        public bool Rejeicao { get; set; }
        public int? Limit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? Offset { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Sort { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
