using CRM.WebApp.DTOs.Business;

namespace CRM.WebApp.Services.Interfaces
{
    public interface IBusinessService
    {
        Task<IEnumerable<BusinessDto>> GetAllBusinesessAsync();
        Task<BusinessDto> GetBusinessByIdAsync(int id);
        Task<BusinessDto> CreateBusinessAsync(CreateBusinessDto createCompanyDto);
        Task<bool> UpdateBusinessAsync(UpdateBusinessDto updateCompanyDto);
        Task<bool> DeleteBusinessAsync(int id);
        Task<bool> BusinessExistsAsync(int id);
    }
}
