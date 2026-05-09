using AutoMapper;
using CRM.Domain.Entities;
using CRM.Domain.Entities.InventoryManagement;
using CRM.Domain.Entities.SalesManagement;
using CRM.WebApp.DTOs.Company;
using CRM.WebApp.DTOs.InventoryManagement.Product;
using CRM.WebApp.DTOs.SalesPipeline;
using CRM.WebApp.Paging;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRM.WebApp.Services.SalesManagement
{
    public class QuoteService : IQuoteService
    {
        private readonly IRepository<Quote> _quoteRepository;
        private readonly IRepository<QuoteLineItem> _lineItemRepository;
        private readonly IRepository<Deal> _dealRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Company> CompanyRepository;

        private readonly IMapper _mapper;
        private readonly IAuthenticationService _userService;
        //private readonly IPdfService _pdfService;

        public QuoteService(
            IRepository<Quote> quoteRepository,
            IRepository<QuoteLineItem> lineItemRepository,
            IRepository<Deal> dealRepository,
            IRepository<Product> productRepository,
            IMapper mapper,
            IRepository<Company> _CompanyRepository,
            IAuthenticationService userService
            //IPdfService pdfService
            )
        {
            _quoteRepository = quoteRepository;
            _lineItemRepository = lineItemRepository;
            _dealRepository = dealRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _userService = userService;
            // _pdfService = pdfService;
            CompanyRepository = _CompanyRepository;
        }

        public async Task<QuoteDto> GetQuoteByIdAsync(int id)
        {
            var quote = await _quoteRepository.GetAsync(a => a.Id == id, includes: z => z.Include(u => u.LineItems).ThenInclude(li => li.Product).Include(q => q.Deals));



            return _mapper.Map<QuoteDto>(quote);
        }

        public async Task<IEnumerable<QuoteDto>> GetAllQuotesAsync()
        {
            var quotes = await _quoteRepository.GetAllAsync(includes: q => new { q.LineItems, q.Deals }).ToListAsync();

            return _mapper.Map<IEnumerable<QuoteDto>>(quotes);
        }

        public async Task<PaginatedList<QuoteDto>> GetQuotesPaginatedAsync(int pageNumber, int pageSize, QuoteFilterDto filter)
        {
            Expression<Func<Quote, bool>> predicate = q =>
                (string.IsNullOrEmpty(filter.SearchTerm) ||
                 q.QuoteNumber.Contains(filter.SearchTerm) ||
                 q.Notes.Contains(filter.SearchTerm)) &&
                (!filter.FromDate.HasValue || q.IssueDate >= filter.FromDate) &&
                (!filter.ToDate.HasValue || q.IssueDate <= filter.ToDate);

            var quotes = await _quoteRepository.GetPaginatedAsync(
                pageNumber,
                pageSize,
                predicate,
                orderBy: q => q.OrderByDescending(q => q.IssueDate),
                includes: q => q.Include(q => q.LineItems));

            return _mapper.Map<PaginatedList<QuoteDto>>(quotes);
        }

        public async Task<int> CreateQuoteAsync(CreateQuoteDto model)
        {
            var quote = _mapper.Map<Quote>(model);
            quote.QuoteNumber = await GenerateQuoteNumberAsync();
            quote.IssueDate = DateTime.UtcNow;
            quote.ExpiryDate = DateTime.UtcNow.AddDays(30); // Default 30-day expiry

            await _quoteRepository.AddAsync(quote);
            return quote.Id;
        }

        public async Task UpdateQuoteAsync(QuoteDto model)
        {
            var quote = await _quoteRepository.GetByIdAsync(model.Id);
            _mapper.Map(model, quote);
            _quoteRepository.Update(quote);
        }

        public async Task DeleteQuoteAsync(int id)
        {
            var quote = await _quoteRepository.GetByIdAsync(id);
            _quoteRepository.Update(quote);
        }

        public async Task<QuoteDto> GetQuoteViewModelForEditAsync(int id)
        {
            var quote = await GetQuoteByIdAsync(id);
            var model = _mapper.Map<QuoteDto>(quote);
            await PrepareQuoteDto(model);
            return model;
        }

        public async Task<QuoteDto> GetQuoteViewModelForCreateAsync()
        {
            var model = new QuoteDto
            {
                IssueDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(30)
            };
            await PrepareQuoteDto(model);
            return model;
        }

        private async Task PrepareQuoteDto(QuoteDto model)
        {
            var Products = await _productRepository.GetAllAsync().ToListAsync();
            model.Products = _mapper.Map<List<ProductDto>>(Products);
            // Add other necessary preparations
        }

        public async Task<QuotePdfModelDto> GenerateQuotePdfModelAsync(int quoteId)
        {
            var quote = await GetQuoteByIdAsync(quoteId);
            var companyInfo = await CompanyRepository.GetByIdAsync(5);

            return new QuotePdfModelDto
            {
                Quote = quote,
                Company = _mapper.Map<CompanyDto>(companyInfo),
                GeneratedDate = DateTime.UtcNow
            };
        }

        public async Task<string> GenerateQuoteNumberAsync()
        {
            var lastQuote = await _quoteRepository.GetAllAsync().OrderByDescending(q => q.Id).ToListAsync();
            var lastNumber = lastQuote.FirstOrDefault()?.QuoteNumber;

            int nextNumber = 1;
            if (!string.IsNullOrEmpty(lastNumber) && lastNumber.StartsWith("Q-") && int.TryParse(lastNumber[2..], out var num))
            {
                nextNumber = num + 1;
            }

            return $"Q-{nextNumber:D6}";
        }

        public async Task<IEnumerable<QuoteLineItemDto>> GetQuoteLineItemsAsync(int quoteId)
        {
            var lineItems = await _lineItemRepository.GetAllAsync(includes: q => q.Product).Where(li => li.QuoteId == quoteId).ToListAsync();

            return _mapper.Map<IEnumerable<QuoteLineItemDto>>(lineItems);
        }

        public async Task AddLineItemToQuoteAsync(QuoteLineItemDto model)
        {
            var lineItem = _mapper.Map<QuoteLineItem>(model);
            await _lineItemRepository.AddAsync(lineItem);
            await CalculateQuoteTotalAsync(model.QuoteId);
        }

        public async Task UpdateLineItemAsync(QuoteLineItemDto model)
        {
            var lineItem = await _lineItemRepository.GetByIdAsync(model.Id);
            _mapper.Map(model, lineItem);
            _lineItemRepository.Update(lineItem);
            await CalculateQuoteTotalAsync(model.QuoteId);
        }

        public async Task RemoveLineItemAsync(int lineItemId)
        {
            var lineItem = await _lineItemRepository.GetByIdAsync(lineItemId);
            var quoteId = lineItem.QuoteId;
            _lineItemRepository.Delete(lineItem);
            await CalculateQuoteTotalAsync(quoteId);
        }

        public async Task<decimal> CalculateQuoteTotalAsync(int quoteId)
        {
            var lineItems = await GetQuoteLineItemsAsync(quoteId);
            var subTotal = lineItems.Sum(li => li.Quantity * li.UnitPrice * (1 - li.DiscountPercentage / 100));
            var taxAmount = subTotal * 0.1m; // Example: 10% tax
            var totalAmount = subTotal + taxAmount;

            var quote = await _quoteRepository.GetByIdAsync(quoteId);
            quote.SubTotal = subTotal;
            quote.TaxAmount = taxAmount;
            quote.TotalAmount = totalAmount;
            _quoteRepository.Update(quote);

            return totalAmount;
        }

        public async Task<bool> AssociateWithDealAsync(int quoteId, int dealId)
        {
            var quote = await _quoteRepository.GetByIdAsync(quoteId);
            var deal = await _dealRepository.GetByIdAsync(dealId);

            if (quote == null || deal == null)
                return false;

            deal.QuoteId = quoteId;
            _dealRepository.Update(deal);
            return true;
        }


    }
}
