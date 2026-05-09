using System.ComponentModel.DataAnnotations;


namespace CRM.Domain.Enums
{
    public enum ExpenseStatus
    {
        [Display(Name = "Entered")]
        Entered,

        [Display(Name = "Submitted")]
        Submitted,

        [Display(Name = "Approved")]
        Approved,

        [Display(Name = "Rejected")]
        Rejected,

        [Display(Name = "Paid")]
        Paid
    }
}
