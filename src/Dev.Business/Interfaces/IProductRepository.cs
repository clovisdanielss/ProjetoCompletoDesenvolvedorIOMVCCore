using Dev.Business.Models;

namespace Dev.Business.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllWithSuppliers();
    }
}
