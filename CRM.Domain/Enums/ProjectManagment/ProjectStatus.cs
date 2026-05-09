using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Enums.ProjectManagment
{
    public enum ProjectStatus
    {
        [Display(Name = "Proposal")]
        Proposal,

        [Display(Name = "Planning")]
        Planning,

        [Display(Name = "Active")]
        Active,

        [Display(Name = "On Hold")]
        OnHold,

        [Display(Name = "Completed")]
        Completed,

        [Display(Name = "Cancelled")]
        Cancelled,

        [Display(Name = "Archived")]
        Archived
    }
}
