using AutoMapper;
using CRM.WebApp.DTOs.Accounting;
using CRM.WebApp.Services.Finance_Accounting;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.ViewModels.Accounting;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.Accounting
{
    public class JournalController : Controller
    {
        private readonly IJournalService _journalService;
        private readonly IMapper _mapper;

        public JournalController(IJournalService journalService, IMapper mapper)
        {
            _journalService = journalService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate, bool? postedOnly)
        {
            var entries = await _journalService.GetJournalEntriesAsync(fromDate, toDate, postedOnly);

            var viewModel = new JournalIndexViewModel
            {
                FromDate = fromDate,
                ToDate = toDate,
                PostedOnly = postedOnly,
                Entries = _mapper.Map<List<JournalEntryViewModel>>(entries)
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateJournalEntryDto journalEntryDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _journalService.CreateJournalEntryAsync(journalEntryDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(journalEntryDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var entry = await _journalService.GetByIdAsync(id);
            if (entry == null)
            {
                return NotFound();
            }

            if (entry.IsPosted)
            {
                TempData["ErrorMessage"] = "Posted journal cannot be edited";
                return RedirectToAction(nameof(Index));
            }

            var viewModel = _mapper.Map<CreateJournalEntryDto>(entry);
            return View(viewModel);
        }




        [HttpPost]
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
