using CRM.Domain.Enums.CustomerService;

namespace CRM.WebApp.DTOs.CustomerService.ServiceRequest
{
    public class ServiceRequestListDto
    {
        public int Id { get; set; }
        public string RequestNumber { get; set; }
        public string RequestType { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CustomerName { get; set; }
        public string AssignedAgentName { get; set; }
        public bool HasAttachments { get; set; }
        public bool IsOverdue { get; set; }
    }
}
