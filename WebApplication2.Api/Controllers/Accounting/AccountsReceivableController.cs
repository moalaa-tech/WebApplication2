using AutoMapper;
using CRM.WebApp.DTOs.Accounting;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.ViewModels.Accounting;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.Accounting
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsReceivableController : ControllerBase
    {
        private readonly IAccountsReceivableService _receivableService;
        private readonly IMapper _mapper;

        public AccountsReceivableController(IAccountsReceivableService receivableService, IMapper mapper)
        {
            _receivableService = receivableService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var receivables = await _receivableService.GetAllAsync();
            var viewModels = _mapper.Map<IEnumerable<AccountsReceivableViewModel>>(receivables);
            return Ok(viewModels);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var receivable = await _receivableService.GetByIdAsync(id);
            return receivable == null ? NotFound() : Ok(_mapper.Map<AccountsReceivableViewModel>(receivable));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountsReceivableViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _receivableService.CreateAsync(_mapper.Map<CreateAccountsReceivableDto>(viewModel));
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error creating receivable: {ex.Message}" });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] AccountsReceivableViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _receivableService.UpdateAsync(_mapper.Map<AccountsReceivableDto>(viewModel));
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error updating receivable: {ex.Message}" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _receivableService.DeleteAsync(id);
            return NoContent();
        }
    }
}