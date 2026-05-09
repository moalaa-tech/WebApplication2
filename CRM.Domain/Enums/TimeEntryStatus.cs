using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Enums
{
    public enum TimeEntryStatus
    {
        [Display(Name = "Draft")]
        Draft,

        [Display(Name = "Submitted")]
        Submitted,

        [Display(Name = "Approved")]
        Approved,

        [Display(Name = "Rejected")]
        Rejected,

        [Display(Name = "Billed")]
        Billed,

        [Display(Name = "Paid")]
        Paid
    }
}
