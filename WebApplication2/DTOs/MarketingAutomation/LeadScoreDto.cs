namespace CRM.WebApp.DTOs.MarketingAutomation
{
    public class LeadScoreDto
    {
        public int Id { get; set; }
        public int LeadId { get; set; }
        public int ScoreCriteriaId { get; set; }
        public string CriteriaName { get; set; }
        public int Points { get; set; }
        public DateTime ScoredDate { get; set; }
    }
}
