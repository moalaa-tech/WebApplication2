using System.Numerics;

namespace CRM.WebApi.DbContext
{
    public class Campaign : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProviderSerial { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Budget { get; set; }
        public CampaignStatus Status { get; set; } = CampaignStatus.Planned;

        public int CampaignTypesId { get; set; }
        public CampaignTypes CampaignTypes { get; set; }



    }
}
