using CRM.WebApp.DTOs.InventoryManagement;
using CRM.WebApp.Services.InventoryManagment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.Controllers.InventoryManagment
{
    public class StockController : Controller
    {
        private readonly IStockService _stock;
        public StockController(IStockService stock) { _stock = stock; }

        public async Task<IActionResult> In()
        {
            ViewBag.Products = new SelectList(await _stock.GetProductsAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> In(StockInDto dto)
        {
            if (!ModelState.IsValid) { ViewBag.Products = new SelectList(await _stock.GetProductsAsync(), "Id", "Name"); return View(dto); }
            await _stock.StockInAsync(dto);
            return RedirectToAction(nameof(In));
        }

        public async Task<IActionResult> Out()
        {
            ViewBag.Products = new SelectList(await _stock.GetProductsAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Out(StockOutDto dto)
        {
            if (!ModelState.IsValid) { ViewBag.Products = new SelectList(await _stock.GetProductsAsync(), "Id", "Name"); return View(dto); }
            var ok = await _stock.StockOutAsync(dto);
            if (!ok) ModelState.AddModelError("", "Not enough stock or product not found.");
            ViewBag.Products = new SelectList(await _stock.GetProductsAsync(), "Id", "Name");
            return ok ? RedirectToAction(nameof(Out)) : View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Adjust()
        {
            ViewBag.Products = new SelectList(await _stock.GetProductsAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adjust(StockAdjustmentDto dto)
        {
            if (!ModelState.IsValid) { ViewBag.Products = new SelectList(await _stock.GetProductsAsync(), "Id", "Name"); return View(dto); }
            await _stock.AdjustStockAsync(dto);
            return RedirectToAction(nameof(Adjust));
        }
    }

}
