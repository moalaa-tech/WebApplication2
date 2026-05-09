using CRM.Domain.Base;
using CRM.Domain.Enums;
using CRM.Domain.IdentityEntity;

namespace CRM.Domain.Entities.SalesManagement
{
    public class Activity : BaseEntity
    {
        public ActivityType Type { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public ActivityStatus Status { get; set; }
        public int? LeadId { get; set; }
        public virtual Lead Lead { get; set; }
        public int? OpportunityId { get; set; }
        public virtual Opportunity Opportunity { get; set; }
        public int? DealId { get; set; }
        public virtual Deal Deal { get; set; }
        public int OwnerUserId { get; set; }
        public virtual ApplicationUser OwnerUser { get; set; }
    }
}
