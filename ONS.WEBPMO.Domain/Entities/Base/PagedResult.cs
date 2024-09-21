using System.Runtime.Serialization;

namespace ONS.WEBPMO.Domain.Entities.Base
{
    [DataContract]
    public class PagedResult<TEntity>
    {
        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public IList<TEntity> List { get; set; }

        [DataMember]
        public int TotalCount { get; set; }

        [DataMember]
        public int CurrentPage { get; set; }

        public PagedResult()
        {
        }

        public PagedResult(IList<TEntity> list, int quantity, int currentPage, int pageSize)
        {
            List = list;
            TotalCount = quantity;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }
    }
}
