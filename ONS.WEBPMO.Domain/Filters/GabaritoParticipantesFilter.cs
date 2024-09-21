using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;
using AspNetCore.IQueryable.Extensions;

namespace ONS.WEBPMO.Domain.Entities.Filters
{
    public class GabaritoParticipantesFilter : ICustomQueryable, IQueryPaging, IQuerySort
    {
        public bool IsPadrao { get; set; }
        public int? IdSemanaOperativa { get; set; }
        public int? IdAgente { get; set; }
        public string IdUsinaPai { get; set; }

        public TipoOrigemColetaEnum TipoOrigemColeta { get; set; }
        public string CodigoPerfilONS { get; set; }
        public int? Limit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? Offset { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Sort { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
