namespace CRM.WebApp.ViewModels.SupplyChainManagement.Shipping
{
    public class ShippingQuoteViewModel
    {
        public string QuoteId { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public string CarrierServiceName { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime QuoteExpiration { get; set; }
    }
}
