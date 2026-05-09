namespace CRM.WebApp.DTOs.SalesPipeline
{
    public class UpdateQuoteDto
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
    }
}
