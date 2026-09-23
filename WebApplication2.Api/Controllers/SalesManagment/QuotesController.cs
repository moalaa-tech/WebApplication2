using AutoMapper;
using CRM.WebApp.DTOs.SalesPipeline;
using CRM.WebApp.Services.InventoryManagment;
using CRM.WebApp.Services.SalesManagement;
using CRM.WebApp.ViewModels.SalesManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication2.Api.Controllers.SalesManagment
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class QuotesController : ControllerBase
    {
        private readonly IQuoteService _quoteService;
        private readonly IProductService _productService;
        private readonly IDealService _dealService;
        private readonly IMapper Mapper;

        public QuotesController(
            IQuoteService quoteService,
            IProductService productService,
            IMapper mapper,
            IDealService dealService)
        {
            _quoteService = quoteService;
            _productService = productService;
            _dealService = dealService;
            Mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int? page, [FromQuery] string? searchTerm, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            var filter = new QuoteFilterViewModel();
            SetFilterValue(filter, nameof(QuoteFilterViewModel.SearchTerm), searchTerm);
            SetFilterValue(filter, nameof(QuoteFilterViewModel.FromDate), fromDate);
            SetFilterValue(filter, nameof(QuoteFilterViewModel.ToDate), toDate);

            int pageSize = 10;
            int pageNumber = page ?? 1;
            var QuoteFilterDto = Mapper.Map<QuoteFilterDto>(filter);
            var quotes = await _quoteService.GetQuotesPaginatedAsync(pageNumber, pageSize, QuoteFilterDto);

            var model = new QuoteListViewModel
            {
                Quotes = quotes,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = pageNumber,
                    ItemsPerPage = pageSize,
                    TotalItems = quotes.TotalPages
                },
                Filter = filter
            };

            return Ok(model);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var quote = await _quoteService.GetQuoteByIdAsync(id);
            if (quote == null)
            {
                return NotFound();
            }

            return Ok(quote);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuoteViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var CreateQuoteDto = Mapper.Map<CreateQuoteDto>(model);

            var quoteId = await _quoteService.CreateQuoteAsync(CreateQuoteDto);
            return Ok(model);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] QuoteViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var QuoteDto = Mapper.Map<QuoteDto>(model);

            await _quoteService.UpdateQuoteAsync(QuoteDto);
            return Ok(model);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _quoteService.DeleteQuoteAsync(id);
            return Ok();
        }

        //[HttpGet]]
        //public async Task<IActionResult> GeneratePdf(int id)
        //{
        //    var pdfModel = await _quoteService.GenerateQuotePdfModelAsync(id);
        //    var pdfBytes = await _pdfService.GenerateQuotePdfAsync(pdfModel);

        //    return File(pdfBytes, "application/pdf", $"Quote_{pdfModel.Quote.QuoteNumber}.pdf");
        //}

        [HttpPost("AddLineItem")]
        public async Task<IActionResult> AddLineItem([FromBody] QuoteLineItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var QuoteLineItemDto = Mapper.Map<QuoteLineItemDto>(model);

                await _quoteService.AddLineItemToQuoteAsync(QuoteLineItemDto);
            }

            return Ok(model);
        }

        [HttpPost("UpdateLineItem")]
        public async Task<IActionResult> UpdateLineItem([FromBody] QuoteLineItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var QuoteLineItemDto = Mapper.Map<QuoteLineItemDto>(model);

                await _quoteService.UpdateLineItemAsync(QuoteLineItemDto);
            }

            return Ok(model);
        }

        [HttpPost("RemoveLineItem")]
        public async Task<IActionResult> RemoveLineItem([FromQuery] int id, [FromQuery] int quoteId)
        {
            await _quoteService.RemoveLineItemAsync(id);
            return Ok();
        }

        [HttpGet("AssociateDeal")]
        public async Task<IActionResult> AssociateDeal([FromQuery] int id)
        {
            var quote = await _quoteService.GetQuoteByIdAsync(id);
            if (quote == null)
            {
                return NotFound();
            }

            var deals = await _dealService.GetDealsAvailableForQuoteAsync(id);

            var model = new AssociateDealViewModel
            {
                QuoteId = id,
                QuoteNumber = quote.QuoteNumber,
                Deals = new SelectList(deals, "Id", "Name")
            };

            return Ok(model);
        }

        [HttpPost("AssociateDeal")]
        public async Task<IActionResult> AssociateDeal([FromBody] AssociateDealViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _quoteService.AssociateWithDealAsync(model.QuoteId, model.DealId);
            return Ok(model);
        }

        private static void SetFilterValue(QuoteFilterViewModel filter, string propertyName, object? value)
        {
            typeof(QuoteFilterViewModel).GetProperty(propertyName)?.SetValue(filter, value);
        }
    }
}