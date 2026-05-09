using CRM.WebApp.DTOs.CustomerService.ServiceRequest;

namespace CRM.WebApp.ViewModels.CustomerService.ServiceRequest
{
    public class ServiceRequestCreateViewModel
    {
        public ServiceRequestCreateDto ServiceRequestCreateDto { get; set; }
        public List<CustomerLookupDto> Customers { get; set; }
        public List<SupportAgentLookupDto> SupportAgents { get; set; }
    }
}
