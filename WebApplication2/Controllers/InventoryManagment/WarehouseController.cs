using CRM.WebApp.DTOs.InventoryManagement;
using CRM.WebApp.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.InventoryManagment
{
    public class WarehouseController : Controller
    {
        private readonly IWarehouseService _warehouseService;


        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }


        public async Task<IActionResult> Index()
        {
            var warehouses = await _warehouseService.GetAllAsync();
            return View(warehouses);
        }


        public IActionResult Create() => View(new WarehouseDto());


        [HttpPost]
        public async Task<IActionResult> Create(WarehouseDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _warehouseService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var warehouse = await _warehouseService.GetByIdAsync(id);
            if (warehouse == null) return NotFound();
            return View(warehouse);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(WarehouseDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _warehouseService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _warehouseService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
