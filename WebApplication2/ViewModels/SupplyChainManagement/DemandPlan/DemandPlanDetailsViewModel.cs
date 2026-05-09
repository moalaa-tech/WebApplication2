namespace CRM.WebApp.ViewModels.SupplyChainManagement.DemandPlan
{
    public class DemandPlanDetailsViewModel
    {
        public int Id { get; set; }

        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string PlanName { get; set; }
    }
}
