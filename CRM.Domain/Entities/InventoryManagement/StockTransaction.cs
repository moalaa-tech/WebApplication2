using CRM.Domain.Enums;


namespace CRM.Domain.Entities.InventoryManagement
{
    public class StockTransaction
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public  Product? Item { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Quantity { get; set; }
        public decimal? Cost { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Reference { get; set; } // e.g., PO#, SO#, ADJ
        public string Note { get; set; }

        // New: optional link to batch and barcode used in this transaction
        public int? BatchId { get; set; }
        public Batch? Batch { get; set; }
        public string? ScannedBarcode { get; set; }
    }
}
