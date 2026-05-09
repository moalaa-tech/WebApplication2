using AutoMapper;
using CRM.WebApp.DTOs.AccountsPayable;
using CRM.WebApp.Services.Finance_Accounting;
using CRM.WebApp.ViewModels;
using CRM.WebApp.ViewModels.AccountsPayable;
using Microsoft.AspNetCore.Mvc;
using InvoiceViewModel = CRM.WebApp.ViewModels.AccountsPayable.InvoiceViewModel;

namespace CRM.WebApp.Controllers.Accounting
{
    public class AccountsPayableController : Controller
    {
        private readonly IAccountsPayableService _service;
        private readonly IMapper _mapper;

        public AccountsPayableController(IAccountsPayableService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dtos = await _service.GetAllAsync();
            var viewModels = _mapper.Map<IEnumerable<InvoiceViewModel>>(dtos);
            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();
            var viewModel = _mapper.Map<InvoiceViewModel>(dto);
            return View(viewModel);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View(new InvoiceViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvoiceViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var dto = _mapper.Map<InvoiceDto>(viewModel);
            await _service.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();
            var viewModel = _mapper.Map<InvoiceViewModel>(dto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InvoiceViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();
            if (!ModelState.IsValid) return View(viewModel);
            var dto = _mapper.Map<InvoiceDto>(viewModel);
            await _service.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();
            var viewModel = _mapper.Map<InvoiceViewModel>(dto);
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}