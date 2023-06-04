﻿using Microsoft.AspNetCore.Mvc;
using Dev.App.ViewModels;
using Dev.Business.Interfaces;
using AutoMapper;
using Dev.Business.Models;

namespace Dev.App.Controllers
{
    public class SuppliersController : BaseController
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        public SuppliersController(ISupplierRepository supplierRepository, IMapper mapper, IAddressRepository addressRepository)
        {
            this._supplierRepository = supplierRepository ?? throw new ArgumentNullException(nameof(supplierRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._addressRepository=addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
        }

        // GET: Suppliers
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.GetAll()));
        }

        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var supplierViewModel = _mapper.Map<SupplierViewModel>(await _supplierRepository.GetByIdWithAddress(id));

            if (supplierViewModel == null)
            {
                return NotFound();
            }

            return View(supplierViewModel);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(supplierViewModel);
            }
            
            await _supplierRepository.Add(_mapper.Map<Supplier>(supplierViewModel));
            return RedirectToAction(nameof(Index));
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var supplierViewModel = _mapper.Map<SupplierViewModel>(await _supplierRepository.GetByIdWithAddress(id));
            if (supplierViewModel == null)
            {
                return NotFound();
            }
            return View(supplierViewModel);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SupplierViewModel supplierViewModel)
        {
            if (id != supplierViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(supplierViewModel);
            }

            await _supplierRepository.Update(_mapper.Map<Supplier>(supplierViewModel));
            return RedirectToAction(nameof(Index));            
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var supplierViewModel = _mapper.Map<SupplierViewModel>(await _supplierRepository.GetById(id));
            if (supplierViewModel == null)
            {
                return NotFound();
            }

            return View(supplierViewModel);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supplier = (await _supplierRepository.QueryReadOnly()).FirstOrDefault(x => x.Id == id);
            if (supplier != null)
            {
                await _supplierRepository.Remove(id);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("UpdateAddress")]
        public async Task<IActionResult> UpdateAddress(Guid id)
        {
            var supplier = await _supplierRepository.GetByIdWithAddress(id);
            var supplierModel = _mapper.Map<SupplierViewModel>(supplier);

            return PartialView("_AddressEditPartial", new SupplierViewModel { Address = supplierModel.Address });
        }

        [HttpPost("UpdateAddress")]
        public async Task<IActionResult> UpdateAddress(AddressViewModel address)
        {
            ModelState.Remove("Name");
            ModelState.Remove("Document");
            if (!ModelState.IsValid)
            {
                return PartialView("_AddressEditPartial", new SupplierViewModel { Address = address });
            }
            var addressDb = _mapper.Map<Address>(address);
            await _addressRepository.Update(addressDb);
            var url = Url.Action(nameof(GetAddress), "Suppliers", new { id = address.Id });
            return Json(new { success = true, url });
        }

        [HttpGet]
        public async Task<IActionResult> GetAddress(Guid id)
        {
            var address = await _addressRepository.GetById(id);
            if(address == null)
            {
                return NotFound();
            }

            return PartialView("_AddressIndexPartial", _mapper.Map<AddressViewModel>(address));
        }

    }
}
