using CRM.Domain.Integrations.SupplyChain;

namespace CRM.Domain.Requests
{
    public class ShippingRequest
    {
        public Address Origin { get; set; }
        public Address Destination { get; set; }
       // public List<ShippingPackagedto> Packages { get; set; } = new List<ShippingPackage>();
        public DateTime? ShipDate { get; set; } = DateTime.UtcNow;

    }
}
