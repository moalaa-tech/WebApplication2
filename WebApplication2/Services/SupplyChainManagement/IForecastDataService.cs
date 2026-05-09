using CRM.WebApp.DTOs.SupplyChainManagement.ForecastData;

namespace CRM.WebApp.Services.SupplyChainManagement
{
    public interface IForecastDataService
    {
        Task<IEnumerable<ForecastDataDto>> GetAllForecastDataAsync();
        Task<ForecastDataDto> GetForecastDataByIdAsync(int id);
        Task<ForecastDataDto> CreateForecastDataAsync(CreateForecastDataDto createDto);
        Task UpdateForecastDataAsync(UpdateForecastDataDto updateDto);
        Task DeleteForecastDataAsync(int id);
        Task<bool> ForecastDataExistsAsync(int id);
        Task<IEnumerable<ForecastDataDto>> GetFilteredForecastDataAsync(string dataType, DateTime? startDate, DateTime? endDate, int? demandPlanId);
        Task<decimal> GetForecastSummaryAsync(string summaryType, DateTime? startDate, DateTime? endDate);
    }
}
