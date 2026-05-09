using CRM.Domain.Enums.SalesManagement;
using CRM.WebApp.DTOs.Opportunity;

namespace CRM.WebApp.DTOs.SalesPipeline
{
    public class SalesPipelineDto
    {
        public decimal TotalPipelineValue { get; set; }
        public int TotalOpportunities { get; set; }
        public Dictionary<OpportunityStage, PipelineStageDataDto> StageData { get; set; }
        public List<OpportunityDto> RecentOpportunities { get; set; }
    }
}
