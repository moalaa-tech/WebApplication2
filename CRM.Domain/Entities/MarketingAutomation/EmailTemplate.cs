using CRM.Domain.Base;

namespace CRM.Domain.Entities.MarketingAutomation
{
    public class EmailTemplate : BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        // Navigation properties
        public ICollection<Campaign> Campaigns { get; set; }
        public ICollection<AutomationStep> AutomationSteps { get; set; }
    }
}
