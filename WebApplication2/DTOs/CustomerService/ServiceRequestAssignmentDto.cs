using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.CustomerService
{
    public class ServiceRequestAssignmentDto
    {
        [Required(ErrorMessage = "Service Request ID is required")]
        public int ServiceRequestId { get; set; }

        [Required(ErrorMessage = "Agent ID is required")]
        public int AgentId { get; set; }

        public string Notes { get; set; }
    }
}
