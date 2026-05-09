namespace CRM.WebApp.ViewModels.CustomerService
{
    public class SupportAgentListViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
        public bool IsActive { get; set; }
        public int OpenTickets { get; set; }
        public double AverageRating { get; set; }
    }
}
