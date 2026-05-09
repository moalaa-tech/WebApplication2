using CRM.Domain.Enums.SalesManagement;

namespace CRM.WebApp.ViewModels.SalesManagement
{
    public class LeadFilterViewModel
    {
        public string SearchTerm { get; set; }
        public LeadStatus? Status { get; set; }
        public string Source { get; set; }
        public string AssignedTo { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
