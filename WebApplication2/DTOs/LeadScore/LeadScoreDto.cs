namespace CRM.WebApp.DTOs.LeadScore
{
    public class LeadScoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }
        public bool IsActive { get; set; }
        public string Criteria { get; set; }
        public string ScoreType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public List<LeadScoreRuleDto> Rules { get; set; }
    }
}
