using CRM.Domain.Enums;

namespace CRM.WebApp.ViewModels.SalesManagement
{
    public class ActivityFilterViewModel
    {
        public string? SearchTerm { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int OwnerUserId { get; set; }
        public ActivityType? Type { get; set; }
        public ActivityStatus? Status { get; set; }
    }
}
