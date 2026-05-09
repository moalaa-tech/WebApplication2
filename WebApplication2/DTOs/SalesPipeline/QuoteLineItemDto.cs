namespace CRM.WebApp.DTOs.SalesPipeline
{
    public class QuoteLineItemDto
    {
        public int Id { get; set; }
        public int QuoteId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public string Description { get; set; }
        public decimal LineTotal => Quantity * UnitPrice * (1 - DiscountPercentage / 100);
    }
}
