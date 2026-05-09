using CRM.Domain.Entities.MarketingAutomation;
using CRM.Domain.Enums.MarketingAutomation;

namespace CRM.WebApp.Services.MarketingAutomation
{
    public interface IMarketingAutomationService
    {
        Task RunAutomationForContact(int automationId, int contactId);
        Task ProcessTrigger(AutomationTrigger trigger, int contactId, string customEvent = null);
        Task ExecuteAutomationStep(int automationId, int contactId, int stepId);
        Task<List<Automation>> GetActiveAutomations();
    }
}
