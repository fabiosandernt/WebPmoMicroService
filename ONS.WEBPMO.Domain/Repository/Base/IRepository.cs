using AspNetCore.IQueryable.Extensions;
using System.Linq.Expressions;


namespace ONS.WEBPMO.Domain.Repository.Base
{
    public interface IRepository<T> where T : class
    {
        
        Task SaveAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        T Get(object id);
        ValueTask<T> GetAsync(object id);
        Task<IEnumerable<T>> GetAllAsync();
        IList<T> GetAll();
        Task<IEnumerable<T>> FindAllByCriterioAsync(Expression<Func<T, bool>> expression);
        ValueTask<T> FindOneByCriterioAsync(Expression<Func<T, bool>> expression);
        ValueTask<bool> Any(Expression<Func<T, bool>> expression);
        IQueryable<T> GetByQueryableExtension(ICustomQueryable filter);
        IQueryable<T> Query();

    }
}
