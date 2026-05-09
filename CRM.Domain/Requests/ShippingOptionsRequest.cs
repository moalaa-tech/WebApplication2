using CRM.Domain.Integrations.SupplyChain;
using System.Net;

namespace CRM.Domain.Requests
{
    public class ShippingOptionsRequest
    {
        public Address Origin { get; set; }
        public Address Destination { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal TotalValue { get; set; }
        //public List<PackageViewModel> Packages { get; set; } = new List<PackageViewModel>();
    }
}
