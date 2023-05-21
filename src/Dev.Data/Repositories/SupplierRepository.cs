using Dev.Business.Interfaces;
using Dev.Business.Models;
using Dev.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Dev.Data.Repositories
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(DataContext context) : base(context)
        {
        }

        public async Task<Supplier> GetByIdWithAddress(Guid id)
        {
            return await (await Query()).Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
