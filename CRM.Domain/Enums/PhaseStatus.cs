using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Enums
{
    public enum PhaseStatus
    {
        Planning,
        Active,
        [Display(Name = "Not Started")]
        NotStarted,

        [Display(Name = "In Progress")]
        InProgress,

        [Display(Name = "Completed")]
        Completed,

        [Display(Name = "Delayed")]
        Delayed,

        [Display(Name = "Approval Pending")]
        ApprovalPending
    }
}
