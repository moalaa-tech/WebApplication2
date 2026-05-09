using CRM.Domain.Base;

namespace CRM.Domain.Entities.MarketingAutomation
{
    public class AutomationContact : BaseEntity
    {
        public int AutomationId { get; set; }
        public Automation Automation { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }

        public int CurrentStep { get; set; }
        public DateTime? NextStepDate { get; set; }
    }
}
