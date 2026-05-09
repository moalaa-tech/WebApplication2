namespace CRM.WebApp.ViewModels.CustomerService
{
    public class SupportAgentDetailsViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public string Specialization { get; set; }
        public bool IsActive { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string WorkingHours { get; set; }
        public string TimeZone { get; set; }
        public int TotalTicketsResolved { get; set; }
        public double AverageRating { get; set; }

        public List<string> Skills { get; set; } = new List<string>();
        public List<SupportAgentTicketViewModel> RecentTickets { get; set; } = new List<SupportAgentTicketViewModel>();
    }
}
