using CRM.WebApp.DTOs.CustomerService.ServiceRequest;

namespace CRM.WebApp.ViewModels.CustomerService.ServiceRequest
{
    public class ServiceRequestEditViewModel
    {
        public ServiceRequestUpdateDto ServiceRequestUpdateDto { get; set; }
        public List<SupportAgentLookupDto> SupportAgents { get; set; }
    }
}
