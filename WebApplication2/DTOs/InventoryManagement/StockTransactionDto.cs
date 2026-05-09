using CRM.Domain.Enums;

namespace CRM.WebApp.DTOs.InventoryManagement
{
    public class StockTransactionDto
    {
        public int ItemId { get; set; }
        public int WarehouseId { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Quantity { get; set; }
        public string Reference { get; set; }
        public string Note { get; set; }

        // Optional batch/expiry/barcode inputs for inventory transactions
        public string? BatchNumber { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? Barcode { get; set; }
        public int? BatchId { get; internal set; }
    }
}
