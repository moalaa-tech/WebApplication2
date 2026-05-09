using CRM.Domain.Base;


namespace CRM.Domain.Entities.InventoryManagement
{
    public class PurchaseOrderLine : BaseEntity
    {
        public int PurchaseOrderId { get; set; }
        public Purchase PurchaseOrder { get; set; }

        public int ItemId { get; set; }
        public Product Item { get; set; }

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
