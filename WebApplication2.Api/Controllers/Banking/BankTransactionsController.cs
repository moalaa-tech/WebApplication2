using AutoMapper;
using CRM.Domain.Enums;
using CRM.WebApp.DTOs.Banking;
using CRM.WebApp.Services.Finance_Accounting;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.Banking
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankTransactionsController : ControllerBase
    {
        private readonly IBankTransactionService _service;
        private readonly IMapper _mapper;

        public BankTransactionsController(IBankTransactionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var transactions = await _service.GetAllAsync();
            return Ok(transactions);
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> Filter([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] TransactionType? type)
        {
            var all = await _service.GetAllAsync();

            var filtered = all.Where(t =>
                (!from.HasValue || t.TransactionDate >= from.Value) &&
                (!to.HasValue || t.TransactionDate <= to.Value) &&
                (!type.HasValue || t.Type == type.Value)
            );

            return Ok(filtered);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBankTransactionDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _service.CreateAsync(dto);
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