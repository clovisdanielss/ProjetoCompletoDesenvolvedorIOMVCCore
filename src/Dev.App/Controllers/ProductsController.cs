using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dev.App.ViewModels;
using Dev.Business.Interfaces;
using AutoMapper;
using Dev.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Dev.App.Authorization;

namespace Dev.App.Controllers
{
    [Authorize]
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, ISupplierRepository supplierRepository
            , IMapper mapper, IProductService productService, INotifier notifier) : base(notifier)
        {
            _productRepository=productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _supplierRepository=supplierRepository ?? throw new ArgumentNullException(nameof(supplierRepository));
            _mapper=mapper ?? throw new ArgumentNullException(nameof(mapper));
            _productService=productService ?? throw new ArgumentNullException(nameof(productService));
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetAllWithSuppliers()));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var productViewModel = await GetProductWithAllSuppliers(id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // GET: Products/Create
        [ClaimsAuthorize("Product", "Create")]
        public async Task<IActionResult> Create()
        {
            var productViewModel = await InsertAllSupliersInProduct(new ProductViewModel());
            ViewData["SupplierId"] = new SelectList(productViewModel.AllSuppliers, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Product", "Create")]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["SupplierId"] = new SelectList(await _supplierRepository.GetAll(), "Id", "Name", productViewModel.SupplierId);
                return View(productViewModel);
            }

            var imgPrefix = Guid.NewGuid() + "_";
            if (!await UploadImage(productViewModel.ImageUpload, imgPrefix))
            {
                return View(productViewModel);
            }
            productViewModel.Image = imgPrefix + productViewModel.ImageUpload.FileName;

            var product = _mapper.Map<Product>(productViewModel);
            await _productService.Add(product);
            if (!ValidOperation()) return View(productViewModel);
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        [ClaimsAuthorize("Product", "Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var productViewModel = _mapper.Map<ProductViewModel>(await GetProductWithAllSuppliers(id));
            if (productViewModel == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(productViewModel.AllSuppliers, "Id", "Name", productViewModel.SupplierId);
            return View(productViewModel);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Product", "Edit")]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel productViewModel)
        {
            var product = await _productRepository.GetByIdWithSupplier(id);
            
            if (id != productViewModel.Id || product == null)
            {
                return NotFound();
            }          

            productViewModel.Supplier = _mapper.Map<SupplierViewModel>(product.Supplier);
            productViewModel.Image = product.Image;

            if (!ModelState.IsValid)
            {
                ViewData["SupplierId"] = new SelectList(productViewModel.AllSuppliers, "Id", "Name", productViewModel.SupplierId);
                return View(productViewModel);
            }

            if (productViewModel.ImageUpload != null)
            {
                var imgPrefix = Guid.NewGuid() + "_";
                if (!await UploadImage(productViewModel.ImageUpload, imgPrefix))
                {
                    return View(productViewModel);
                }
                productViewModel.Image = imgPrefix + productViewModel.ImageUpload.FileName;
                product.Image = productViewModel.Image;
            }

            product.Name = productViewModel.Name;
            product.Description= productViewModel.Description;
            product.Value= productViewModel.Value;
            product.Active= productViewModel.Active;

            await _productService.Update(product);
            if (!ValidOperation()) return View(productViewModel);
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Delete/5
        [ClaimsAuthorize("Product", "Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var productViewModel = _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Product", "Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = (await _productRepository.QueryReadOnly()).FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                await _productService.Remove(id);
                if (!ValidOperation()) return View(product);
                TempData["Success"] = "Produto excluido com sucesso!";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<ProductViewModel> GetProductWithAllSuppliers(Guid id)
        {
            var product = _mapper.Map<ProductViewModel>(await _productRepository.GetByIdWithSupplier(id));
            return await InsertAllSupliersInProduct(product);
        }

        private async Task<ProductViewModel> InsertAllSupliersInProduct(ProductViewModel product)
        {
            product.AllSuppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.GetAll());
            return product;
        }
        private async Task<bool> UploadImage(IFormFile imageUpload, string imgPrefix)
        {
            if (imageUpload.Length <= 0) return false;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefix + imageUpload.FileName);
            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe arquivo com esse nome");
                return false;
            }
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageUpload.CopyToAsync(stream);
            }
            return true;
        }
    }
}
