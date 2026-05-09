
using CRM.Domain.Entities.SalesManagement;

namespace CRM.WebApp.DTOs.SalesPipeline
{
    public class ActivityFilterDto
    {
        public string? SearchTerm { get; internal set; }
        public DateTime? FromDate { get; internal set; }
        public DateTime? ToDate { get; internal set; }
        public int OwnerUserId { get; internal set; }
        public ActivityType? Type { get; internal set; }
    }
}
