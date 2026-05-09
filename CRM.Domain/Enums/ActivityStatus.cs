using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Enums
{
    public enum ActivityStatus
    {
        [Display(Name = "Not Started")]
        NotStarted,
        [Display(Name = "In Progress")]
        InProgress,
        Completed,
        Cancelled,
        Deferred

    }
}
