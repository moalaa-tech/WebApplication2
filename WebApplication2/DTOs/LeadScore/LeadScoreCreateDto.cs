using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.LeadScore
{
    public class LeadScoreCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0, 100)]
        public int Score { get; set; } = 0;

        [Required]
        public string Criteria { get; set; }

        [Required]
        [StringLength(50)]
        public string ScoreType { get; set; }

        public List<LeadScoreRuleDto> Rules { get; set; }
    }
}
