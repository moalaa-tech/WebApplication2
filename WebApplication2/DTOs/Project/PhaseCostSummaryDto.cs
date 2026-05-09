namespace CRM.WebApp.DTOs.Project
{
    public class PhaseCostSummaryDto
    {
        public int PhaseId { get; set; }
        public string PhaseName { get; set; }
        public string PhaseCode { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        // Estimated Values
        public decimal EstimatedHours { get; set; }
        public decimal EstimatedCost { get; set; }

        // Actual Values
        public decimal ActualHours { get; set; }
        public decimal ActualCost { get; set; }

        // Cost Breakdown
        public decimal LaborCost { get; set; }
        public decimal ExpenseCost { get; set; }

        // Variance Calculations
        public decimal HoursVariance => EstimatedHours - ActualHours;
        public decimal CostVariance => EstimatedCost - ActualCost;
        public decimal HoursVariancePercentage => EstimatedHours > 0 ?
            (HoursVariance / EstimatedHours) * 100 : 0;
        public decimal CostVariancePercentage => EstimatedCost > 0 ?
            (CostVariance / EstimatedCost) * 100 : 0;

        // Status Indicators
        public bool IsOverBudget => CostVariance < 0;
        public bool IsUnderBudget => CostVariance > 0;
        public bool IsOnBudget => Math.Abs(CostVariancePercentage) <= 5; // Within 5% variance

        // Timeline Information
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? EstimatedEndDate { get; set; }
        public int DaysBehindSchedule { get; set; }
        public decimal Variance { get; internal set; }
    }
}
