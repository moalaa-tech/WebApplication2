namespace CRM.WebApp.DTOs.CustomerService
{
    public class SLAMetricDto
    {
        public int Id { get; set; }
        public int? TicketId { get; set; }
        public string TicketNumber { get; set; }
        public int? ServiceRequestId { get; set; }
        public string ServiceRequestNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? ResponseTime { get; set; }
        public DateTime? ResolutionTime { get; set; }
        public bool IsMet { get; set; }
        public string StatusColor => IsMet ? "success" : "danger";

        // Time calculations
        public double? ResponseTimeHours => ResponseTime.HasValue
            ? (ResponseTime.Value - StartTime).TotalHours : null;

        public double? ResolutionTimeHours => ResolutionTime.HasValue
            ? (ResolutionTime.Value - StartTime).TotalHours : null;

        public string Notes { get; set; }
        public string CustomerName { get; set; }
        public string AssignedAgent { get; set; }
    }
}
