namespace CRM.Domain.Requests
{
    public class ShippingQuoteRequest : ShippingRequest
    {
        public bool IncludeInsurance { get; set; }
        public bool RequireSignature { get; set; }
    }
}
