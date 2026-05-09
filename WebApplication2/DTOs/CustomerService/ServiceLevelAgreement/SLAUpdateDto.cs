using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.CustomerService
{
    public class SLAUpdateDto
    {
        [Required(ErrorMessage = "SLA ID is required")]
        public int Id { get; set; }

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
        public int ResponseTime { get; set; }

        [Required(ErrorMessage = "Resolution time is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Resolution time must be positive")]
        public int ResolutionTime { get; set; }

        [StringLength(2000, ErrorMessage = "Escalation process cannot exceed 2000 characters")]
        public string EscalationProcess { get; set; }

        [StringLength(4000, ErrorMessage = "Terms cannot exceed 4000 characters")]
        public string TermsAndConditions { get; set; }
    }
}
