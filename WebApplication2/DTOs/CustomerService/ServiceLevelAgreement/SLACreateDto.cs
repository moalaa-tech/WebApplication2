using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.CustomerService
{
    public class SLACreateDto
    {
        [Required(ErrorMessage = "SLA name is required")]
        [StringLength(200, ErrorMessage = "SLA name cannot exceed 200 characters")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Service type is required")]
        [StringLength(100, ErrorMessage = "Service type cannot exceed 100 characters")]
        public string ServiceType { get; set; }

        [Required(ErrorMessage = "Response time is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Response time must be positive")]
        public int ResponseTime { get; set; } // in hours

        [Required(ErrorMessage = "Resolution time is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Resolution time must be positive")]
        public int ResolutionTime { get; set; } // in hours

        [StringLength(2000, ErrorMessage = "Escalation process cannot exceed 2000 characters")]
        public string EscalationProcess { get; set; }

        [StringLength(4000, ErrorMessage = "Terms cannot exceed 4000 characters")]
        public string TermsAndConditions { get; set; }

        public bool IsActive { get; set; } = true;

    }
}
