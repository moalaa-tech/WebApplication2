using CRM.Domain.Base;

namespace CRM.Domain.Entities.SalesManagement
{
    public class Quote : BaseEntity
    {
        public string QuoteNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string TermsAndConditions { get; set; }
        public string Notes { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual ICollection<QuoteLineItem> LineItems { get; set; }
        public virtual ICollection<Deal> Deals { get; set; }
    }
}
