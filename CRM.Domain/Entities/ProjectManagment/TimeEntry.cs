using CRM.Domain.Base;
using CRM.Domain.Enums;
using CRM.Domain.IdentityEntity;

namespace CRM.Domain.Entities.ProjectManagment
{
    public class TimeEntry : BaseEntity
    {
        public int JobPhaseId { get; set; }
        public JobPhase JobPhase { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal Hours { get; set; }
        public string Description { get; set; }
        public bool IsBillable { get; set; }
        public decimal Rate { get; set; }
        public TimeEntryStatus Status { get; set; } // Enum: Entered, Submitted, Approved, Billed
    }
}
