using CRM.WebApp.DTOs.Vendor;
using CRM.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers
{
    public class VendorsController : Controller
    {
        private readonly IVendorService _service;

        public VendorsController(IVendorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            var vendors = await _service.GetAllAsync(search);
            ViewBag.Search = search;
            return View(vendors);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var vendor = await _service.GetByIdAsync(id);
            return vendor == null ? NotFound() : View(vendor);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(VendorDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _service.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vendor = await _service.GetByIdAsync(id);
            return vendor == null ? NotFound() : View(vendor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VendorDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _service.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var vendor = await _service.GetByIdAsync(id);
            return vendor == null ? NotFound() : View(vendor);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
