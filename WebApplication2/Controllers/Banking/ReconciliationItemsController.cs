using AutoMapper;
using CRM.WebApp.DTOs.Banking;
using CRM.WebApp.Services.Finance_Accounting;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.ViewModels.Banking;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.Banking
{
    public class ReconciliationItemsController : Controller
    {
        private readonly IReconciliationItemService _service;
        private readonly IMapper mapper;

        public ReconciliationItemsController(IReconciliationItemService service,
            IMapper _mapper
            )
        {
            _service = service;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var item = await _service.GetAllByReconciliationIdAsync(id);
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditReconciliationItemViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var EditReconciliationItemdto = mapper.Map<UpdateReconciliationItemDto>(vm);
            await _service.UpdateAsync(EditReconciliationItemdto);
            return RedirectToAction("Index", "Reconciliations");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("Index", "Reconciliations");
        }
    }
}
