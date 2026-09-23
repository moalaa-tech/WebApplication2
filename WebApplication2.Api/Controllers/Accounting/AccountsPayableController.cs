using AutoMapper;
using CRM.WebApp.DTOs.AccountsPayable;
using CRM.WebApp.Services.Finance_Accounting;
using CRM.WebApp.ViewModels.AccountsPayable;
using Microsoft.AspNetCore.Mvc;
using InvoiceViewModel = CRM.WebApp.ViewModels.AccountsPayable.InvoiceViewModel;

namespace WebApplication2.Api.Controllers.Accounting
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsPayableController : ControllerBase
    {
        private readonly IAccountsPayableService _service;
        private readonly IMapper _mapper;

        public AccountsPayableController(IAccountsPayableService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dtos = await _service.GetAllAsync();
            var viewModels = _mapper.Map<IEnumerable<InvoiceViewModel>>(dtos);
            return Ok(viewModels);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();
            var viewModel = _mapper.Map<InvoiceViewModel>(dto);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvoiceViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var dto = _mapper.Map<InvoiceDto>(viewModel);
            await _service.CreateAsync(dto);
            return Ok(viewModel);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] InvoiceViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var dto = _mapper.Map<InvoiceDto>(viewModel);
            await _service.UpdateAsync(dto);
            return Ok(viewModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}