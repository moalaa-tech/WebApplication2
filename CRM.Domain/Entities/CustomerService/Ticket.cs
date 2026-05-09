using CRM.Domain.Base;
using CRM.Domain.Entities.AccountsReceivable;
using CRM.Domain.Enums.CustomerService;


namespace CRM.Domain.Entities.CustomerService
{
    public class Ticket : BaseEntity
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public int CustomerId { get; set; }
        public int? AssignedTo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        public SupportAgent AssignedAgent { get; set; }
    }
}
