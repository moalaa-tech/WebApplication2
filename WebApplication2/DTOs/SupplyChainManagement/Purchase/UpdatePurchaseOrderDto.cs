namespace CRM.WebApp.DTOs.SupplyChainManagement.Purchase
{
    public class UpdatePurchaseOrderDto
    {
        public int Id { get; set; }
        public string PONumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public int SupplierId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public List<UpdatePurchaseOrderItemDto> Items { get; set; } = new();

    }
}
