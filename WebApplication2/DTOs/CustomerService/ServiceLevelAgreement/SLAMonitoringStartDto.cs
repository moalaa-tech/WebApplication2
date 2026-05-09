using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.CustomerService
{
    public class SLAMonitoringStartDto
    {
        [Required(ErrorMessage = "SLA ID is required")]
        public int SLAId { get; set; }

        public int? TicketId { get; set; }
        public int? ServiceRequestId { get; set; }

        [Required(ErrorMessage = "Monitoring must be associated with either a ticket or service request")]
        public bool IsValid => TicketId.HasValue || ServiceRequestId.HasValue;
    }
}
