using CRM.WebApp.DTOs.SalesPipeline;

namespace CRM.WebApp.Services.SalesManagement
{
    public interface ISalesPipelineService
    {
        Task<SalesPipelineDto> GetPipelineOverviewAsync();
        Task<SalesForecastDto> GetSalesForecastAsync(DateTime startDate, DateTime endDate);
        Task<StageAnalysisDto> GetStageAnalysisAsync();
        Task<WinLossAnalysisDto> GetWinLossAnalysisAsync(DateTime startDate, DateTime endDate);
    }
}
