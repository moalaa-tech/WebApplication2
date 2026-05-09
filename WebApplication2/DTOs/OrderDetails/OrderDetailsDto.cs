using CRM.WebApp.DTOs.InventoryManagement.Product;

namespace CRM.WebApp.DTOs.OrderDetails
{
    public class OrderDetailsDto
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }
        public ProductDto Product { get; set; }

        public int ItemsCount { get; set; }

        public long UTMCampaign { get; set; }
        public string? Note { get; set; }
        public string? UTMSource { get; set; }

    }
}
