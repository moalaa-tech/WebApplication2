using CRM.Domain.Base;
using CRM.Domain.Enums.MarketingAutomation;
using System.Numerics;


namespace CRM.Domain.Entities.MarketingAutomation
{
    public class Campaign : BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Description { get; set; }
        public string ProviderSerial { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Budget { get; set; }
        public CampaignStatus Status { get; set; } = CampaignStatus.Planned;
        public int CampaignTypesId { get; set; }
        public CampaignTypes CampaignTypes { get; set; }
        public int? EmailTemplateId { get; set; }
        public virtual EmailTemplate EmailTemplate { get; set; }
        public ICollection<CampaignContact> Contacts { get; set; }
        public ICollection<Interaction> Interactions { get; set; }
    }
}
