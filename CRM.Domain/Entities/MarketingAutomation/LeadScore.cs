using CRM.Domain.Base;
using CRM.Domain.Entities.SalesManagement;


namespace CRM.Domain.Entities.MarketingAutomation
{
    public class LeadScore : BaseEntity
    {
        public int LeadId { get; set; }
        public Lead Lead { get; set; }
        public int ScoreCriteriaId { get; set; }
        public ScoreCriteria Criteria { get; set; }
        public int Points { get; set; }
        public DateTime ScoredDate { get; set; } = DateTime.UtcNow;

        public ICollection<LeadScoreRule> Rules { get; set; }

    }
}
