using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.CustomerService
{
    public class SLADetailDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string ServiceType { get; set; }

        // Time metrics
        [Range(1, int.MaxValue)]
        public int ResponseTime { get; set; } // Hours
        [Range(1, int.MaxValue)]
        public int ResolutionTime { get; set; } // Hours
        public string ResponseTimeFormatted => $"{ResponseTime} hours";
        public string ResolutionTimeFormatted => $"{ResolutionTime} hours";

        // Status
        public bool IsActive { get; set; }
        public string Status => IsActive ? "Active" : "Inactive";
        public string StatusColor => IsActive ? "success" : "danger";

        // Dates
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }

        // Compliance metrics
        public double ComplianceRate { get; set; } // Percentage
        public string ComplianceRateDisplay => $"{ComplianceRate:0.0}%";
        public int TotalInstancesTracked { get; set; }
        public int MetSLACount { get; set; }
        public int BreachedCount { get; set; }

        // Escalation info
        [StringLength(2000)]
        public string EscalationProcess { get; set; }
        public List<string> EscalationContacts { get; set; } = new List<string>();

        // Terms
        [StringLength(4000)]
        public string TermsAndConditions { get; set; }

        // Related metrics
        public List<SLAMetricDto> RecentMetrics { get; set; } = new List<SLAMetricDto>();
        public SLAMetricSummaryDto TimeToResolutionStats { get; set; }
        public SLAMetricSummaryDto TimeToResponseStats { get; set; }

        // UI properties
        public string CardColor { get; set; } = "#3498db";
        public string Icon { get; set; } = "fa-file-contract";
    }
}
