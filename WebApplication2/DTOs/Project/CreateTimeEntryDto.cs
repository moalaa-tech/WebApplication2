using CRM.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.Project
{
    public class CreateTimeEntryDto
    {
        [Required]
        [Display(Name = "Job Phase")]
        public int JobPhaseId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Entry Date")]
        public DateTime EntryDate { get; set; } = DateTime.Today;

        [Required]
        [Range(0.1, 24)]
        [Display(Name = "Hours Worked")]
        public decimal Hours { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Billable?")]
        public bool IsBillable { get; set; } = true;

        [Required]
        [Range(0, 999)]
        [DataType(DataType.Currency)]
        [Display(Name = "Hourly Rate")]
        public decimal Rate { get; set; }

        [Display(Name = "Status")]
        public TimeEntryStatus Status { get; set; } = TimeEntryStatus.Submitted;

        // Additional fields for validation
        public DateTime PhaseStartDate { get; set; }
        public DateTime? PhaseEndDate { get; set; }
        public decimal MaxDailyHours { get; set; } = 24;
    }
}
