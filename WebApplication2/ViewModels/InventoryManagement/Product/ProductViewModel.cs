using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.InventoryManagement.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string NameAr { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }

        [DataType(DataType.Currency)]
        public decimal TotalCost { get; set; }

        [DataType(DataType.Currency)]
        public decimal ShippingCost { get; set; }

        [DataType(DataType.Currency)]
        public decimal FreightCost { get; set; }

        public int? ProductTypeId { get; set; }
        public required string ProductTypeName { get; set; }

        public required string ImageFile { get; set; }

        public required string Description { get; set; }
        public decimal CurrentStock { get; internal set; }

        public List<BatchStockDetailViewModel> Batches { get; set; } = new List<BatchStockDetailViewModel>();
    }
}