using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.CustomerService.ServiceRequest
{
    public class ServiceRequestCreateDto
    {
        [Required(ErrorMessage = "Request type is required")]
        [StringLength(100, ErrorMessage = "Request type cannot exceed 100 characters")]
        public string RequestType { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Customer ID is required")]
        public Guid CustomerId { get; set; }
    }
}
