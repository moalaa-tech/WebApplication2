using CRM.Domain.Enums.SalesManagement;
using CRM.WebApp.DTOs.Lead;

namespace CRM.WebApp.DTOs.Opportunity
{
    public class CreateOpportunityDto
    {
        public int Id { get; set; }
        public int LeadId { get; set; }
        public LeadDto Lead { get; set; }

        public string Title { get; set; }
        public OpportunityStage Stage { get; set; }
        public decimal EstimatedValue { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
