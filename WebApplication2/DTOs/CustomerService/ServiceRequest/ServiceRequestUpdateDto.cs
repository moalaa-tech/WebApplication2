using CRM.Domain.Enums.CustomerService;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.CustomerService.ServiceRequest
{
    public class ServiceRequestUpdateDto
    {
        [Required(ErrorMessage = "ID is required")]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Request type cannot exceed 100 characters")]
        public string RequestType { get; set; }

        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
        public string Description { get; set; }

        public RequestStatus Status { get; set; }

        public Guid? AssignedTo { get; set; }

        [StringLength(2000, ErrorMessage = "Resolution notes cannot exceed 2000 characters")]
        public string ResolutionNotes { get; set; }
    }
}
