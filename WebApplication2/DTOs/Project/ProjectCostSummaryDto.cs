namespace CRM.WebApp.DTOs.Project
{
    public class ProjectCostSummaryDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }

        // Budget Information
        public decimal Budget { get; set; }
        public decimal TotalEstimatedCost { get; set; }
        public decimal TotalActualCost { get; set; }
        public decimal Variance => Budget - TotalActualCost;
        public decimal VariancePercentage => Budget > 0 ? (Variance / Budget) * 100 : 0;

        // Cost Breakdown
        public decimal LaborCost { get; set; }
        public decimal ExpenseCost { get; set; }
        public decimal TotalCost => LaborCost + ExpenseCost;

        // Hours Information
        public decimal TotalHours { get; set; }
        public decimal EstimatedHours { get; set; }
        public decimal HoursVariance => EstimatedHours - TotalHours;
        public decimal HoursVariancePercentage => EstimatedHours > 0 ? (HoursVariance / EstimatedHours) * 100 : 0;

        // Status Indicators
        public bool IsOverBudget => Variance < 0;
        public bool IsOnBudget => Math.Abs(VariancePercentage) <= 5; // Within 5% is considered on budget
        public bool IsUnderBudget => Variance > 0 && !IsOnBudget;

        // Timeline Information
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? EstimatedCompletionDate { get; set; }
        public int DaysBehindSchedule { get; set; }

        // Phase Counts
        public int TotalPhases { get; set; }
        public int CompletedPhases { get; set; }
        public int ActivePhases { get; set; }
        public int PlannedPhases { get; set; }
    }
}
