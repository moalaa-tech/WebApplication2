using CRM.Domain.Enums.MarketingAutomation;

namespace CRM.WebApp.DTOs.AutomationStep
{
    public class AutomationStepDto
    {
        public int Id { get; set; }
        public int AutomationId { get; set; }
        public int Order { get; set; }
        public AutomationAction Action { get; set; }
        public int? EmailTemplateId { get; set; }
        public int? WaitDays { get; set; }
        public int? CampaignId { get; set; }
        public string? StatusValue { get; set; }
    }
}
