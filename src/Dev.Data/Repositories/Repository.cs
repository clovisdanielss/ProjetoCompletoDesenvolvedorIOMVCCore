using Dev.Business.Interfaces;
using Dev.Business.Models;
using Dev.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Dev.Data.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity, new()
    {
        protected readonly DataContext context;
        protected readonly DbSet<T> entitySet;

        protected Repository(DataContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            entitySet = context.Set<T>();
        }

        public async Task Add(T item)
        {
            entitySet.Add(item);
            await SaveChanges();
        }

        public void Dispose()
        {
            context?.Dispose();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await entitySet.ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await entitySet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IQueryable<T>> Query()
        {
            return entitySet.AsQueryable();
        }

        public async Task<IQueryable<T>> QueryReadOnly()
        {
            return entitySet.AsNoTracking().AsQueryable();
        }

        public async Task Remove(Guid id)
        {
            entitySet.Remove(new T { Id = id });
        }

        public async Task<int> SaveChanges()
        {
            var result = await context.SaveChangesAsync();
            return result;
        }

        public async Task Update(T item)
        {
            entitySet.Update(item);
        }
    }
}
