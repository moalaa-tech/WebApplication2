using CRM.WebApp.DTOs.InventoryManagement;
using CRM.WebApp.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.InventoryManagment
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var warehouses = await _warehouseService.GetAllAsync();
            return Ok(warehouses);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WarehouseDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _warehouseService.CreateAsync(dto);
            return Ok(dto);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] WarehouseDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _warehouseService.UpdateAsync(dto);
            return Ok(dto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _warehouseService.DeleteAsync(id);
            return NoContent();
        }
    }
}