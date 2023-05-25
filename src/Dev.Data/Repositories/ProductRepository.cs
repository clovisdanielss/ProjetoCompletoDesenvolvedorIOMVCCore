using Dev.Business.Interfaces;
using Dev.Business.Models;
using Dev.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Dev.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetAllWithSuppliers()
        {
            return await (await Query()).Include(x => x.Supplier).ToListAsync();
        }

        public async Task<Product> GetByIdWithSupplier(Guid id)
        {
            return await (await Query()).Include(x => x.Supplier).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
