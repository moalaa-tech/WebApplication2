using CRM.Domain.Base;
using CRM.Domain.Enums.SupplyChainManagement;


namespace CRM.Domain.Entities.SupplyChainManagement
{
    public class Shipping : BaseEntity
    {
        public string ShippingNumber { get; set; }
        public DateTime ShippingDate { get; set; }
        public ShippingMethod Method { get; set; }
        public string TrackingNumber { get; set; }
        public ShippingStatus Status { get; set; }
        public decimal Cost { get; set; }
        public int LogisticsId { get; set; }
        public Logistics Logistics { get; set; }

        public ICollection<Freight> Freights { get; set; } = new List<Freight>();
    }
}
