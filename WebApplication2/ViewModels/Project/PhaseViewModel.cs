using CRM.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.Project
{
    public class PhaseViewModel
    {
        public int Id { get; set; }
        public string PhaseCode { get; set; }
        public string Name { get; set; }

        [Display(Name = "Status")]
        public PhaseStatus Status { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Estimated Cost")]
        [DataType(DataType.Currency)]
        public decimal EstimatedCost { get; set; }

        [Display(Name = "Actual Cost")]
        [DataType(DataType.Currency)]
        public decimal ActualCost { get; set; }

        [Display(Name = "Variance")]
        [DataType(DataType.Currency)]
        public decimal CostVariance => EstimatedCost - ActualCost;

        public string ProgressStatus { get; set; }
        public int CompletionPercentage { get; set; }
    }
}
