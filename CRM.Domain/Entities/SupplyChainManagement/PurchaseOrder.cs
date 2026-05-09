using CRM.Domain.Base;
using CRM.Domain.Entities.InventoryManagement;
using System.ComponentModel.DataAnnotations;


namespace CRM.Domain.Entities.SupplyChainManagement
{
    public class PurchaseOrder : BaseEntity
    {
        public string PONumber { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }

        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Draft"; // Draft, Approved, Completed, Cancelled


        public virtual ICollection<PurchaseOrderItem> Items { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Notes { get; set; }
        public string OrderNumber { get; set; }
        public string PaymentTerms { get; set; }

        public ICollection<PurchaseOrderLine> Lines { get; set; }
    }

}
