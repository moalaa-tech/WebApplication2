using CRM.Domain.Base;
using CRM.Domain.Enums.MarketingAutomation;

namespace CRM.Domain.Entities.MarketingAutomation
{
    public class Automation : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AutomationTrigger Trigger { get; set; }
        public DateTime? LastRunDate { get; set; }

        // Navigation properties
        public ICollection<AutomationStep> Steps { get; set; }
        public ICollection<AutomationContact> Contacts { get; set; }
        public string? CustomEventName { get; set; }
    }
}
