using CRM.WebApp.DTOs.SalesPipeline;

namespace CRM.WebApp.Services.SalesManagement
{
    public class SalesPipelineService : ISalesPipelineService
    {
        public async Task<SalesPipelineDto> GetPipelineOverviewAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<SalesForecastDto> GetSalesForecastAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<StageAnalysisDto> GetStageAnalysisAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<WinLossAnalysisDto> GetWinLossAnalysisAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
