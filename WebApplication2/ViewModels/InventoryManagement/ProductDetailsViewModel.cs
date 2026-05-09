using CRM.WebApp.DTOs.InventoryManagement;
using CRM.WebApp.DTOs.InventoryManagement.Product;

namespace CRM.WebApp.ViewModels.InventoryManagement
{
    public class ProductDetailsViewModel
    {
        public ProductDto Product { get; set; }
        public IEnumerable<BatchDto> Batches { get; set; }
    }
}