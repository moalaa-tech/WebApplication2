using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.SalesManagement
{
    public class QuoteLineItemViewModel
    {
        public int Id { get; set; }
        public int QuoteId { get; set; }

        [Required]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        [Range(0, 100)]
        public decimal DiscountPercentage { get; set; }

        public string Description { get; set; }

        public int LineTotal { get; set; }
    }
}
