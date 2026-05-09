using AutoMapper;
using CRM.WebApp.DTOs.Accounting;
using CRM.WebApp.Services.Finance_Accounting;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.ViewModels.Accounting;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.Accounting
{
    public class AccountsReceivableController : Controller
    {
        private readonly IAccountsReceivableService _receivableService;
        private readonly IMapper _mapper;

        public AccountsReceivableController(IAccountsReceivableService receivableService, IMapper mapper)
        {
            _receivableService = receivableService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var receivables = await _receivableService.GetAllAsync();
            var viewModels = _mapper.Map<IEnumerable<AccountsReceivableViewModel>>(receivables);
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAccountsReceivableViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _receivableService.CreateAsync(_mapper.Map<CreateAccountsReceivableDto>(viewModel));
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error creating receivable: {ex.Message}");
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var receivable = await _receivableService.GetByIdAsync(id);
            if (receivable == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<AccountsReceivableViewModel>(receivable));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AccountsReceivableViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _receivableService.UpdateAsync(_mapper.Map<AccountsReceivableDto>(viewModel));
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating receivable: {ex.Message}");
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var receivable = await _receivableService.GetByIdAsync(id);
            if (receivable == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<AccountsReceivableViewModel>(receivable));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _receivableService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}