using AspNetCore.IQueryable.Extensions;
using Microsoft.EntityFrameworkCore;
using ONS.WEBPMO.Domain.Repository.Base;
using ONS.WEBPMO.Infrastructure.Context;
using System.Linq.Expressions;

namespace ONS.WEBPMO.Infrastructure.DataBase
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> _query { get; set; }
        protected DbContext _context { get; set; }

        public Repository(WEBPMODbContext context)
        {
            _context = context;
            _query = _context.Set<T>();
        }

        public async Task SaveAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "A entidade não pode ser nula.");
            await _query.AddAsync(entity);
            await _context.SaveChangesAsync();
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

        public async ValueTask<T> GetAsync(object id)
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
    }
}
