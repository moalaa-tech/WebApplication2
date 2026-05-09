using CRM.Domain.Enums.CustomerService;

namespace CRM.WebApp.DTOs.CustomerService.ServiceRequest
{
    public class ServiceRequestDetailDto
    {
        public int Id { get; set; }
        public string RequestNumber { get; set; }
        public string RequestType { get; set; }
        public string Description { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string ResolutionNotes { get; set; }

        // Customer information
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }

        // Agent information (if assigned)
        public int? AssignedTo { get; set; }
        public string AssignedAgentName { get; set; }

        // Attachments
        public List<ServiceRequestAttachmentDto> Attachments { get; set; }

        // SLA information (if applicable)
        public int? SLAId { get; set; }
        public string SLAName { get; set; }
        public int? ResponseTimeHours { get; set; }
        public int? ResolutionTimeHours { get; set; }
        public DateTime? SLAStartTime { get; set; }
        public DateTime? ResponseDeadline { get; set; }
        public DateTime? ResolutionDeadline { get; set; }
        public bool? SLAMet { get; set; }
    }
}
