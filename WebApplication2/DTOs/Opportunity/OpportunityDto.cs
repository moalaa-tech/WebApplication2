using CRM.Domain.Enums.SalesManagement;
using CRM.WebApp.DTOs.Lead;

namespace CRM.WebApp.DTOs.Opportunity
{
    public class OpportunityDto
    {
        public int Id { get; set; }
        public int LeadId { get; set; }
        public LeadDto Lead { get; set; }

        public string Title { get; set; }
        public OpportunityStage Stage { get; set; }
        public decimal EstimatedValue { get; set; }
        public DateTime CreatedAt { get; set; }

        public string OwnerUserName { get; set; }
        public string StageName { get; set; }
    }
}
