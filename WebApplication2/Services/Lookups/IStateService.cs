using CRM.WebApp.DTOs.HumanResources;

namespace CRM.WebApp.Services.Lookups
{
    public interface IStateService
    {
        Task<List<StateDto>> GetAllAsync();
        Task<StateDto?> GetByIdAsync(int id);
        Task<StateDto> CreateAsync(StateDto dto);
        Task<StateDto> UpdateAsync(StateDto dto);
        Task DeleteAsync(int id);
    }

}
