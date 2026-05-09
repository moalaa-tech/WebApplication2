namespace CRM.Domain.Requests
{
    public class ShippingLabel
    {
        public byte[] LabelData { get; set; }
        public string FileFormat { get; set; } // "PDF", "PNG", etc.
        public string TrackingNumber { get; set; }
        public string LabelUrl { get; set; }
    }
}
