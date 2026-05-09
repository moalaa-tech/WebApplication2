using CRM.Domain.Base;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.Domain.Enums.SalesManagement;
using CRM.Domain.IdentityEntity;

namespace CRM.Domain.Entities.SalesManagement
{
    public class Lead : BaseEntity
    {
        public string Email { get; set; }
        public string Title { get; set; }

        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }

        public LeadStatus Status { get; set; } = LeadStatus.New;

        public int AssignedToUserId { get; set; }

        public virtual ApplicationUser AssignedToUser { get; set; }

        public ICollection<LeadScore> Scores { get; set; } = new List<LeadScore>();


    }
}
