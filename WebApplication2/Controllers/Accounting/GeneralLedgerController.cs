using AutoMapper;
using CRM.WebApp.DTOs.Accounting;
using CRM.WebApp.Services.Finance_Accounting;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.ViewModels.Accounting;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.Accounting
{
    public class GeneralLedgerController : Controller
    {
        private readonly IGeneralLedgerService _ledgerService;
        private readonly IMapper _mapper;

        public GeneralLedgerController(IGeneralLedgerService ledgerService, IMapper mapper)
        {
            _ledgerService = ledgerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var ledgers = await _ledgerService.GetAllAsync();
            var viewModels = _mapper.Map<IEnumerable<GeneralLedgerViewModel>>(ledgers);
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGeneralLedgerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = _mapper.Map<CreateGeneralLedgerDto>(viewModel);
                    await _ledgerService.CreateAsync(dto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error creating ledger: {ex.Message}");
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var ledger = await _ledgerService.GetByIdAsync(id);
            if (ledger == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<GeneralLedgerViewModel>(ledger));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GeneralLedgerViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _ledgerService.UpdateAsync(_mapper.Map<GeneralLedgerDTO>(viewModel));
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating ledger: {ex.Message}");
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var ledger = await _ledgerService.GetByIdAsync(id);
            if (ledger == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<GeneralLedgerViewModel>(ledger));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ledgerService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostTransaction(int id)
        {
            await _ledgerService.PostTransactionAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
