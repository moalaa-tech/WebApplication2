using CRM.Domain.Enums.ProjectManagment;

namespace CRM.WebApp.DTOs.Project
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string ProjectCode { get; set; }
        public string Name { get; set; }
        public string CustomerName { get; set; }
        public string Description { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectStatus Status { get; set; } // Enum: Planning, Active, OnHold, Completed
        public decimal Budget { get; set; }
        public IEnumerable<JobPhaseDto> Phases { get; set; }
        public decimal TotalHours { get; internal set; }
        public decimal TotalExpenses { get; internal set; }
        public decimal RemainingBudget { get; internal set; }
    }
}
