using CRM.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.Project
{
    public class UpdateJobPhaseDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Phase Code")]
        public string PhaseCode { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Phase Name")]
        public string Name { get; set; }

        [StringLength(500)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Range(0, 999999)]
        [Display(Name = "Estimated Hours")]
        public decimal EstimatedHours { get; set; }

        [Range(0, 999999999)]
        [DataType(DataType.Currency)]
        [Display(Name = "Estimated Cost")]
        public decimal EstimatedCost { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Required]
        [Display(Name = "Status")]
        public PhaseStatus Status { get; set; }

        // Audit fields (read-only in UI)
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
