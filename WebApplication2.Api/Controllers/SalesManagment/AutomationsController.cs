using AutoMapper;
using CRM.Domain.Enums.MarketingAutomation;
using CRM.WebApp.DTOs.Automation;
using CRM.WebApp.DTOs.AutomationStep;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.Services.MarketingAutomation;
using CRM.WebApp.ViewModels.Automation;
using Microsoft.AspNetCore.Mvc;
using static CRM.WebApp.ViewModels.Automation.RunAutomationViewModel;

namespace WebApplication2.Api.Controllers.SalesManagment
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AutomationsController : ControllerBase
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
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AutomationViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var automation = _mapper.Map<CreateAutomationDto>(model.Automation);
            await Service.AddAsync(automation);

            // Add steps
            foreach (var stepModel in model.Steps)
            {
                var step = _mapper.Map<CreateAutomationStepDto>(stepModel);
                step.AutomationId = model.Automation.Id;
                await AutomationStepService.CreateAsync(step);
            }

            return Ok(model);
        }

        [HttpGet("Run")]
        public async Task<IActionResult> Run([FromQuery] int id)
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

            return Ok(model);
        }

        [HttpPost("Run")]
        public async Task<IActionResult> Run([FromBody] RunAutomationViewModel model)
        {
            foreach (var contactId in model.SelectedContactIds)
            {
                await _automationService.RunAutomationForContact(model.AutomationId, contactId);
            }

            return Ok();
        }

        [HttpPost("ToggleStatus")]
        public async Task<IActionResult> ToggleStatus([FromQuery] int id)
        {
            var automation = await Service.GetByIdAsync(id);
            if (automation != null)
            {
                automation.IsActive = !automation.IsActive;
                await Service.SaveChangesAsync();
            }

            return Ok();
        }
    }
}