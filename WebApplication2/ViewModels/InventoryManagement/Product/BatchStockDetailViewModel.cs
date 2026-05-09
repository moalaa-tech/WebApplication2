namespace CRM.WebApp.ViewModels.InventoryManagement.Product
{
    public class BatchStockDetailViewModel
    {
        public string BatchNumber { get; set; }
        public decimal Quantity { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string WarehouseName { get; set; }
    }
}
