using CRM.Domain.Enums.CustomerService;
using System.ComponentModel;

namespace CRM.WebApp.DTOs.CustomerService.Ticket
{
    public class UpdateTicketDto
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }

        [DisplayName("Customer")]
        public int CustomerId { get; set; }

        [DisplayName("Assigned To")]
        public int? AssignedTo { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Last Updated")]
        public DateTime LastUpdated { get; set; }
    }
}
