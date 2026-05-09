using CRM.Domain.Enums.MarketingAutomation;

namespace CRM.WebApp.DTOs.MarketingAutomation.Campaign
{
    public class CampaignDto
    {
        public int Id { get; set; }


        public string Name { get; set; }

        public string Description { get; set; }


        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal Budget { get; set; }

        public CampaignStatus Status { get; set; }

        public int? EmailTemplateId { get; set; }
        public string EmailTemplateName { get; set; }

        public int ContactCount { get; set; }
        public int InteractionCount { get; set; }
    }
}
