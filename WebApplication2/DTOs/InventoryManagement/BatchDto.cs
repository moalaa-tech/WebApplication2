namespace CRM.WebApp.DTOs.InventoryManagement
{
    public class BatchDto
    {
        public int Id { get; set; }
        public string BatchNumber { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int ProductId { get; set; }
        public string WarehouseName { get; set; }
        public int WarehouseId { get; set; }
        public decimal InitialQuantity { get; set; }
        public decimal QuantityOnHand { get; set; }
    }
}