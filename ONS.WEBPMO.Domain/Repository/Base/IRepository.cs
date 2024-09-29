using AspNetCore.IQueryable.Extensions;
using System.Linq.Expressions;


namespace ONS.WEBPMO.Domain.Repository.Base
{
    public interface IRepository<T> where T : class
    {        
        Task SaveAsync(T entity);
        Task DeleteAsync(T entity);        
        Task UpdateAsync(T entity);
        T FindByKey(object id);
        ValueTask<T> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetAllAsync();
        IList<T> GetAll();
        Task<IEnumerable<T>> FindAllByCriterioAsync(Expression<Func<T, bool>> expression);
        ValueTask<T> FindOneByCriterioAsync(Expression<Func<T, bool>> expression);
        ValueTask<bool> Any(Expression<Func<T, bool>> expression);
        IQueryable<T> GetByQueryableExtension(ICustomQueryable filter);
        IQueryable<T> Query();        
        T FindByKeyConcurrencyValidate(object key, object version, bool checkDeletedEntity = true);
        void ValidateConcurrency(T entity, object version, bool checkDeletedEntity = true);       
        void ValidateConcurrency(object entityKey, object version, bool checkDeletedEntity = true);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        void Add(T entity, bool saveChanges = false);

        /// <summary>
        /// Adiciona as entidades <paramref name="entities"/> ao contexto e executa o query no BD imediatamente (mais não realiza commit imediatamente, realiza commit normalmente como definido pelo IoC)
        /// </summary>
        /// <param name="entities">Entidades a serem salvas.</param>
        void Add(params T[] entities);

        /// <summary>
        /// Adiciona as entidades <paramref name="entities"/> ao contexto e executa o query no BD imediatamente (mais não realiza commit imediatamente, realiza commit normalmente como definido pelo IoC)
        /// </summary>
        /// <param name="entities">Entidades a serem salvas.</param>
        /// <param name="saveChanges"></param>
        void Add(IEnumerable<T> entities, bool saveChanges = false);

    }
}
