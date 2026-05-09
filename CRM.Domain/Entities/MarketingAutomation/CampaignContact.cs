namespace CRM.Domain.Entities.MarketingAutomation
{
    public class CampaignContact
    {
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }

    }
}
