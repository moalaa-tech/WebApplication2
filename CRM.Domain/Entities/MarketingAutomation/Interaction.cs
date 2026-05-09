using CRM.Domain.Base;
using CRM.Domain.Enums.MarketingAutomation;


namespace CRM.Domain.Entities.MarketingAutomation
{
    public class Interaction : BaseEntity
    {
        public int? ContactId { get; set; }
        public int? CampaignId { get; set; }
        public InteractionType Type { get; set; }
        public string Details { get; set; }
        public DateTime InteractionDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Contact Contact { get; set; }
        public Campaign Campaign { get; set; }
    }
}
