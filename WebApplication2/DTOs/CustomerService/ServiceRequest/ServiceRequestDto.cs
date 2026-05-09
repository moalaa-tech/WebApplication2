using CRM.Domain.Enums.CustomerService;

namespace CRM.WebApp.DTOs.CustomerService.ServiceRequest
{
    public class ServiceRequestDto
    {
        public int Id { get; set; }
        public string RequestType { get; set; }
        public string Description { get; set; }
        public RequestStatus Status { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
