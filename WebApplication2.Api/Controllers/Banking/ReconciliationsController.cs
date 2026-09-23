using AutoMapper;
using CRM.WebApp.DTOs.Banking;
using CRM.WebApp.Services.Finance_Accounting;
using CRM.WebApp.ViewModels.Banking;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.Banking
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReconciliationsController : ControllerBase
    {
        private readonly IReconciliationService _service;
        private readonly IMapper _mapper;

        public ReconciliationsController(IReconciliationService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReconciliationViewModel vm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var dto = _mapper.Map<CreateReconciliationDto>(vm);
            await _service.CreateAsync(dto);
            return Ok(vm);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var rec = await _service.GetByIdAsync(id);
            var items = await _service.GetItemsAsync(id);
            return Ok(new { reconciliation = rec, items });
        }
    }
}