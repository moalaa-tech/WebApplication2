using AutoMapper;
using CRM.WebApp.DTOs.EmailTemplate;
using CRM.WebApp.Services.MarketingAutomation;
using CRM.WebApp.ViewModels.EmailTemplate;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.MarketingAutomation
{
    public class EmailTemplateController : Controller
    {
        private readonly IEmailTemplateService _service;
        private readonly IMapper _mapper;

        public EmailTemplateController(IEmailTemplateService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dtos = await _service.GetAllAsync();
            var vmList = _mapper.Map<List<EmailTemplateViewModel>>(dtos);
            return View(vmList);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            var vm = _mapper.Map<EmailTemplateViewModel>(dto);
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmailTemplateViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var dto = _mapper.Map<CreateEmailTemplateDto>(vm);
            await _service.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            var vm = _mapper.Map<EmailTemplateViewModel>(dto);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmailTemplateViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var dto = _mapper.Map<UpdateEmailTemplateDto>(vm);
            await _service.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            var vm = _mapper.Map<EmailTemplateViewModel>(dto);
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
