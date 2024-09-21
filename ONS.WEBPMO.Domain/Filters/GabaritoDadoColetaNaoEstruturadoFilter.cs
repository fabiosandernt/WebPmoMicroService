using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;

namespace ONS.WEBPMO.Domain.Entities.Filters
{

    public class GabaritoDadoColetaNaoEstruturadoFilter : ICustomQueryable, IQueryPaging, IQuerySort
    {
        public int IdInsumo { get; set; }

        public int IdColetaInsumo { get; set; }

        public int IdAgente { get; set; }

        public int? IdSemanaOperativa { get; set; }

        public string PerfilONS { get; set; }

        public bool IsPadrao { get; set; }
        public int? Limit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? Offset { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Sort { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
