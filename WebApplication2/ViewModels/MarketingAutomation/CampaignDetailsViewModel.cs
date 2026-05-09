using CRM.Domain.Enums.MarketingAutomation;
using CRM.WebApp.ViewModels.Contact;

namespace CRM.WebApp.ViewModels.MarketingAutomation
{
    public class CampaignDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Budget { get; set; }
        public CampaignStatus Status { get; set; }
        public string EmailTemplateName { get; set; }
        public int ContactCount { get; set; }
        public int InteractionCount { get; set; }

        public List<ContactViewModel> Contacts { get; set; }
        public List<InteractionViewModel> Interactions { get; set; }
    }
}
