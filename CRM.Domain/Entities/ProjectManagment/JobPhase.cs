using CRM.Domain.Base;
using CRM.Domain.Enums;

namespace CRM.Domain.Entities.ProjectManagment
{
    public class JobPhase : BaseEntity
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string PhaseCode { get; set; }
        public string Name { get; set; }
        public decimal EstimatedHours { get; set; }
        public decimal EstimatedCost { get; set; }
        public decimal ActualHours { get; set; }
        public decimal ActualCost { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public PhaseStatus Status { get; set; }
        public ICollection<TimeEntry> TimeEntries { get; set; }
        public ICollection<PhaseExpense> Expenses { get; set; }
    }
}
