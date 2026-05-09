using CRM.Domain.Enums.ProjectManagment;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.Project
{
    public class ProjectDetailsViewModel
    {
        // Project Information
        public int Id { get; set; }
        public string ProjectCode { get; set; }

        [Display(Name = "Project Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Customer")]
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Status")]
        public ProjectStatus Status { get; set; }

        [Display(Name = "Budget")]
        [DataType(DataType.Currency)]
        public decimal Budget { get; set; }

        // Calculated Properties
        [Display(Name = "Duration")]
        public string Duration => EndDate.HasValue ?
            $"{(EndDate.Value - StartDate).TotalDays} days" :
            $"{(DateTime.Today - StartDate).TotalDays} days (ongoing)";

        [Display(Name = "Budget Utilization")]
        public string BudgetUtilization => $"{BudgetUtilizationPercentage:N1}%";
        public decimal BudgetUtilizationPercentage { get; set; }

        // Cost Summary
        public ProjectCostSummaryViewModel CostSummary { get; set; }

        // Related Data Collections
        public List<PhaseViewModel> Phases { get; set; } = new List<PhaseViewModel>();
        public List<TimeEntryViewModel> RecentTimeEntries { get; set; } = new List<TimeEntryViewModel>();
        public List<ExpenseViewModel> RecentExpenses { get; set; } = new List<ExpenseViewModel>();
        public List<ProjectNoteViewModel> RecentNotes { get; set; } = new List<ProjectNoteViewModel>();
        public List<ProjectDocumentViewModel> ImportantDocuments { get; set; } = new List<ProjectDocumentViewModel>();

        // UI Helpers
        public bool ShowFinancialData => Status != ProjectStatus.Proposal && Status != ProjectStatus.Planning;
        public bool IsActive => Status == ProjectStatus.Active;
        public bool IsCompleted => Status == ProjectStatus.Completed;

        // Navigation Properties
        public string ReturnUrl { get; set; }
    }
}
