using CRM.Domain.Enums.CustomerService;
using CRM.WebApp.DTOs.Customer;

namespace CRM.WebApp.DTOs.CustomerService.Ticket
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public CustomerDto Customer { get; set; }
        public int CustomerId { get; set; }
        public int? AssignedTo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
