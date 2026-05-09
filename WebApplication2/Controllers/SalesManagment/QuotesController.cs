using AutoMapper;
using CRM.WebApp.DTOs.SalesPipeline;
using CRM.WebApp.Services.InventoryManagment;
using CRM.WebApp.Services.SalesManagement;
using CRM.WebApp.ViewModels.SalesManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.Controllers.SalesManagment
{
    //[Authorize]
    public class QuotesController : Controller
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
        public async Task<IActionResult> Index(int? page, string searchTerm, DateTime? fromDate, DateTime? toDate)
        {
            var filter = new QuoteFilterViewModel
            {
                SearchTerm = searchTerm,
                FromDate = fromDate,
                ToDate = toDate
            };

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

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var quote = await _quoteService.GetQuoteByIdAsync(id);
            if (quote == null)
            {
                return NotFound();
            }

            return View(quote);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await _quoteService.GetQuoteViewModelForCreateAsync();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var CreateQuoteDto = Mapper.Map<CreateQuoteDto>(model);

                var quoteId = await _quoteService.CreateQuoteAsync(CreateQuoteDto);
                return RedirectToAction(nameof(Details), new { id = quoteId });
            }

            await PrepareQuoteViewModel(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _quoteService.GetQuoteViewModelForEditAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, QuoteViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var QuoteDto = Mapper.Map<QuoteDto>(model);

                await _quoteService.UpdateQuoteAsync(QuoteDto);
                return RedirectToAction(nameof(Details), new { id });
            }

            await PrepareQuoteViewModel(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var quote = await _quoteService.GetQuoteByIdAsync(id);
            if (quote == null)
            {
                return NotFound();
            }

            return View(quote);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _quoteService.DeleteQuoteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //[HttpGet]]  
        //public async Task<IActionResult> GeneratePdf(int id)
        //{
        //    var pdfModel = await _quoteService.GenerateQuotePdfModelAsync(id);
        //    var pdfBytes = await _pdfService.GenerateQuotePdfAsync(pdfModel);

        //    return File(pdfBytes, "application/pdf", $"Quote_{pdfModel.Quote.QuoteNumber}.pdf");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLineItem(QuoteLineItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var QuoteLineItemDto = Mapper.Map<QuoteLineItemDto>(model);

                await _quoteService.AddLineItemToQuoteAsync(QuoteLineItemDto);
            }

            return RedirectToAction(nameof(Edit), new { id = model.QuoteId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLineItem(QuoteLineItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var QuoteLineItemDto = Mapper.Map<QuoteLineItemDto>(model);

                await _quoteService.UpdateLineItemAsync(QuoteLineItemDto);
            }

            return RedirectToAction(nameof(Edit), new { id = model.QuoteId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLineItem(int id, int quoteId)
        {
            await _quoteService.RemoveLineItemAsync(id);
            return RedirectToAction(nameof(Edit), new { id = quoteId });
        }

        [HttpGet]
        public async Task<IActionResult> AssociateDeal(int id)
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

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssociateDeal(AssociateDealViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _quoteService.AssociateWithDealAsync(model.QuoteId, model.DealId);
                if (result)
                {
                    return RedirectToAction(nameof(Details), new { id = model.QuoteId });
                }
            }

            return View(model);
        }

        private async Task PrepareQuoteViewModel(QuoteViewModel model)
        {
            model.Products = await _productService.GetAllProductsAsync();
        }
    }
}
