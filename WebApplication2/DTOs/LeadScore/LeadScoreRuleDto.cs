using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.LeadScore
{
    public class LeadScoreRuleDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string RuleName { get; set; }

        [Required]
        [StringLength(500)]
        public string Condition { get; set; }

        [Required]
        [Range(0, 100)]
        public int Points { get; set; }

        [Required]
        public int ExecutionOrder { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
