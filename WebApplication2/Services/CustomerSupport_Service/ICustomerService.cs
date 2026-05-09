using CRM.WebApp.DTOs.Customer;

namespace CRM.WebApp.Services.CustomerSupport_Service
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync(int pageNumber, int pageSize);
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();

        Task<CustomerDto> GetCustomerByIdAsync(int id);
        Task<int> CreateCustomerAsync(CreateCustomerDto createCustomerDto);
        Task<bool> UpdateCustomerAsync(UpdateCustomerDto updateCustomerDto);
        Task<bool> DeleteCustomerAsync(int id);
    }
}
