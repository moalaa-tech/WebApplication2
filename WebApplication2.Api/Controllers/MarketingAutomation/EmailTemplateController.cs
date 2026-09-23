using AutoMapper;
using CRM.WebApp.DTOs.EmailTemplate;
using CRM.WebApp.Services.MarketingAutomation;
using CRM.WebApp.ViewModels.EmailTemplate;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.MarketingAutomation
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailTemplateController : ControllerBase
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
            return Ok(vmList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            var vm = _mapper.Map<EmailTemplateViewModel>(dto);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmailTemplateViewModel vm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var dto = _mapper.Map<CreateEmailTemplateDto>(vm);
            await _service.CreateAsync(dto);
            return Ok(vm);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] EmailTemplateViewModel vm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var dto = _mapper.Map<UpdateEmailTemplateDto>(vm);
            await _service.UpdateAsync(dto);
            return Ok(vm);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }

}
