using CRM.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;


namespace CRM.Domain.Entities.SupplyChainManagement
{
    public class PurchaseOrderItem : BaseEntity
    {
        public int PurchaseOrderId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        [NotMapped]
        public decimal TotalPrice => Quantity * UnitPrice;
        
        public string Description { get; set; }
    }
}
