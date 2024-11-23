using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;
using AspNetCore.IQueryable.Extensions;

namespace ONS.WEBPMO.Domain.Entities.Base
{
    public interface IBaseFilter: ICustomQueryable, IQueryPaging, IQuerySort
    {
    }
}
