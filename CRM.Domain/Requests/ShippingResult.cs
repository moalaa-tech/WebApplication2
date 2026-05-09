namespace CRM.Domain.Requests
{
    public class ShippingResult
    {
        public bool Success { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public string CarrierServiceName { get; set; }
        public string TrackingNumber { get; set; }
        public string ErrorMessage { get; set; }
    }
}
