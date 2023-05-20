using Dev.Business.Models;

namespace Dev.Business.Interfaces
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        Task Add(T item);
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task Update(T item);
        Task Remove(Guid id);
        Task<IQueryable<T>> Query();
        Task<IQueryable<T>> QueryReadOnly();
        Task<int> SaveChanges();
    }
}
