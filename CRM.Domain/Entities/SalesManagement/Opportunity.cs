using CRM.Domain.Base;
using CRM.Domain.Enums.SalesManagement;
using CRM.Domain.IdentityEntity;

namespace CRM.Domain.Entities.SalesManagement
{
    public class Opportunity : BaseEntity
    {
        public int LeadId { get; set; }
        public Lead Lead { get; set; }

        public string Title { get; set; }
        public OpportunityStage Stage { get; set; }
        public decimal EstimatedValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public int? ContactId { get; set; }
        public virtual Contact Contact { get; set; }
        //public int? AccountId { get; set; }
        //public virtual Account Account { get; set; }

        public int OwnerUserId { get; set; }
        public virtual ApplicationUser OwnerUser { get; set; }
    }
}
