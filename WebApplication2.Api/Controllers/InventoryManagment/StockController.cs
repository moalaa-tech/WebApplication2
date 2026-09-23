using CRM.WebApp.DTOs.InventoryManagement;
using CRM.WebApp.Services.InventoryManagment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication2.Api.Controllers.InventoryManagment
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stock;
        public StockController(IStockService stock) { _stock = stock; }

        [HttpGet("In")]
        public async Task<IActionResult> In()
        {
            var products = new SelectList(await _stock.GetProductsAsync(), "Id", "Name");
            return Ok(products);
        }

        [HttpPost("In")]
        public async Task<IActionResult> In([FromBody] StockInDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _stock.StockInAsync(dto);
            return Ok(dto);
        }

        [HttpGet("Out")]
        public async Task<IActionResult> Out()
        {
            var products = new SelectList(await _stock.GetProductsAsync(), "Id", "Name");
            return Ok(products);
        }

        [HttpPost("Out")]
        public async Task<IActionResult> Out([FromBody] StockOutDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var ok = await _stock.StockOutAsync(dto);
            if (!ok) ModelState.AddModelError("", "Not enough stock or product not found.");
            return ok ? Ok(dto) : BadRequest(ModelState);
        }

        [HttpGet("Adjust")]
        public async Task<IActionResult> Adjust()
        {
            var products = new SelectList(await _stock.GetProductsAsync(), "Id", "Name");
            return Ok(products);
        }

        [HttpPost("Adjust")]
        public async Task<IActionResult> Adjust([FromBody] StockAdjustmentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _stock.AdjustStockAsync(dto);
            return Ok(dto);
        }
    }
}