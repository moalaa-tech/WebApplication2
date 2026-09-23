using AutoMapper;
using CRM.WebApp.DTOs.DocumentManagement;
using CRM.WebApp.Services.DocumentManagement;
using CRM.WebApp.ViewModels.DocumentManagement;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.DocumentManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentCategoriesController : ControllerBase
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
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDocumentCategoryDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _categoryService.CreateCategoryAsync(createDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error creating category: {ex.Message}" });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateDocumentCategoryDto updateDto)
        {
            if (id != updateDto.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _categoryService.UpdateCategoryAsync(id, updateDto);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error updating category: {ex.Message}" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result)
            {
                return BadRequest(new { message = "Cannot delete category. It may have associated documents or subcategories." });
            }

            return Ok();
        }
    }
}