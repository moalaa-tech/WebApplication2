using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.CustomerService
{
    public class DepartmentStatsViewModel
    {
        [Display(Name = "Department")]
        public string Department { get; set; }

        [Display(Name = "Total Agents")]
        public int AgentCount { get; set; }

        [Display(Name = "Active Agents")]
        public int ActiveAgentCount { get; set; }

        [Display(Name = "Avg. Rating")]
        [DisplayFormat(DataFormatString = "{0:0.0}")]
        public double AverageRating { get; set; }

        [Display(Name = "Tickets Resolved")]
        public int TotalTicketsResolved { get; set; }

        [Display(Name = "Avg. Resolution Time (hrs)")]
        [DisplayFormat(DataFormatString = "{0:0.0}")]
        public double AverageResolutionTime { get; set; }

        [Display(Name = "SLA Compliance")]
        [DisplayFormat(DataFormatString = "{0:0.0%}")]
        public double SLAComplianceRate { get; set; }

        // Calculated properties for UI
        [Display(Name = "Utilization")]
        [DisplayFormat(DataFormatString = "{0:0.0}")]
        public double UtilizationPercentage =>
            AgentCount > 0 ? (double)TotalTicketsResolved / AgentCount : 0;

        [Display(Name = "Active Ratio")]
        [DisplayFormat(DataFormatString = "{0:0.0%}")]
        public double ActiveRatio =>
            AgentCount > 0 ? (double)ActiveAgentCount / AgentCount : 0;
    }
}
