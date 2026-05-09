namespace CRM.Domain.Requests
{
    public class ShippingLabelRequest : ShippingRequest
    {
        public string CarrierServiceId { get; set; }
        public string CustomerReference { get; set; }
        public string BillingAccountNumber { get; set; }
    }
}
