using CRM.Domain.Enums.MarketingAutomation;
using CRM.WebApp.DTOs.Automation;
using CRM.WebApp.DTOs.EmailTemplate;
using CRM.WebApp.ViewModels.AutomationContact;
using CRM.WebApp.ViewModels.AutomationStep;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.ViewModels.Automation
{
    public class AutomationViewModel
    {
        public AutomationDto Automation { get; set; }
        public List<AutomationTrigger> AvailableTriggers { get; set; }



        public List<AutomationStepViewModel> Steps { get; set; }
        public List<AutomationContactViewModel> ActiveContacts { get; set; }
        public AutomationRunHistoryViewModel RunHistory { get; set; }

        public SelectList TriggerOptions { get; set; }
        public SelectList ActionOptions { get; set; }
        public SelectList EmailTemplateOptions { get; set; }
        public SelectList CampaignOptions { get; set; }
        public SelectList StatusOptions { get; set; }
        public IEnumerable<EmailTemplateDto> EmailTemplates { get; internal set; }
        public List<AutomationAction> AvailableActions { get; internal set; }
    }
}
