using AutoMapper;
using CRM.WebApp.DTOs.AutomationStep;
using CRM.WebApp.Services.MarketingAutomation;
using CRM.WebApp.ViewModels.AutomationStep;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.SalesManagment
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutomationStepController : ControllerBase
    {
        private readonly IAutomationStepService _service;
        private readonly IMapper _mapper;

        public AutomationStepController(IAutomationStepService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dtos = await _service.GetAllAsync();
            var vms = _mapper.Map<List<AutomationStepViewModel>>(dtos);
            return Ok(vms);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AutomationStepViewModel vm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var dto = _mapper.Map<CreateAutomationStepDto>(vm);
            await _service.CreateAsync(dto);
            return Ok(vm);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] AutomationStepViewModel vm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var dto = _mapper.Map<UpdateAutomationStepDto>(vm);
            await _service.UpdateAsync(dto);
            return Ok(vm);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            var vm = _mapper.Map<AutomationStepViewModel>(dto);
            return Ok(vm);
        }
    }

}