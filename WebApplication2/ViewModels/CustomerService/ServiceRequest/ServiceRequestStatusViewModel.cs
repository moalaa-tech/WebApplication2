using CRM.Domain.Enums.CustomerService;
using CRM.WebApp.DTOs.CustomerService.ServiceRequest;

namespace CRM.WebApp.ViewModels.CustomerService.ServiceRequest
{
    public class ServiceRequestStatusViewModel
    {
        public ServiceRequestStatusDto StatusDto { get; set; }
        public RequestStatus CurrentStatus { get; set; }
    }
}
