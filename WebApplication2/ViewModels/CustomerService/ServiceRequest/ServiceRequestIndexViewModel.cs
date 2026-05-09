using CRM.WebApp.DTOs.CustomerService.ServiceRequest;

namespace CRM.WebApp.ViewModels.CustomerService.ServiceRequest
{
    public class ServiceRequestIndexViewModel
    {
        public IEnumerable<ServiceRequestDto> ServiceRequests { get; set; }
        public Dictionary<string, int> StatusCounts { get; set; }
    }
}
