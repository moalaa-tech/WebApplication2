namespace CRM.WebApp.DTOs.SupplyChainManagement.Procurement
{
    public class PurchaseOrderItemDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string Description { get; set; }

    }
}
