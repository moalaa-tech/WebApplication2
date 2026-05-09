using CRM.Domain.Base;

namespace CRM.Domain.Entities.SupplyChainManagement
{
    public class ShippingCarrier : BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public bool IsActive { get; set; }
        public string[] SupportedServices { get; set; }
        public string[] SupportedCountries { get; set; }
        public decimal MaximumPackageWeight { get; set; }
        public string[] PackageTypes { get; set; }

        public string Description { get; set; }
    }
}
