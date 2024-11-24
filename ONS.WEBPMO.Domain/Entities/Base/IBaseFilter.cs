using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;

namespace ONS.WEBPMO.Domain.Entities.Base
{
    public interface IBaseFilter : ICustomQueryable, IQueryPaging, IQuerySort
    {
    }
}
