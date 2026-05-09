using CRM.Domain.Base;
using CRM.Domain.Entities.MarketingAutomation.EasyOrder;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Entities.InventoryManagement
{
    public class Product : BaseEntity
    {
        public  string Name { get; set; }
        public  string NameAr { get; set; }
        public decimal TotalCost { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal FreightCost { get; set; }
        public int? ProductTypeId { get; set; }
        public ProductType? ProductType { get; set; }
        public string? ImageFile { get; set; }
        public string? Description { get; set; }
        public int QuantityInStock { get; set; }

        public ICollection<ProductVariant>? ProductVariant { get; set; }

        public int? ReorderRuleId { get; set; }
        public ReorderRule? ReorderRule { get; set; }
        public Guid? EasyOrderProductId { get; set; }
        public EasyOrderProduct? EasyOrderProduct { get; set; }

        public ICollection<StockTransaction>? StockTransactions { get; set; }

        // New: batches and barcodes
        public ICollection<Batch> Batches { get; set; } = new List<Batch>();
        public ICollection<ProductBarcode> Barcodes { get; set; } = new List<ProductBarcode>();
    }
}
