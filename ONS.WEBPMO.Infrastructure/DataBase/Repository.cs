using AspNetCore.IQueryable.Extensions;
using Microsoft.EntityFrameworkCore;
using ONS.Common.Entities;
using ONS.WEBPMO.Domain.Repository.Base;
using ONS.WEBPMO.Infrastructure.Context;
using System.Linq.Expressions;

namespace ONS.WEBPMO.Infrastructure.DataBase
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> _query { get; set; }
        protected DbContext _context { get; set; }
        private const bool ConcurrencyCheckEnabled = true; // Altere para false para desabilitar a verificação de concorrência

        public Repository(WEBPMODbContext context)
        {
            _context = context;
            _query = _context.Set<T>();
        }

        public async Task SaveAsync(T entity)
        {            
            await _query.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public void Add(T entity)
        {          
             _query.Add(entity);
             _context.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            _query.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _query.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async ValueTask<T> GetByIdAsync(object id)
        {
            return await _query.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                var consulta = await _query.ToListAsync();
                return consulta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<T>> FindAllByCriterioAsync(Expression<Func<T, bool>> expression)
        {
            return await _query.Where(expression).ToListAsync();
        }

        public async ValueTask<T> FindOneByCriterioAsync(Expression<Func<T, bool>> expression)
        {
            return await _query.FirstOrDefaultAsync(expression);
        }

        public async ValueTask<bool> Any(Expression<Func<T, bool>> expression)
        {
            return await _query.AnyAsync(expression);
        }

        public IQueryable<T> GetByQueryableExtension(ICustomQueryable filter)
        {
            return _query.AsQueryable().Apply(filter);
        }

        IQueryable<T> IRepository<T>.Query()
        {
            return _query.AsQueryable();
        }

        public IList<T> GetAll()
        {
            var consulta = _query.ToList();
            return consulta;
        }

        public T FindByKey(object id)
        {
            var consulta = _query.Find(id);
            return consulta;
        }

        public T FindByKeyConcurrencyValidate(object key, object version, bool checkDeletedEntity = true)
        {
            // Busca a entidade pelo ID
            var entityFinded = _query.Find(key);

            // Se a entidade não for encontrada e 'checkDeletedEntity' for verdadeiro, lança uma exceção
            if (entityFinded == null && checkDeletedEntity)
            {
                throw new DbUpdateConcurrencyException("Entity not found. It may have been deleted.");
            }

            // Valida a concorrência utilizando a versão fornecida
            ValidateConcurrency(entityFinded, version, checkDeletedEntity);

            return entityFinded;
        }

        public void ValidateConcurrency(T entity, object version, bool checkDeletedEntity = true)
        {
            if (ConcurrencyCheckEnabled)
            {
                if (entity != null)
                {
                    var entityVersion = entity.GetType().GetProperty("RowVersion")?.GetValue(entity);
                    if (entityVersion == null || !entityVersion.Equals(version))
                    {
                        throw new DbUpdateConcurrencyException("Entity version mismatch. Concurrency check failed.");
                    }
                }
            }
        }

        public void ValidateConcurrency(object entityKey, object version, bool checkDeletedEntity = true)
        {
            // Verifica se a entidade existe
            var entityFinded = _query.Find(entityKey);

            if (entityFinded == null && checkDeletedEntity)
            {
                throw new DbUpdateConcurrencyException("Entity not found. It may have been deleted.");
            }

            ValidateConcurrency(entityFinded, version, checkDeletedEntity);
        }

        public void Delete(T entity)
        {
            _query.Remove(entity);
            _context.SaveChanges();
        }

        public void Delete(IEnumerable<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException(nameof(entities), "A coleção de entidades não pode ser nula ou vazia.");
            }

            _query.RemoveRange(entities);
            _context.SaveChanges();
        }

        /// <summary>
        /// Adiciona as entidades <paramref name="entities"/> ao contexto e executa o query no BD imediatamente (mas não realiza commit imediatamente, realiza commit normalmente como definido pelo IoC)
        /// </summary>
        /// <param name="entities">Entidades a serem salvas.</param>
        /// <param name="saveChanges">Indica se as alterações devem ser salvas imediatamente no banco de dados.</param>
        public virtual void Add(IEnumerable<T> entities, bool saveChanges = false)
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = false;

            foreach (T entity in entities)
            {
                Add(entity);
            }

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Adiciona as entidades <paramref name="entities"/> ao contexto e executa o query no BD imediatamente (mas não realiza commit imediatamente, realiza commit normalmente como definido pelo IoC)
        /// </summary>
        /// <param name="entities">Entidades a serem salvas.</param>
        public virtual void Add(params T[] entities)
        {
            Add(entities.AsEnumerable());
        }

        public IList<T> All(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            var consultaOrdenada = orderBy(_query.AsQueryable());
            return consultaOrdenada.ToList();
        }

        public void Add(T entity, bool saveChanges = false)
        {
            _query.Add(entity); 

            if (saveChanges)
            {
                _context.SaveChanges(); 
            }
        }

        public void DeleteByKey(object key)
        {
            
            var entity = _query.Find(key);
                        
            if (entity == null)
            {
                throw new ArgumentException($"Entity with key {key} not found.");
            }
            
            _query.Remove(entity);
            
            _context.SaveChanges();
        }

    }
}
