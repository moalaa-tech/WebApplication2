using CRM.Domain.Base;
using CRM.Domain.Enums.SupplyChainManagement;


namespace CRM.Domain.Entities.SupplyChainManagement
{
    public class Freight : BaseEntity
    {
        public string FreightNumber { get; set; }
        public FreightType Type { get; set; }
        public decimal Weight { get; set; }
        public decimal Volume { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int ShippingId { get; set; }
        public Shipping Shipping { get; set; }
    }
}
