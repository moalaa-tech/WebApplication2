using CRM.WebApp.DTOs.HumanResources.JobPosting;
using CRM.WebApp.Services.HumanResources;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.HumanResources
{
    public class JobPostingsController : Controller
    {
        private readonly IJobPostingService _jobPostingService;

        public JobPostingsController(IJobPostingService jobPostingService)
        {
            _jobPostingService = jobPostingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobPostingDto>>> Index([FromQuery] bool activeOnly = true)
        {
            var postings = await _jobPostingService.GetAllJobPostingsAsync(activeOnly);
            return View(postings);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<JobPostingDto>> Create([FromBody] CreateJobPostingDto dto)
        {
            var posting = await _jobPostingService.CreateJobPostingAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPatch("{id}/close")]
        public async Task<IActionResult> Close(int id)
        {
            await _jobPostingService.CloseJobPostingAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // --- Remaining Actions ---

        // Details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var posting = await _jobPostingService.GetJobPostingByIdAsync(id);
            if (posting == null)
                return NotFound();
            return View(posting);
        }

        // Edit (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var posting = await _jobPostingService.GetJobPostingByIdAsync(id);
            if (posting == null)
                return NotFound();
            // Map JobPostingDto to CreateJobPostingDto or EditJobPostingDto as needed
            var dto = new CreateJobPostingDto
            {
                Title = posting.Title,
                Description = posting.Description,
                Requirements = posting.Requirements,
                PostingDate = posting.PostingDate,
                ClosingDate = posting.ClosingDate,
                IsActive = posting.IsActive,
                DepartmentId = posting.DepartmentId,
                DepartmentName = posting.DepartmentName,
                ApplicationCount = posting.ApplicationCount
            };
            return View(dto);
        }

        // Edit (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateJobPostingDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _jobPostingService.UpdateJobPostingAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

        // Delete (GET)
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var posting = await _jobPostingService.GetJobPostingByIdAsync(id);
            if (posting == null)
                return NotFound();
            return View(posting);
        }

        // Delete (POST)
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _jobPostingService.DeleteJobPostingAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
