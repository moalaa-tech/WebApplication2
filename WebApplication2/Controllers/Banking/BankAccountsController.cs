using AutoMapper;
using CRM.WebApp.DTOs.Banking;
using CRM.WebApp.Services.Finance_Accounting;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.ViewModels.Banking;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.Banking
{
    public class BankAccountsController : Controller
    {
        private readonly IBankAccountService _service;
        private readonly IMapper _mapper;

        public BankAccountsController(IBankAccountService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var accounts = await _service.GetAllAsync();
            return View(_mapper.Map<List<BankAccountViewModel>>(accounts));
        }

        public async Task<IActionResult> Details(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return View(_mapper.Map<BankAccountViewModel>(dto));
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateBankAccountViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var dto = _mapper.Map<CreateBankAccountDto>(vm);
            await _service.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return View(_mapper.Map<UpdateBankAccountDto>(dto));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBankAccountDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _service.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return View(_mapper.Map<BankAccountViewModel>(dto));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
