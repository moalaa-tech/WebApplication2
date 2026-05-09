using CRM.Domain.Enums.SalesManagement;
using CRM.WebApp.DTOs.Opportunity;

namespace CRM.WebApp.ViewModels.SalesManagement.SalesPipeline
{
    public class SalesPipelineViewModel
    {
        public decimal TotalPipelineValue { get; set; }
        public int TotalOpportunities { get; set; }
        public Dictionary<OpportunityStage, PipelineStageDataViewModel> StageData { get; set; }
        public List<OpportunityDto> RecentOpportunities { get; set; }
    }
}
