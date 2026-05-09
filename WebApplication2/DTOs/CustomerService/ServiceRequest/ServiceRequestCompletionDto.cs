using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.CustomerService.ServiceRequest
{
    public class ServiceRequestCompletionDto
    {
        [Required(ErrorMessage = "Service Request ID is required")]
        public int ServiceRequestId { get; set; }

        [Required(ErrorMessage = "Resolution notes are required")]
        [StringLength(2000, ErrorMessage = "Resolution notes cannot exceed 2000 characters")]
        public string ResolutionNotes { get; set; }

        public bool SatisfiedCustomer { get; set; }
        public List<int> AttachmentIds { get; set; } // IDs of any new attachments
    }
}
