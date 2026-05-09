using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.SalesManagement
{
    public class CreateQuoteViewModel
    {
        public int Id { get; set; }



        [Required]
        [Display(Name = "Issue Date")]
        public DateTime IssueDate { get; set; }

        [Required]
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }

        [Display(Name = "Terms and Conditions")]
        public string TermsAndConditions { get; set; }

        public string Notes { get; set; }
    }
}
