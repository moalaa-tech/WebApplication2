namespace CRM.WebApp.DTOs.Project
{
    public class JobPhaseDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }

        // Phase Identification
        public string PhaseCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Estimates
        public decimal EstimatedHours { get; set; }
        public decimal EstimatedCost { get; set; }

        // Actuals
        public decimal ActualHours { get; set; }
        public decimal ActualCost { get; set; }

        // Variance Calculations
        public decimal HoursVariance => EstimatedHours - ActualHours;
        public decimal CostVariance => EstimatedCost - ActualCost;
        public decimal HoursVariancePercentage => EstimatedHours > 0 ? (HoursVariance / EstimatedHours) * 100 : 0;
        public decimal CostVariancePercentage => EstimatedCost > 0 ? (CostVariance / EstimatedCost) * 100 : 0;

        // Status Information
        public string Status { get; set; } // Enum mapped to string
        public bool IsCompleted => Status == "Completed";
        public bool IsActive => Status == "Active";
        public bool IsBehindSchedule { get; set; }

        // Timeline
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? EstimatedEndDate { get; set; }
        public int DaysBehind { get; set; }

        // Related Entities
        public IEnumerable<TimeEntryDto> TimeEntries { get; set; }
        public IEnumerable<PhaseExpenseDto> Expenses { get; set; }

        // Progress Tracking
        public decimal CompletionPercentage { get; set; }
        public string ProgressStatus { get; set; } // Ahead, On Track, Behind

        // Approval Information
        public bool IsApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
    }
}
