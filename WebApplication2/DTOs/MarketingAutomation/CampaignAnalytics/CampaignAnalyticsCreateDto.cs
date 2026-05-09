using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.MarketingAutomation.CampaignAnalytics
{
    public class CampaignAnalyticsCreateDto
    {
        [Required]
        public Guid CampaignId { get; set; }

        [Required]
        [StringLength(100)]
        public string CampaignName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalBudget { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalSpent { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Impressions { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Clicks { get; set; }

        [Range(0, int.MaxValue)]
        public int Conversions { get; set; }

        [Range(0, 100)]
        public decimal ConversionRate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal CostPerClick { get; set; }

        [Range(0, double.MaxValue)]
        public decimal CostPerConversion { get; set; }

        [Range(0, double.MaxValue)]
        public decimal RevenueGenerated { get; set; }

        public decimal ROIPercentage { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        [Required]
        public string Channel { get; set; }

    }
}
