using CRM.Domain.Base;


namespace CRM.Domain.Entities.CustomerService
{
    public class SLAMetrics : BaseEntity
    {
        public int SLAId { get; set; }
        public int? TicketId { get; set; }
        public int? ServiceRequestId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? ResponseTime { get; set; }
        public DateTime? ResolutionTime { get; set; }
        public bool IsMet { get; set; }
        public string Notes { get; set; }

        public ServiceLevelAgreement SLA { get; set; }
        public Ticket Ticket { get; set; }
        public ServiceRequest ServiceRequest { get; set; }
    }
}
