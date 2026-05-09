using CRM.Domain.Enums.SalesManagement;

namespace CRM.WebApp.DTOs.Lead
{
    public class LeadDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }

        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }

        public LeadStatus Status { get; set; } = LeadStatus.New;


        public string AssignedToUserId { get; set; }
        public string AssignedToUserName { get; set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string TotalScore { get; internal set; }
    }
}
