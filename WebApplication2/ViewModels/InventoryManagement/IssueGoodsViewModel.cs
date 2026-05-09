namespace CRM.WebApp.ViewModels.InventoryManagement
{
    public class IssueGoodsViewModel
    {
        public int ItemId { get; set; }
        public int WarehouseId { get; set; }
        public decimal Quantity { get; set; }
        public string Reference { get; set; }

        // For batch tracking
        public int? BatchId { get; set; }
        public string? Barcode { get; set; }
    }
}