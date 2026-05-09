using CRM.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.Project
{
    public class UpdateTimeEntryDto
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Entry Date")]
        public DateTime EntryDate { get; set; }

        [Required]
        [Range(0.1, 24)]
        [Display(Name = "Hours Worked")]
        public decimal Hours { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Billable?")]
        public bool IsBillable { get; set; }

        [Required]
        [Range(0, 999)]
        [DataType(DataType.Currency)]
        [Display(Name = "Hourly Rate")]
        public decimal Rate { get; set; }

        // Status can only be updated through specific actions
        public TimeEntryStatus Status { get; set; }

        // Audit fields
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        // Additional fields for validation
        public DateTime PhaseStartDate { get; set; }
        public DateTime? PhaseEndDate { get; set; }
        public decimal MaxDailyHours { get; set; } = 24;
    }
}
