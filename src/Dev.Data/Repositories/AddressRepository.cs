using Dev.Business.Interfaces;
using Dev.Business.Models;
using Dev.Data.Context;

namespace Dev.Data.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(DataContext context) : base(context)
        {
        }
    }
}
