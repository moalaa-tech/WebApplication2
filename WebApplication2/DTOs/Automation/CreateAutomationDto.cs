using CRM.Domain.Enums.MarketingAutomation;

namespace CRM.WebApp.DTOs.Automation
{
    public class CreateAutomationDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public AutomationTrigger Trigger { get; set; }
        public DateTime? LastRunDate { get; set; }
        public string? CustomEventName { get; set; }
    }
}
