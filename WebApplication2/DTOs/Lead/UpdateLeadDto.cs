using CRM.Domain.Enums.SalesManagement;

namespace CRM.WebApp.DTOs.Lead
{
    public class UpdateLeadDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }

        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }

        public LeadStatus Status { get; set; } = LeadStatus.New;
    }
}
