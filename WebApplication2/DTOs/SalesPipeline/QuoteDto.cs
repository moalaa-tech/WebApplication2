
using CRM.WebApp.DTOs.Deal;
using CRM.WebApp.DTOs.InventoryManagement.Product;

namespace CRM.WebApp.DTOs.SalesPipeline
{
    public class QuoteDto
    {
        public int Id { get; set; }
        public string QuoteNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string TermsAndConditions { get; set; }
        public string Notes { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public List<ProductDto> Products { get; internal set; }
        public IEnumerable<QuoteLineItemDto> LineItems { get; set; }
        public IEnumerable<DealDto> Deals { get; set; }


    }
}
