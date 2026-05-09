using CRM.Domain.Integrations.SupplyChain;

namespace CRM.Domain.Requests
{
    public class ShipmentTrackingInfo
    {
        public string TrackingNumber { get; set; }
        public string Carrier { get; set; }
        public string Status { get; set; }
        public string CurrentStatus { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public DateTime? ActualDeliveryDate { get; set; }
        public TrackingEvent[] Events { get; set; }
        public Address Origin { get; set; }
        public Address Destination { get; set; }
        public decimal Weight { get; set; }
        public string WeightUnit { get; set; }
    }
}
