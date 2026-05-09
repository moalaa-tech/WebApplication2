using CRM.Domain.Base;
using CRM.Domain.Entities.AccountsReceivable;
using CRM.Domain.Enums.CustomerService;


namespace CRM.Domain.Entities.CustomerService
{
    public class ServiceRequest : BaseEntity
    {
        public string RequestType { get; set; }
        public string Description { get; set; }
        public RequestStatus Status { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string RequestNumber { get; set; } // Format: SR-YYYYMMDD-0001
        public int? AssignedTo { get; set; }
        public string ResolutionNotes { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        public SupportAgent AssignedAgent { get; set; }
        public ICollection<ServiceRequestAttachment> Attachments { get; set; }
    }
}
