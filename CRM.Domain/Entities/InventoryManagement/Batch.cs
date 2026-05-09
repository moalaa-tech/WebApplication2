using CRM.Domain.Base;

namespace CRM.Domain.Entities.InventoryManagement
{
    public class Batch : BaseEntity
    {
        public required string BatchNumber { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int? ProductVariantId { get; set; }
        public ProductVariant? ProductVariant { get; set; }

        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }

        public decimal InitialQuantity { get; set; }
        public decimal QuantityOnHand { get; set; }

        public ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
    }
}