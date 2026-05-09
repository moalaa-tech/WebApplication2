namespace CRM.WebApp.ViewModels.CustomerService
{
    public class SupportAgentTicketViewModel
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string TicketNumber { get; set; }
        public string Subject { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ResolutionDate { get; set; }

        public string Priority { get; set; }


    }
}
