using AutoMapper;
using CRM.Domain.Enums.MarketingAutomation;
using CRM.WebApp.DTOs.Automation;
using CRM.WebApp.DTOs.AutomationStep;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.Services.MarketingAutomation;
using CRM.WebApp.ViewModels.Automation;
using Microsoft.AspNetCore.Mvc;
using static CRM.WebApp.ViewModels.Automation.RunAutomationViewModel;

namespace CRM.WebApp.Controllers.SalesManagment
{
    //[Authorize]
    public class AutomationsController : Controller
    {
        private readonly IMarketingAutomationService _automationService;
        private readonly IMapper _mapper;
        private readonly IAutomationService Service;
        private readonly IEmailTemplateService EmailTemplateService;
        private readonly IAutomationStepService AutomationStepService;
        private readonly IContactService ContactService;

        public AutomationsController(
            IMarketingAutomationService automationService,
            IMapper mapper,
            IAutomationService _service,
            IAutomationStepService automationStepService,
            IEmailTemplateService emailTemplateService,
            IContactService contactService
            )
        {
            _automationService = automationService;
            _mapper = mapper;
            Service = _service;
            EmailTemplateService = emailTemplateService;
            AutomationStepService = automationStepService;
            ContactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await Service.GetAllAsync();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new AutomationViewModel
            {
                AvailableTriggers = Enum.GetValues<AutomationTrigger>().ToList(),
                AvailableActions = Enum.GetValues<AutomationAction>().ToList(),
                EmailTemplates = await EmailTemplateService.GetAllAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AutomationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var automation = _mapper.Map<CreateAutomationDto>(model.Automation);
                await Service.AddAsync(automation);

                // Add steps
                foreach (var stepModel in model.Steps)
                {
                    var step = _mapper.Map<CreateAutomationStepDto>(stepModel);
                    step.AutomationId = model.Automation.Id;
                    await AutomationStepService.CreateAsync(step);
                }

                return RedirectToAction(nameof(Index));
            }

            // If we got this far, something failed, redisplay form
            model.AvailableTriggers = Enum.GetValues<AutomationTrigger>().ToList();
            model.AvailableActions = Enum.GetValues<AutomationAction>().ToList();
            model.EmailTemplates = await EmailTemplateService.GetAllAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Run(int id)
        {
            var automation = await Service.GetByIdAsync(id);
            if (automation == null)
            {
                return NotFound();
            }

            var contacts = ContactService.GetAllAsync().Result.Select(a => new ContactSelectionViewModel
            {
                Email = a.Email,
                Name = a.Name,

            }).ToList();

            var model = new RunAutomationViewModel
            {
                AutomationId = id,
                AutomationName = automation.Name,
                Contacts = contacts
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Run(RunAutomationViewModel model)
        {
            foreach (var contactId in model.SelectedContactIds)
            {
                await _automationService.RunAutomationForContact(model.AutomationId, contactId);
            }

            TempData["Message"] = "Automation started for selected contacts";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var automation = await Service.GetByIdAsync(id);
            if (automation != null)
            {
                automation.IsActive = !automation.IsActive;
                await Service.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
