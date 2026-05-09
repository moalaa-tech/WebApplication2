using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.CustomerService
{
    public class SLAMonitoringCompleteDto
    {
        [Required(ErrorMessage = "Completion status is required")]
        public bool IsMet { get; set; }

        [StringLength(2000, ErrorMessage = "Notes cannot exceed 2000 characters")]
        public string Notes { get; set; }

        public Dictionary<string, string> AdditionalMetrics { get; set; } = new Dictionary<string, string>();
    }
}
