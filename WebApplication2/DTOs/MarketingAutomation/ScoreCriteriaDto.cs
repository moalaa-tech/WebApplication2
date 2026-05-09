using CRM.Domain.Enums.MarketingAutomation;

namespace CRM.WebApp.DTOs.MarketingAutomation
{
    public class ScoreCriteriaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DefaultPoints { get; set; }
        public bool IsActive { get; set; }
        public ScoreCategory Category { get; set; }
    }
}
