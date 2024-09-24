using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;
using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Domain.Entities.Filters
{
    public class GabaritoDadosFilter : ICustomQueryable, IQueryPaging, IQuerySort
    {
        public bool IsPadrao { get; set; }
        public int? IdSemanaOperativa { get; set; }
        public int? IdAgente { get; set; }
        public string IdOrigemColeta { get; set; }
        public string CodigoPerfilOns { get; set; }

        public TipoInsumoEnum TipoInsumo { get; set; }
        public TipoOrigemColetaEnum? TipoOrigemColeta { get; set; }
        public int? Limit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? Offset { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Sort { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
