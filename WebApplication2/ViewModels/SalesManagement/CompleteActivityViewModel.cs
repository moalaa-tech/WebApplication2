using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.SalesManagement
{
    public class CompleteActivityViewModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public DateTime DueDate { get; set; }

        [Display(Name = "Outcome Notes")]
        public string OutcomeNotes { get; set; }
    }
}
