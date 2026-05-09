using CRM.WebApp.DTOs.SalesPipeline;
using CRM.WebApp.Paging;

namespace CRM.WebApp.Services.SalesManagement
{
    public interface IQuoteService
    {
        Task<QuoteDto> GetQuoteByIdAsync(int id);
        Task<IEnumerable<QuoteDto>> GetAllQuotesAsync();
        Task<PaginatedList<QuoteDto>> GetQuotesPaginatedAsync(int pageNumber, int pageSize, QuoteFilterDto filter);
        Task<int> CreateQuoteAsync(CreateQuoteDto model);
        Task UpdateQuoteAsync(QuoteDto model);
        Task DeleteQuoteAsync(int id);
        Task<QuoteDto> GetQuoteViewModelForEditAsync(int id);
        Task<QuoteDto> GetQuoteViewModelForCreateAsync();
        Task<QuotePdfModelDto> GenerateQuotePdfModelAsync(int quoteId);
        Task<string> GenerateQuoteNumberAsync();
        Task<IEnumerable<QuoteLineItemDto>> GetQuoteLineItemsAsync(int quoteId);
        Task AddLineItemToQuoteAsync(QuoteLineItemDto model);
        Task UpdateLineItemAsync(QuoteLineItemDto model);
        Task RemoveLineItemAsync(int lineItemId);
        Task<decimal> CalculateQuoteTotalAsync(int quoteId);
        Task<bool> AssociateWithDealAsync(int quoteId, int dealId);
    }
}
