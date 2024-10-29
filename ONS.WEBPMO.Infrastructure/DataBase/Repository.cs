using AspNetCore.IQueryable.Extensions;
using Microsoft.EntityFrameworkCore;
using ONS.WEBPMO.Domain.Repository.Base;
using ONS.WEBPMO.Infrastructure.Context;
using System.Linq.Expressions;

namespace ONS.WEBPMO.Infrastructure.DataBase
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> Query { get; set; }
        protected DbContext Context { get; set; }
        public Repository(WEBPMODbContext context)
        {
            this.Context = context;
            this.Query = Context.Set<T>();
        }

        public async Task<T> GetByIdAsync<TKey>(TKey id)
        {
            return await Query.FindAsync(id);
        }
        public async Task AddAsync(T entity)
        {
            await Query.AddAsync(entity);
            await Context.SaveChangesAsync();
        }
        public async Task<IEnumerable<T>> FindAllByCriterio(Expression<Func<T, bool>> expression)
        {
            var teste = await this.Query.Where(expression).ToListAsync();
            return teste;
        }
        public async Task<ICollection<T>> GetAllAsync()
        {
            return await Query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await Query.FindAsync(id);
        }

        public async Task RemoveAsync(T entity)
        {
            Query.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            Query.Update(entity);
            await Context.SaveChangesAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return Query.AnyAsync(expression);
        }

        public async Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression)
        {
            return await Query.FirstOrDefaultAsync(expression);
        }
               

        public IQueryable<T> GetByQueryable(ICustomQueryable filter)
        {
            return Query.AsQueryable().Apply(filter);
        }

        IQueryable<T> IRepository<T>.Query()
        {
            return Query;
        }
    }
}
