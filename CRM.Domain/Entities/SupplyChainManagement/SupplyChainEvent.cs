using CRM.Domain.Base;


namespace CRM.Domain.Entities.SupplyChainManagement
{
    public class SupplyChainEvent : BaseEntity
    {
        public SupplyChainEvent() { }

        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string Description { get; set; }
        public int DemandPlanId { get; set; }
        public DemandPlan DemandPlan { get; set; }
        public ICollection<SupplyChainEventDetail> Details { get; set; } = new List<SupplyChainEventDetail>();
    }
}
