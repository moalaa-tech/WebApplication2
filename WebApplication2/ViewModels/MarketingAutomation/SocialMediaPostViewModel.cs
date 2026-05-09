using CRM.Domain.Enums.MarketingAutomation;
using CRM.WebApp.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.MarketingAutomation
{
    public class SocialMediaPostViewModel
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

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
