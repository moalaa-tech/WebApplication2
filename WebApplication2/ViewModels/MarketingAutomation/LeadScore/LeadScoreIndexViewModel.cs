using CRM.WebApp.DTOs.LeadScore;

namespace CRM.WebApp.ViewModels.MarketingAutomation.LeadScore
{
    public class LeadScoreIndexViewModel
    {
        public IEnumerable<LeadScoreDto> LeadScores { get; set; }
        public Dictionary<string, int> ScoreTypeCounts { get; set; }
    }
}
