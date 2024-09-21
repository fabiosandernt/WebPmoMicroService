using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;

namespace ONS.WEBPMO.Domain.Entities.Filters
{
    public class EnviarDadosColetaInsumoFilter : ICustomQueryable, IQueryPaging, IQuerySort
    {
        public EnviarDadosColetaInsumoFilter()
        {
            IdsColetaInsumo = new List<int>();
        }

        public IList<int> IdsColetaInsumo { get; set; }
        public int IdSemanaOperativa { get; set; }
        public IList<string> VersoesString { get; set; }
        public int? Limit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? Offset { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Sort { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
