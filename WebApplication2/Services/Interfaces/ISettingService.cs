using CRM.WebApp.DTOs.Setting;

namespace CRM.WebApp.Services.Interfaces
{
    public interface ISettingService
    {
        Task<IEnumerable<SettingDto>> GetAllSettingsAsync();
        Task<SettingDto> GetSettingByIdAsync(int id);
        Task<int> CreateSettingAsync(CreateSettingDto createSettingDto);
        Task<bool> UpdateSettingAsync(UpdateSettingDto updateSettingDto);
        Task<bool> DeleteSettingAsync(int id);
    }
}
