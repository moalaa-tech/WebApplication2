using CRM.WebApp.DTOs.Accounting;
using CRM.WebApp.Services.Finance_Accounting;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.Accounting
{
    public class ExpenseCategoriesController : Controller
    {
        private readonly IExpenseCategoryService _categoryService;

        public ExpenseCategoriesController(IExpenseCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseCategoryCreateDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _categoryService.CreateAsync(dto);
            TempData["Success"] = "Category created successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();

            var dto = new ExpenseCategoryUpdateDto
            {
                Name = category.Name,
                NameAR = category.NameAR
            };
            ViewBag.Id = id;
            return View(dto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExpenseCategoryUpdateDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var updated = await _categoryService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();

            TempData["Success"] = "Category updated successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteAsync(id);
            TempData["Success"] = "Category deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
