using CRM.WebApp.DTOs.InventoryManagement.Product;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.SalesManagement
{
    public class QuoteViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Quote Number")]
        public string QuoteNumber { get; set; }

        [Required]
        [Display(Name = "Issue Date")]
        public DateTime IssueDate { get; set; }

        [Required]
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }

        [Display(Name = "Terms and Conditions")]
        public string TermsAndConditions { get; set; }

        public string Notes { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
        public IEnumerable<QuoteLineItemViewModel> LineItems { get; set; }
        public IEnumerable<DealviewModel> Deals { get; set; }
    }
}
