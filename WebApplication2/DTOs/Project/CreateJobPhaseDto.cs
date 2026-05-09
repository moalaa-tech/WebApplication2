using CRM.Domain.Enums;
using CRM.WebApp.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.Project
{
    public class CreateJobPhaseDto
    {
        [Required(ErrorMessage = "Project ID is required")]
        [Display(Name = "Project")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Phase code is required")]
        [StringLength(20, ErrorMessage = "Phase code cannot exceed 20 characters")]
        [Display(Name = "Phase Code")]
        public string PhaseCode { get; set; }

        [Required(ErrorMessage = "Phase name is required")]
        [StringLength(100, ErrorMessage = "Phase name cannot exceed 100 characters")]
        [Display(Name = "Phase Name")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Estimated hours are required")]
        [Range(0.1, 10000, ErrorMessage = "Estimated hours must be between 0.1 and 10,000")]
        [Display(Name = "Estimated Hours")]
        public decimal EstimatedHours { get; set; }

        [Required(ErrorMessage = "Estimated cost is required")]
        [Range(0.01, 10000000, ErrorMessage = "Estimated cost must be between $0.01 and $10,000,000")]
        [DataType(DataType.Currency)]
        [Display(Name = "Estimated Cost")]
        public decimal EstimatedCost { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DateAfterProjectStart]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DateAfterStartDate(nameof(StartDate))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Initial Status")]
        public PhaseStatus Status { get; set; } = PhaseStatus.Planning;

        // Hidden fields for validation context
        public DateTime ProjectStartDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }
    }
}
