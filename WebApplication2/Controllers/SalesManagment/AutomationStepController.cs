using AutoMapper;
using CRM.WebApp.DTOs.AutomationStep;
using CRM.WebApp.Services.MarketingAutomation;
using CRM.WebApp.ViewModels.AutomationStep;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.SalesManagment
{
    public class AutomationStepController : Controller
    {
        private readonly IAutomationStepService _service;
        private readonly IMapper _mapper;

        public AutomationStepController(IAutomationStepService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var dtos = await _service.GetAllAsync();
            var vms = _mapper.Map<List<AutomationStepViewModel>>(dtos);
            return View(vms);
        }

        public async Task<IActionResult> Create()
        {
            return View(new AutomationStepViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(AutomationStepViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var dto = _mapper.Map<CreateAutomationStepDto>(vm);
            await _service.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            var vm = _mapper.Map<AutomationStepViewModel>(dto);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AutomationStepViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var dto = _mapper.Map<UpdateAutomationStepDto>(vm);
            await _service.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            var vm = _mapper.Map<AutomationStepViewModel>(dto);
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            var vm = _mapper.Map<AutomationStepViewModel>(dto);
            return View(vm);
        }
    }

}
