namespace CRM.WebApp.DTOs.SupplyChainManagement
{
    public class DemandPlanDto
    {
        public int Id { get; set; }
        public string PlanName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public ICollection<DemandPlanItemDto> Items { get; set; }
    }
}
