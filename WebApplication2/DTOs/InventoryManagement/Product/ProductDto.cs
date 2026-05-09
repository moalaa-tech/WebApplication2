using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.InventoryManagement.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? NameAr { get; set; }

        public decimal? TotalCost { get; set; }

        public decimal? ShippingCost { get; set; }

        public decimal? FreightCost { get; set; }
        public decimal? SalesPrice { get; set; }


        public int? ProductTypeId { get; set; }
        public string? ProductTypeName { get; set; }

        public string? ImageFile { get; set; }

        public string? Description { get; set; }


        public int CurrentStock { get; set; }
        public int MinimumStockLevel { get; set; }

        public string? SKU { get; set; }
    }
}
