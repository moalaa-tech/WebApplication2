using CRM.WebApp.DTOs.CustomerService.ServiceRequest;

namespace CRM.WebApp.Services.CustomerSupport_Service
{
    public interface IServiceRequestService
    {
        Task<IEnumerable<ServiceRequestDto>> GetAllServiceRequestsAsync();
        Task<ServiceRequestDto> GetServiceRequestByIdAsync(int id);
        Task<int> CreateServiceRequestAsync(ServiceRequestCreateDto requestDto);
        Task UpdateServiceRequestAsync(ServiceRequestUpdateDto requestDto);
        Task DeleteServiceRequestAsync(int id);
    }
}
