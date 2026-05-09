using AutoMapper;
using CRM.WebApp.DTOs.Accounting;
using CRM.WebApp.Paging;
using CRM.WebApp.Services.Finance_Accounting;
using CRM.WebApp.ViewModels.Accounting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ExpensesController : Controller
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
        viewModel.Page= res.Page;
        viewModel.TotalCount = res.TotalCount;
        viewModel.Categories = catItems;
        viewModel.From = query.From;
        viewModel.To = query.To;
        viewModel.CategoryId = query.CategoryId;
       // PagedResult<ExpenseListViewModel> Result = new PagedResult<ExpenseListViewModel>(Items, res.TotalCount, res.Page, res.PageSize);
        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var categories = await expenseCategoryService.GetAllAsync();
        CreateEditExpenseViewModel vm = new CreateEditExpenseViewModel
        {
            Categories = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString()))
        };
        return View(vm);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromBody] CreateEditExpenseViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var categories = await expenseCategoryService.GetAllAsync();
            var vm = new CreateEditExpenseViewModel
            {
                Categories = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString()))
            };
            return View(vm);
        }

        CreateExpenseDto dto = _mapper.Map<CreateExpenseDto>(model);
        var createdBy = 1;
        var result = await _service.CreateAsync(dto, createdBy);
        return RedirectToAction(nameof(Index));
    }



    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var e = await _service.GetByIdAsync(id);
        return View(e);
    }

 

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var dto = await _service.GetByIdAsync(id);
        if (dto == null) return NotFound();

        var categories = await expenseCategoryService.GetAllAsync();
        var vm = new CreateEditExpenseViewModel
        {
            Id = dto.Id,
            Title = dto.Title,
            TitleAR = dto.TitleAR,
            Notes = dto.Notes,
            Amount = dto.Amount,
            Date = dto.Date,
            CategoryId = dto.CategoryId,
            Categories = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString()))
        };
        return View(vm);
    }

    [HttpPut("{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, [FromBody] CreateEditExpenseViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var categories = await expenseCategoryService.GetAllAsync();
            model.Categories = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString()));
            return View(model);
        }
        CreateExpenseDto dto = _mapper.Map<CreateExpenseDto>(model);
        var ok = await _service.UpdateAsync(id, dto);
        return RedirectToAction(nameof(Index));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> MonthlyReport(int year)
    {
        var r = await _service.GetMonthlyReportAsync(year);
        return View(r);
    }

    [HttpGet]
    public async Task<IActionResult> ExpensesDashboard(int? year)
    {
        int y = year ?? DateTime.UtcNow.Year;
        var rows = await _service.GetMonthlyReportAsync(y);

        // ترتيب وتحويل لمصفوفة 12 شهر
        var totals = Enumerable.Range(1, 12).Select(m => rows.FirstOrDefault(r => r.Month == m)?.Total ?? 0M).ToArray();

        ViewBag.Year = y;
        ViewBag.MonthlyTotals = totals;
        return View();
    }
}
