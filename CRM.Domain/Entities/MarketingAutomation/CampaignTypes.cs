using CRM.Domain.Base;

namespace CRM.Domain.Entities.MarketingAutomation
{
    public class CampaignTypes : BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }

        public ICollection<Campaign> Campaigns { get; set; }
    }
}
