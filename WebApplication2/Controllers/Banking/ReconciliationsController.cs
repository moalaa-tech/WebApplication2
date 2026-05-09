using AutoMapper;
using CRM.WebApp.DTOs.Banking;
using CRM.WebApp.Services.Finance_Accounting;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.ViewModels.Banking;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.Banking
{
    public class ReconciliationsController : Controller
    {
        private readonly IReconciliationService _service;
        private readonly IMapper _mapper;

        public ReconciliationsController(IReconciliationService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _service.GetAllAsync();
            return View(items);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateReconciliationViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var dto = _mapper.Map<CreateReconciliationDto>(vm);
            await _service.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var rec = await _service.GetByIdAsync(id);
            var items = await _service.GetItemsAsync(id);
            ViewBag.Items = items;
            return View(rec);
        }
    }
}
