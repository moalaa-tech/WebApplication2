using CRM.Domain.Base;
using CRM.Domain.Entities.AccountsReceivable;
using CRM.Domain.Enums.ProjectManagment;


namespace CRM.Domain.Entities.ProjectManagment
{
    public class Project : BaseEntity
    {
        public string ProjectCode { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Description { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectStatus Status { get; set; } // Enum: Planning, Active, OnHold, Completed
        public decimal Budget { get; set; }
        public ICollection<JobPhase> Phases { get; set; }
        public ICollection<ProjectNote> Notes { get; set; }
        public ICollection<ProjectDocument> Documents { get; set; }

        public ICollection<ProjectTask> Tasks { get; set; }
        public ICollection<ProjectCost> Costs { get; set; }
        public ICollection<TimeEntry> TimeEntries { get; set; }
    }
}
