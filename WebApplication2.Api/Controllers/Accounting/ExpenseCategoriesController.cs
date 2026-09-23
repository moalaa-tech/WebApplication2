using CRM.WebApp.DTOs.Accounting;
using CRM.WebApp.Services.Finance_Accounting;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.Accounting
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseCategoriesController : ControllerBase
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
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExpenseCategoryCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _categoryService.CreateAsync(dto);
            return Ok(dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ExpenseCategoryUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _categoryService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();

            return Ok(dto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}