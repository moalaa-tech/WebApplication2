using CRM.Domain.Base;
using CRM.Domain.Enums.MarketingAutomation;


namespace CRM.Domain.Entities.MarketingAutomation
{
    public class AutomationStep : BaseEntity
    {
        public int AutomationId { get; set; }
        public int Order { get; set; }
        public AutomationAction Action { get; set; }
        public int? EmailTemplateId { get; set; }
        public int? WaitDays { get; set; }

        // Navigation properties
        public Automation Automation { get; set; }
        public EmailTemplate EmailTemplate { get; set; }
        public int? CampaignId { get; set; }
        public string? StatusValue { get; set; }
    }
}
