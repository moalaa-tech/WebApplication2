using CRM.Domain.Enums;

namespace CRM.WebApp.ViewModels.Project
{
    public class ProjectCostSummaryViewModel
    {
        public int PhaseId { get; set; }
        public string PhaseCode { get; set; }
        public string PhaseName { get; set; }
        public PhaseStatus Status { get; set; }

        public decimal EstimatedHours { get; set; }
        public decimal ActualHours { get; set; }
        public decimal HoursVariance => EstimatedHours - ActualHours;

        public decimal EstimatedCost { get; set; }
        public decimal ActualCost { get; set; }
        public decimal CostVariance => EstimatedCost - ActualCost;

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? DaysDuration => (EndDate - StartDate)?.Days;

        public decimal LaborCost { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal OtherCost { get; set; }

        public decimal CompletionPercentage { get; set; }
    }
}
