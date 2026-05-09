using AutoMapper;
using CRM.Domain.Enums;
using CRM.WebApp.DTOs.Banking;
using CRM.WebApp.Services.Finance_Accounting;
using CRM.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.Banking
{
    public class BankTransactionsController : Controller
    {
        private readonly IBankTransactionService _service;
        private readonly IMapper _mapper;

        public BankTransactionsController(IBankTransactionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var transactions = await _service.GetAllAsync();
            return View(transactions);
        }

        [HttpGet]
        public async Task<IActionResult> Filter(DateTime? from, DateTime? to, TransactionType? type)
        {
            var all = await _service.GetAllAsync();

            var filtered = all.Where(t =>
                (!from.HasValue || t.TransactionDate >= from.Value) &&
                (!to.HasValue || t.TransactionDate <= to.Value) &&
                (!type.HasValue || t.Type == type.Value)
            );

            return View("Index", filtered);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateBankTransactionDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _service.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await _service.GetByIdAsync(id);
            return View(transaction);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
