namespace CRM.Domain.Requests
{
    public class ShippingOption
    {
        public string CarrierId { get; set; }
        public string CarrierName { get; set; }
        public string ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal Cost { get; set; }
        public string Currency { get; set; } = "USD";
        public DateTime EstimatedDeliveryDate { get; set; }
        public int TransitDays { get; set; }
        public bool RequiresSignature { get; set; }
        public bool IsInsured { get; set; }
        public decimal MaximumInsuranceValue { get; set; }
        public string[] PackageTypes { get; set; }
        public decimal MaximumWeight { get; set; }
        public string WeightUnit { get; set; } = "kg";
    }
}
