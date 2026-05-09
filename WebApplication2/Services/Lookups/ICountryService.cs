using CRM.WebApp.DTOs.HumanResources;

namespace CRM.WebApp.Services.Lookups
{
    public interface ICountryService
    {
        Task<List<CountryDto>> GetAllAsync();
        Task<CountryDto?> GetByIdAsync(int id);
        Task<CountryDto> CreateAsync(CountryDto dto);
        Task<CountryDto> UpdateAsync(CountryDto dto);
        Task DeleteAsync(int id);
    }

}
