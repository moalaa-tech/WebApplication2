using CRM.WebApp.DTOs.LeadScore;

namespace CRM.WebApp.ViewModels.MarketingAutomation.LeadScore
{
    public class LeadScoreRuleViewModel
    {
        public LeadScoreRuleDto Rule { get; set; }
        public int LeadScoreId { get; set; }
        public string LeadScoreName { get; set; }
    }
}
