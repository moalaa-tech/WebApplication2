using System.ComponentModel.DataAnnotations;


namespace CRM.Domain.Enums
{
    public enum ExpenseCategory
    {
        [Display(Name = "Materials")]
        Materials,

        [Display(Name = "Labor")]
        Labor,

        [Display(Name = "Equipment")]
        Equipment,

        [Display(Name = "Subcontractor")]
        Subcontractor,

        [Display(Name = "Travel")]
        Travel,

        [Display(Name = "Meals")]
        Meals,

        [Display(Name = "Lodging")]
        Lodging,

        [Display(Name = "Training")]
        Training,

        [Display(Name = "Software")]
        Software,

        [Display(Name = "Hardware")]
        Hardware,

        [Display(Name = "Office Supplies")]
        OfficeSupplies,

        [Display(Name = "Other")]
        Other
    }
}
