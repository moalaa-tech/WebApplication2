using CRM.Domain.Enums.CustomerService;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.CustomerService.ServiceRequest
{
    public class ServiceRequestStatusDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [EnumDataType(typeof(RequestStatus))]
        public RequestStatus Status { get; set; }

        [StringLength(1000, ErrorMessage = "Resolution notes cannot exceed 1000 characters")]
        public string ResolutionNotes { get; set; }

        // Optional: Who is changing the status
        public int? ChangedByUserId { get; set; }

        // Optional: Timestamp of the change (can be set server-side)
        public DateTime? StatusChangedDate { get; set; }

        // Optional: For tracking previous status
        public RequestStatus? PreviousStatus { get; set; }
    }
}
