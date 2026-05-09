using CRM.WebApp.DTOs.SalesPipeline;
using CRM.WebApp.Paging;

namespace CRM.WebApp.ViewModels.SalesManagement
{
    public class QuoteListViewModel
    {
        public PaginatedList<QuoteDto> Quotes { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public QuoteFilterViewModel Filter { get; set; }
    }
}
