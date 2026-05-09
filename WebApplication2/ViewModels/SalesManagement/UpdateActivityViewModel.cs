using CRM.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.ViewModels.SalesManagement
{
    public class UpdateActivityViewModel
    {
        public int Id { get; set; }
        public Domain.Enums.ActivityType Type { get; set; }
        public string TypeName => Type.ToString();
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public ActivityStatus Status { get; set; }
        public string StatusName => Status.ToString();
        public int? LeadId { get; set; }
        public string LeadName { get; set; }
        public int? OpportunityId { get; set; }
        public string OpportunityName { get; set; }
        public int? DealId { get; set; }
        public string DealName { get; set; }
        public string OwnerUserId { get; set; }
        public string OwnerUserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsOverdue => Status != ActivityStatus.Completed && DueDate < DateTime.UtcNow;

        public IEnumerable<SelectListItem> Users { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; }
    }
}
