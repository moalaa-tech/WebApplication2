namespace CRM.WebApp.DTOs.SupplyChainManagement
{
    public class DemandPlanUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public List<DemandPlanItemUpdateDto> Items { get; set; } = new List<DemandPlanItemUpdateDto>();
    }
}
