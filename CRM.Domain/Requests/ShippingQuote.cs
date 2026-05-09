namespace CRM.Domain.Requests
{
    public class ShippingQuote : ShippingResult
    {
        public string QuoteId { get; set; }
        public DateTime QuoteExpiration { get; set; }
    }
}
