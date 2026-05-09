using CRM.Domain.Enums.MarketingAutomation;

namespace CRM.WebApp.ViewModels.MarketingAutomation
{
    public class InteractionViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } // Email, Call, Meeting, etc.
        public string Notes { get; set; }
        public InteractionStatus Status { get; set; }
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public int CampaignId { get; set; }

        // Helper properties for display
        public string FormattedDate => Date.ToString("MMM d, yyyy h:mm tt");
        public string StatusBadgeClass => Status switch
        {
            InteractionStatus.Successful => "badge-success",
            InteractionStatus.Failed => "badge-danger",
            InteractionStatus.Pending => "badge-warning",
            _ => "badge-secondary"
        };
    }
}
