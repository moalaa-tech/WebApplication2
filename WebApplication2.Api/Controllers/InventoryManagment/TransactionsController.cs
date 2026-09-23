using CRM.WebApp.Services.InventoryManagment;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.InventoryManagment
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IStockService _stock;
        public TransactionsController(IStockService stock) { _stock = stock; }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Guid? productId)
        {
            //var items = await _stock.GetTransactionsAsync(productId);
            return Ok();
        }
    }
}