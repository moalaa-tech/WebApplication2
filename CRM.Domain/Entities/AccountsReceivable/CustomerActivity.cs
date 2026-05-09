using CRM.Domain.Entities.SalesManagement;

namespace CRM.Domain.Entities.AccountsReceivable
{
    public class CustomerActivity
    {
        public Guid Id { get; set; }
        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }
        public ActivityType Type { get; set; }
        public string Details { get; set; } // e.g., "Product Page Visited", "Email Opened: Welcome"
        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

    }
}
