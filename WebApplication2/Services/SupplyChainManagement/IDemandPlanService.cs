using CRM.WebApp.DTOs.SupplyChainManagement;
using CRM.WebApp.DTOs.SupplyChainManagement.ForecastData;

namespace CRM.WebApp.Services.SupplyChainManagement
{
    public interface IDemandPlanService
    {
        Task<DemandPlanDto> GetDemandPlanByIdAsync(int id);
        Task<IReadOnlyList<DemandPlanListDto>> GetAllDemandPlansAsync();
        Task<DemandPlanDto> CreateDemandPlanAsync(DemandPlanCreateDto demandPlanCreateDto);
        Task UpdateDemandPlanAsync(int id, DemandPlanUpdateDto demandPlanUpdateDto);
        Task DeleteDemandPlanAsync(int id);

        Task<bool> AddItemToPlanAsync(int planId, DemandPlanItemCreateDto itemDto);
        Task<bool> UpdatePlanItemAsync(DemandPlanItemDto itemDto);
        Task<bool> RemoveItemFromPlanAsync(int itemId);
        Task<bool> UpdateItemProcurementStatusAsync(int itemId, bool isProcured);
        Task<SupplyChainDashboardDto> GetDashboardDataAsync();
        Task<IEnumerable<DemandPlanItemDto>> GetPlanItemsAsync(int planId);
        Task<IEnumerable<ForecastDataDto>> GetForecastDataAsync(int planId, string dataType = null);
        Task<bool> GenerateForecastAsync(int planId);


    }
}
