using AutoMapper;
using CRM.Domain.Enums;
using CRM.WebApp.DTOs.InventoryManagement;
using CRM.WebApp.DTOs.InventoryManagement.Product;
using CRM.WebApp.Paging;
using CRM.WebApp.Services.Inventory;
using CRM.WebApp.Services.InventoryManagment;
using CRM.WebApp.ViewModels.InventoryManagement;
using CRM.WebApp.ViewModels.InventoryManagement.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuestPDF.Helpers;
using System.Threading.Tasks;

namespace CRM.WebApp.Controllers.InventoryManagment
{
    public class InventoryController : Controller
    {
        private readonly IInventoryService InventoryService;
        private IWarehouseService WarehouseService;
        private readonly IMapper Mapper;
        public InventoryController(IInventoryService _InventoryService, IMapper _Mapper, IWarehouseService warehouseService)
        {
            InventoryService = _InventoryService;
            Mapper = _Mapper;
            WarehouseService = warehouseService;
        }


        [HttpGet]
        public IActionResult dashboard()
        {            
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            var dtos = await InventoryService.GetItemsAsync(pageIndex);

            foreach (var item in dtos)
            {
                item.CurrentStock = await InventoryService.GetCurrentStockAsync(item.Id);
            }

            return View(dtos);
        }

        [HttpGet]
        public async Task<IActionResult> ReorderSuggestions()
        {
            var suggestions = await InventoryService.GetReorderSuggestionsAsync();
            return View(suggestions);
        }

        [HttpGet]
        public async Task<IActionResult> BatchDetails(int itemId, int? warehouseId = null)
        {
            var batches = await InventoryService.GetBatchesForItemAsync(itemId, warehouseId);
            return View(batches);
        }

        [HttpGet]
        public async Task<IActionResult> ScanBarcode()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ScanBarcode(string barcode)
        {
            if (string.IsNullOrEmpty(barcode))
            {
                return View();
            }

            var product = await InventoryService.LookupProductByBarcodeAsync(barcode);
            if (product == null)
            {
                ModelState.AddModelError("", "No product found with this barcode");
                return View();
            }

            return RedirectToAction("Details", new { id = product.Id });
        }


        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var result =await InventoryService.GetItemsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            //await InventoryService.UpdateProductAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateItem()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateItem(CreateProductViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var dto = Mapper.Map<ProductDto>(vm);
            await InventoryService.CreateItemAsync(dto);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> ReceiveGoods()
        {
            var vm = new ReceiveGoodsViewModel();
            await PopulateReceiveGoodsDropdowns(vm);
            return View(vm);
        }

        private async Task PopulateReceiveGoodsDropdowns(ReceiveGoodsViewModel vm)
        {
            var items = await InventoryService.GetItemsAsync(1, 1000);
            vm.Items = items?.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.Name
            }).ToList() ?? new List<SelectListItem>();

            var warehouses = await InventoryService.GetWarehousesAsync();
            vm.Warehouses = warehouses?.Select(w => new SelectListItem
            {
                Value = w.Id.ToString(),
                Text = w.Name
            }).ToList() ?? new List<SelectListItem>();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReceiveGoods(ReceiveGoodsViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await PopulateReceiveGoodsDropdowns(vm);
                return View(vm);
            }
            var dto = new StockTransactionDto
            {
                ItemId = vm.ItemId,
                WarehouseId = vm.WarehouseId,
                Quantity = vm.Quantity,
                TransactionType = TransactionType.In,
                Reference = vm.Reference,
                BatchNumber = vm.BatchNumber,
                ManufactureDate = vm.ManufactureDate,
                ExpiryDate = vm.ExpiryDate,
                Barcode = vm.Barcode
            };
            await InventoryService.ReceiveGoodsAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult IssueGoods() => View(new IssueGoodsViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IssueGoods(IssueGoodsViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var dto = new StockTransactionDto
            {
                ItemId = vm.ItemId,
                WarehouseId = vm.WarehouseId,
                Quantity = vm.Quantity,
                TransactionType = TransactionType.Out,
                Reference = vm.Reference,
                BatchId = vm.BatchId,
                Barcode = vm.Barcode
            };
            await InventoryService.IssueGoodsAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await InventoryService.GetItemsAsync(1, id);
            if (product == null || !product.Any())
            {
                return NotFound();
            }

            var item = product.First();
            item.CurrentStock = await InventoryService.GetCurrentStockAsync(id);
            var batches = await InventoryService.GetBatchesForItemAsync(id);

            var viewModel = new ProductDetailsViewModel
            {
                Product = item,
                Batches = batches
            };

            return View(viewModel);

            //var dto = new StockTransactionDto
            //{
            //    ItemId = viewModel.Product.Id,
            //    WarehouseId = viewModel.Batches.WarehouseId,
            //    Quantity = vm.Quantity,
            //    TransactionType = TransactionType.Out,
            //    Reference = vm.Reference,
            //    BatchNumber = vm.BatchNumber,
            //    Barcode = vm.Barcode
            //};
            //await InventoryService.IssueGoodsAsync(dto);
            //return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> LookupByBarcode(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return BadRequest();
            var dto = await InventoryService.LookupProductByBarcodeAsync(code);
            if (dto == null) return NotFound();
            return View(dto);
        }


        //[HttpGet]
        //public async Task<IActionResult> Details(int id)
        //{
        //    var dto = (await InventoryService.GetItemsAsync()).FirstOrDefault(i => i.Id == id);
        //    if (dto == null) return NotFound();

        //    var vm = Mapper.Map<ProductViewModel>(dto);
        //    vm.CurrentStock = await InventoryService.GetCurrentStockAsync(id);
        //    return View(vm);
        //}
    }
}
