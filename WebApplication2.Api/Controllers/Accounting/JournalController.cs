using AutoMapper;
using CRM.WebApp.DTOs.Accounting;
using CRM.WebApp.Services.Finance_Accounting;
using CRM.WebApp.ViewModels.Accounting;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.Accounting
{
    [ApiController]
    [Route("api/[controller]")]
    public class JournalController : ControllerBase
    {
        private readonly IJournalService _journalService;
        private readonly IMapper _mapper;

        public JournalController(IJournalService journalService, IMapper mapper)
        {
            _journalService = journalService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate, [FromQuery] bool? postedOnly)
        {
            var entries = await _journalService.GetJournalEntriesAsync(fromDate, toDate, postedOnly);

            var viewModel = new JournalIndexViewModel
            {
                FromDate = fromDate,
                ToDate = toDate,
                PostedOnly = postedOnly,
                Entries = _mapper.Map<List<JournalEntryViewModel>>(entries)
            };

            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJournalEntryDto journalEntryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _journalService.CreateJournalEntryAsync(journalEntryDto);
                return Ok(journalEntryDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("Post/{id:int}")]
        public async Task<IActionResult> Post(int id)
        {
            try
            {
                await _journalService.PostJournalEntryAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}