using AutoMapper;
using CRM.WebApp.DTOs.Accounting;
using CRM.WebApp.Services.Finance_Accounting;
using CRM.WebApp.ViewModels.Accounting;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.Accounting
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneralLedgerController : ControllerBase
    {
        private readonly IGeneralLedgerService _ledgerService;
        private readonly IMapper _mapper;

        public GeneralLedgerController(IGeneralLedgerService ledgerService, IMapper mapper)
        {
            _ledgerService = ledgerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var ledgers = await _ledgerService.GetAllAsync();
            var viewModels = _mapper.Map<IEnumerable<GeneralLedgerViewModel>>(ledgers);
            return Ok(viewModels);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGeneralLedgerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var dto = _mapper.Map<CreateGeneralLedgerDto>(viewModel);
                await _ledgerService.CreateAsync(dto);
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error creating ledger: {ex.Message}" });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] GeneralLedgerViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _ledgerService.UpdateAsync(_mapper.Map<GeneralLedgerDTO>(viewModel));
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error updating ledger: {ex.Message}" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _ledgerService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("PostTransaction/{id:int}")]
        public async Task<IActionResult> PostTransaction(int id)
        {
            await _ledgerService.PostTransactionAsync(id);
            return Ok();
        }
    }
}