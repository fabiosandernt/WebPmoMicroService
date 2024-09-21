using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;
using AspNetCore.IQueryable.Extensions;

namespace ONS.WEBPMO.Domain.Entities.Filters
{
    public class InsumoFiltro : ICustomQueryable, IQueryPaging, IQuerySort
    {
        public string Nome { get; set; }

        public string TipoInsumo { get; set; }

        public string SiglaInsumo { get; set; }

        public int? CategoriaId { get; set; }

        public int? TipoColetaId { get; set; }
        public int? Limit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? Offset { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Sort { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
