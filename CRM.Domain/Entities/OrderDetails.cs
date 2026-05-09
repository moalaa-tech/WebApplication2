using CRM.Domain.Base;
using CRM.Domain.Entities.InventoryManagement;

namespace CRM.Domain.Entities
{
    public class OrderDetails : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public long UTMCampaign { get; set; }
        public string? Note { get; set; }
        public string? UTMSource { get; set; }
    }
}
