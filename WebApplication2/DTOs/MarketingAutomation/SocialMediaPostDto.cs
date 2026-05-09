using CRM.Domain.Enums.MarketingAutomation;
using CRM.WebApp.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.MarketingAutomation
{
    public class SocialMediaPostDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(2000)]
        public string Content { get; set; }

        [Required]
        [FutureDate(ErrorMessage = "Scheduled time must be in the future")]
        public DateTime ScheduledTime { get; set; }

        [Required]
        public SocialMediaPlatform Platform { get; set; }

        public PostStatus Status { get; set; }
    }
}
