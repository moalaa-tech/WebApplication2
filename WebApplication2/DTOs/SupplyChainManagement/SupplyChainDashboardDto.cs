namespace CRM.WebApp.DTOs.SupplyChainManagement
{
    public class SupplyChainDashboardDto
    {
        public int TotalPlans { get; set; }
        public int ActivePlans { get; set; }
        public int CompletedPlans { get; set; }
        public decimal TotalEstimatedCost { get; set; }
        public int TotalItems { get; set; }
        public int ProcuredItems { get; set; }
        public List<DemandPlanDto> RecentPlans { get; set; }
        public List<DemandPlanItemDto> HighPriorityItems { get; set; }
    }
}
