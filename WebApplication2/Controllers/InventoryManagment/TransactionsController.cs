using CRM.WebApp.Services.InventoryManagment;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.InventoryManagment
{
    public class TransactionsController : Controller
    {
        private readonly IStockService _stock;
        public TransactionsController(IStockService stock) { _stock = stock; }

        public async Task<IActionResult> Index(Guid? productId)
        {
            //var items = await _stock.GetTransactionsAsync(productId);
            return View();
        }
    }
}
