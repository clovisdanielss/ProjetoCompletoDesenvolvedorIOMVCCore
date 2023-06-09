﻿using Dev.Business.Models;

namespace Dev.Business.Interfaces
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<Supplier> GetByIdWithAddress(Guid id);
    }
}
