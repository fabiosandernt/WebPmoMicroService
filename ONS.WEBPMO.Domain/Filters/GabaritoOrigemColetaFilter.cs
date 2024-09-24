using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;
using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Domain.Entities.Filters
{
    public class GabaritoOrigemColetaFilter : ICustomQueryable, IQueryPaging, IQuerySort
    {
        public int? IdAgente { get; set; }
        public int? IdInsumo { get; set; }
        public int? IdSemanaOperativa { get; set; }
        public string IdOrigemColeta { get; set; }
        public bool IsPadrao { get; set; }
        public TipoOrigemColetaEnum? TipoOrigemColeta { get; set; }
        public TipoInsumoEnum TipoInsumo { get; set; }
        public string CodigoPerfilONS { get; set; }
        public int? Limit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? Offset { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Sort { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
