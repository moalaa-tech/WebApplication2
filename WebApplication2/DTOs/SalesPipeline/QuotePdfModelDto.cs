
using CRM.WebApp.DTOs.Company;

namespace CRM.WebApp.DTOs.SalesPipeline
{
    public class QuotePdfModelDto
    {
        public QuoteDto Quote { get; internal set; }
        public DateTime GeneratedDate { get; internal set; }
        public CompanyDto Company { get; set; }
    }
}
