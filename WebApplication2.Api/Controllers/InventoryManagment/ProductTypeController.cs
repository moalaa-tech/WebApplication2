using AutoMapper;
using CRM.WebApp.DTOs.InventoryManagement.ProductType;
using CRM.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.InventoryManagment
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeService _service;
        private readonly IMapper _mapper;

        public ProductTypeController(IProductTypeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductTypeDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _service.CreateAsync(dto);
            return Ok(dto);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] UpdateProductTypeDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _service.UpdateAsync(dto);
            return Ok(dto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}