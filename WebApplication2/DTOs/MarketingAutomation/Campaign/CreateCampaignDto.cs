using CRM.Domain.Enums.MarketingAutomation;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.MarketingAutomation.Campaign
{
    public class CreateCampaignDto
    {

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Budget { get; set; }

        public CampaignStatus Status { get; set; }

        public int? EmailTemplateId { get; set; }
        public string EmailTemplateName { get; set; }

        public int ContactCount { get; set; }
        public int InteractionCount { get; set; }
    }
}
