using CRM.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.Project
{
    public class ExpenseViewModel
    {
        public int Id { get; set; }
        public string PhaseName { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime ExpenseDate { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Category")]
        public ExpenseCategory Category { get; set; }

        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Display(Name = "Vendor")]
        public string Vendor { get; set; }

        [Display(Name = "Status")]
        public ExpenseStatus Status { get; set; }

        [Display(Name = "Billable")]
        public bool IsBillable { get; set; }
    }
}
