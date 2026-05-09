using AutoMapper;
using CRM.WebApp.DTOs.SupplyChainManagement.Procurement;
using CRM.WebApp.DTOs.SupplyChainManagement.Purchase;
using CRM.WebApp.Services.SupplyChainManagement;
using CRM.WebApp.ViewModels.SupplyChainManagement.Purchase;
using CRM.WebApp.ViewModels.SupplyChainManagement.Supplier;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.SupplyChainManagement
{
    public class PurchaseOrdersController : Controller
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;

        public PurchaseOrdersController(
            IPurchaseOrderService purchaseOrderService,
            ISupplierService supplierService,
            IMapper mapper)
        {
            _purchaseOrderService = purchaseOrderService;
            _supplierService = supplierService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var purchaseOrders = await _purchaseOrderService.GetAllPurchaseOrdersAsync();
            var viewModels = _mapper.Map<IEnumerable<PurchaseOrderViewModel>>(purchaseOrders);
            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var purchaseOrder = await _purchaseOrderService.GetPurchaseOrderByIdAsync(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<PurchaseOrderViewModel>(purchaseOrder);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new CreatePurchaseOrderViewModel
            {
                OrderDate = DateTime.Today,
                AvailableSuppliers = await GetSupplierDropdownList()
            };

            // Add a default empty item
            viewModel.Items.Add(new CreatePurchaseOrderItemViewModel());

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePurchaseOrderViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var createDto = _mapper.Map<CreatePurchaseOrderDto>(viewModel);
                    await _purchaseOrderService.CreatePurchaseOrderAsync(createDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            // Reload dropdown data if validation fails
            viewModel.AvailableSuppliers = await GetSupplierDropdownList();
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var purchaseOrder = await _purchaseOrderService.GetPurchaseOrderByIdAsync(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<EditPurchaseOrderViewModel>(purchaseOrder);
            viewModel.AvailableSuppliers = await GetSupplierDropdownList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditPurchaseOrderViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var updateDto = _mapper.Map<UpdatePurchaseOrderDto>(viewModel);
                    await _purchaseOrderService.UpdatePurchaseOrderAsync(updateDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            viewModel.AvailableSuppliers = await GetSupplierDropdownList();
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var purchaseOrder = await _purchaseOrderService.GetPurchaseOrderByIdAsync(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<PurchaseOrderViewModel>(purchaseOrder);
            return View(viewModel);
        }

        // POST: PurchaseOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _purchaseOrderService.DeletePurchaseOrderAsync(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id, string status)
        {
            try
            {
                await _purchaseOrderService.ChangeStatusAsync(id, status);
                TempData["SuccessMessage"] = $"Status changed to {status} successfully.";
            }
            catch (ArgumentException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Details), new { id });
        }


        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyPONumber(string poNumber, int id = 0)
        {
            var exists = await _purchaseOrderService.PONumberExistsAsync(poNumber, id == 0 ? null : id);
            return Json(!exists);
        }

        // Helper method to get supplier dropdown list
        private async Task<List<SupplierDropdownViewModel>> GetSupplierDropdownList()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return _mapper.Map<List<SupplierDropdownViewModel>>(suppliers);
        }

        // Partial: Add new item row (for AJAX)
        public IActionResult AddItemRow()
        {
            return PartialView("_PurchaseOrderItemRow", new CreatePurchaseOrderItemViewModel());
        }

    }
}
