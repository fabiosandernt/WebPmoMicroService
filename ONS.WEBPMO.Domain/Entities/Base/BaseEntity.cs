

namespace ONS.WEBPMO.Domain.Entities.Base
{
    public abstract class BaseEntity<T>
    {
        public virtual T Id { get; set; }
    }
}
