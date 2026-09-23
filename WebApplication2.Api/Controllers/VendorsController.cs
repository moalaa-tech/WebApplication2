using CRM.WebApp.DTOs.Vendor;
using CRM.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorService _service;

        public VendorsController(IVendorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string? search)
        {
            var vendors = await _service.GetAllAsync(search);
            return Ok(vendors);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var vendor = await _service.GetByIdAsync(id);
            return vendor == null ? NotFound() : Ok(vendor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VendorDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _service.AddAsync(dto);
            return Ok(dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] VendorDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _service.UpdateAsync(dto);
            return Ok(dto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}