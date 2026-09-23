using AutoMapper;
using CRM.WebApp.DTOs.Accounting;
using CRM.WebApp.Paging;
using CRM.WebApp.Services.Finance_Accounting;
using CRM.WebApp.ViewModels.Accounting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication2.Api.Controllers.Accounting
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _service;
        private readonly IMapper _mapper;
        private readonly IExpenseCategoryService expenseCategoryService;

        public ExpensesController(IExpenseService service, IMapper mapper, IExpenseCategoryService expenseCategoryService)
        {
            _service = service;
            _mapper = mapper;
            this.expenseCategoryService = expenseCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] ExpenseQuery query)
        {
            var categories = await expenseCategoryService.GetAllAsync();
            var catItems = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();

            var res = await _service.GetPagedAsync(query);
            var Items = _mapper.Map<List<ExpenseListViewModel>>(res.Items);

            ExpenseListViewModel viewModel = new ExpenseListViewModel();
            viewModel.Items = res.Items;
            viewModel.PageSize = res.PageSize;
            viewModel.Page = res.Page;
            viewModel.TotalCount = res.TotalCount;
            viewModel.Categories = catItems;
            viewModel.From = query.From;
            viewModel.To = query.To;
            viewModel.CategoryId = query.CategoryId;
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEditExpenseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CreateExpenseDto dto = _mapper.Map<CreateExpenseDto>(model);
            var createdBy = 1;
            var result = await _service.CreateAsync(dto, createdBy);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var e = await _service.GetByIdAsync(id);
            return Ok(e);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateEditExpenseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CreateExpenseDto dto = _mapper.Map<CreateExpenseDto>(model);
            var ok = await _service.UpdateAsync(id, dto);
            return Ok(model);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }

        [HttpGet("MonthlyReport")]
        public async Task<IActionResult> MonthlyReport([FromQuery] int? year)
        {
            var r = await _service.GetMonthlyReportAsync(year ?? DateTime.UtcNow.Year);
            return Ok(r);
        }

        [HttpGet("ExpensesDashboard")]
        public async Task<IActionResult> ExpensesDashboard([FromQuery] int? year)
        {
            int y = year ?? DateTime.UtcNow.Year;
            var rows = await _service.GetMonthlyReportAsync(y);

            var totals = Enumerable.Range(1, 12).Select(m => rows.FirstOrDefault(r => r.Month == m)?.Total ?? 0M).ToArray();

            return Ok(new { year = y, monthlyTotals = totals });
        }
    }
}