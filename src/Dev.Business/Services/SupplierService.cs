using Dev.Business.Interfaces;
using Dev.Business.Models;
using Dev.Business.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Business.Services
{
    /// <summary>
    /// Só fiz uso de validações básicas. Também era necessário implementar 
    /// validações de banco. Por exemplo, se existe produto com o mesmo nome, ou
    /// se existe fornecedor com o mesmo documento.
    /// </summary>
    public class SupplierService : BaseService, ISupplierService
    {
        private readonly SupplierValidator _supplierValidator;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly AddressValidator _addressValidator;
        public SupplierService(ISupplierRepository supplierRepository, IAddressRepository addressRepository, INotifier notifier) : base(notifier)
        {
            _supplierValidator = new SupplierValidator();
            _addressValidator = new AddressValidator();
            _supplierRepository=supplierRepository ?? throw new ArgumentNullException(nameof(supplierRepository));
            _addressRepository=addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
        }
        public async Task Add(Supplier supplier)
        {
            if (!Validate(_supplierValidator, supplier) || !Validate(_addressValidator, supplier.Address)) return;

            await _supplierRepository.Add(supplier);
        }

        public void Dispose()
        {
            _supplierRepository?.Dispose();
        }

        public async Task Remove(Guid id)
        {
            await _supplierRepository.Remove(id);
        }

        public async Task Update(Supplier supplier)
        {
            if (!Validate(_supplierValidator, supplier) || !Validate(_addressValidator, supplier.Address)) return;

            await _supplierRepository.Update(supplier);
        }

        public async Task UpdateAddress(Address address)
        {
            if (!Validate(_addressValidator, address)) return;

            await _addressRepository.Update(address);
        }
    }
}
