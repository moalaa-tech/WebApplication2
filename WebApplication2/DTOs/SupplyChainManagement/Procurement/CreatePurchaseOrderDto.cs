namespace CRM.WebApp.DTOs.SupplyChainManagement.Procurement
{
    public class CreatePurchaseOrderDto
    {
        public string PONumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public int SupplierId { get; set; }
        public string Description { get; set; }
        public List<CreatePurchaseOrderItemDto> Items { get; set; } = new();

    }
}
