namespace CRM.Domain.Requests
{
    public class TrackingEvent
    {
        public DateTime Timestamp { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string EventCode { get; set; }
    }
}
