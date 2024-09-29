
using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;

namespace ONS.WEBPMO.Domain.Entities.Filters
{
    public class GabaritoConfiguracaoFilter : ICustomQueryable, IQueryPaging, IQuerySort
    {
        public GabaritoConfiguracaoFilter()
        {
            IdsOrigemColeta = new List<string>();
            IdsInsumo = new List<int>();
        }

        public bool IsOrigemColetaNull { get; set; }
        public bool IsPadrao { get; set; }
        public int? IdSemanaOperativa { get; set; }
        public int? IdAgente { get; set; }
        public string IdOrigemColetaPai { get; set; }
        public int? IdInsumo { get; set; }
        public string TipoInsumo { get; set; }
        public string CodigoPerfilONS { get; set; }
        public bool IsNullCodigoPerfilONS { get; set; }
        public IList<string> IdsOrigemColeta { get; set; }
        public IList<int> IdsInsumo { get; set; }
        public int? Limit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? Offset { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Sort { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
