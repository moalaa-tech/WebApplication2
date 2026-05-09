using CRM.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.Project
{
    public class TimeEntryViewModel
    {
        public int Id { get; set; }
        public string PhaseName { get; set; }
        public string UserName { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }

        [Display(Name = "Hours")]
        public decimal Hours { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Status")]
        public TimeEntryStatus Status { get; set; }

        [Display(Name = "Rate")]
        [DataType(DataType.Currency)]
        public decimal Rate { get; set; }

        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        public decimal Total => Hours * Rate;
    }
}
