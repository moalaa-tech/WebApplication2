using AutoMapper;
using CRM.WebApp.DTOs.DocumentManagement;
using CRM.WebApp.Services.DocumentManagement;
using CRM.WebApp.ViewModels.DocumentManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.Controllers.DocumentManagement
{
    public class DocumentCategoriesController : Controller
    {
        private readonly IDocumentCategoryService _categoryService;
        private readonly IMapper _mapper;

        public DocumentCategoriesController(IDocumentCategoryService categoryService, IMapper _mapper)
        {
            _categoryService = categoryService;
            this._mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var result = _mapper.Map<List<DocumentCategoryViewModel>>(categories);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.ParentCategories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDocumentCategoryDto createDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.CreateCategoryAsync(createDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error creating category: {ex.Message}");
                }
            }

            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.ParentCategories = new SelectList(categories, "Id", "Name");
            return View(createDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.ParentCategories = new SelectList(categories.Where(c => c.Id != id), "Id", "Name", category.ParentId);

            var updateDto = new UpdateDocumentCategoryDto
            {
                Name = category.Name,
                Description = category.Description,
                ParentCategoryId = category.ParentId,
                IsActive = category.IsActive,
                Order = category.Order
            };

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateDocumentCategoryDto updateDto)
        {
            if (id != updateDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateCategoryAsync(id, updateDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating category: {ex.Message}");
                }
            }

            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.ParentCategories = new SelectList(categories.Where(c => c.Id != id), "Id", "Name", updateDto.ParentCategoryId);
            return View(updateDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result)
            {
                TempData["ErrorMessage"] = "Cannot delete category. It may have associated documents or subcategories.";
                return RedirectToAction(nameof(Delete), new { id });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
