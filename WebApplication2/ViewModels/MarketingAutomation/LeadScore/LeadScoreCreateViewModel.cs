using CRM.WebApp.DTOs.LeadScore;

namespace CRM.WebApp.ViewModels.MarketingAutomation.LeadScore
{
    public class LeadScoreCreateViewModel
    {
        public LeadScoreCreateDto LeadScoreCreateDto { get; set; }
        public List<string> AvailableScoreTypes { get; set; } = new List<string>
        {
            "Behavioral",
            "Demographic",
            "Engagement",
            "Financial",
            "Social"
        };
    }
}
