using CRM.Domain.Base;
using CRM.Domain.Entities.InventoryManagement;

namespace CRM.Domain.Entities.SalesManagement
{
    public class QuoteLineItem : BaseEntity
    {
        public int QuoteId { get; set; }
        public virtual Quote Quote { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public string Description { get; set; }
    }
}
