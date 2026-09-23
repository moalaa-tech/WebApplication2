using AutoMapper;
using CRM.WebApp.DTOs.Banking;
using CRM.WebApp.Services.Finance_Accounting;
using CRM.WebApp.ViewModels.Banking;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.Banking
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankAccountsController : ControllerBase
    {
        private readonly IBankAccountService _service;
        private readonly IMapper _mapper;

        public BankAccountsController(IBankAccountService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var accounts = await _service.GetAllAsync();
            return Ok(_mapper.Map<List<BankAccountViewModel>>(accounts));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return Ok(_mapper.Map<BankAccountViewModel>(dto));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBankAccountViewModel vm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var dto = _mapper.Map<CreateBankAccountDto>(vm);
            await _service.CreateAsync(dto);
            return Ok(vm);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateBankAccountDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _service.UpdateAsync(dto);
            return Ok(dto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}