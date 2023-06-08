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
    public class ProductService : BaseService, IProductService
    {
        private readonly ProductValidator _productValidator;
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository, INotifier notifier): base(notifier)
        {
            _productRepository=productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _productValidator=new ProductValidator();
        }
        public async Task Add(Product product)
        {
            if (!Validate(_productValidator, product)) return;

            await _productRepository.Add(product);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }

        public async Task Remove(Guid id)
        {
            await _productRepository.Remove(id);
        }

        public async Task Update(Product product)
        {
            if (!Validate(_productValidator, product)) return;

            await _productRepository.Update(product);
        }
    }
}
