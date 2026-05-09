namespace CRM.WebApp.DTOs.SupplyChainManagement
{
    public class ShippingDto
    {
        public Guid Id { get; set; }
        public string TrackingNumber { get; set; }
        public string CarrierName { get; set; }
        public string ServiceType { get; set; }
        public AddressDto Origin { get; set; }
        public AddressDto Destination { get; set; }
        public List<PackageDto> Packages { get; set; } = new List<PackageDto>();
        public decimal TotalCost { get; set; }
        public decimal TotalWeight { get; set; }
        public DateTime ShipDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public DateTime? ActualDeliveryDate { get; set; }
        public string Status { get; set; }
        public bool RequiresSignature { get; set; }
        public bool IsInsured { get; set; }
        public decimal InsuranceValue { get; set; }
        public string LabelUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
