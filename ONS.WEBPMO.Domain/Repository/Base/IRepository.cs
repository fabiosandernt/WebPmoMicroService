using AspNetCore.IQueryable.Extensions;
using System.Linq.Expressions;


namespace ONS.WEBPMO.Domain.Repository.Base
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync<TKey>(TKey id);
        Task<ICollection<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> GetByQueryable(ICustomQueryable filter);
        IQueryable<T> Query();
        Task<IEnumerable<T>> FindAllByCriterio(Expression<Func<T, bool>> expression);

    }
}
