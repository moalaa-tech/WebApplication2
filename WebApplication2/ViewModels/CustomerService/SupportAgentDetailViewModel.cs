namespace CRM.WebApp.ViewModels.CustomerService
{
    public class SupportAgentDetailViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public string Specialization { get; set; }
        public List<string> Skills { get; set; }
        public string WorkingHours { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
        public int TotalTicketsResolved { get; set; }
        public double AverageRating { get; set; }
        public double AverageResolutionTime { get; set; }
        public List<SupportAgentTicketViewModel> RecentTickets { get; set; }
    }
}
