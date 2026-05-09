using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.SalesManagement
{
    public class AssociateDealViewModel
    {
        public int QuoteId { get; set; }
        public string QuoteNumber { get; set; }

        [Required]
        [Display(Name = "Deal")]
        public int DealId { get; set; }

        public SelectList Deals { get; set; }
    }
}
