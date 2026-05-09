namespace CRM.WebApp.DTOs.SupplyChainManagement.Procurement
{
    public class PurchaseOrderDto
    {
        public int Id { get; set; }
        public string PONumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public List<PurchaseOrderItemDto> Items { get; set; } = new();

    }
}
