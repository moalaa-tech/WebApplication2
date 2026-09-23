using CRM.WebApp.DTOs.HumanResources.JobPosting;
using CRM.WebApp.Services.HumanResources;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.HumanResources
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobPostingsController : ControllerBase
    {
        private readonly IJobPostingService _jobPostingService;

        public JobPostingsController(IJobPostingService jobPostingService)
        {
            _jobPostingService = jobPostingService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] bool activeOnly = true)
        {
            var postings = await _jobPostingService.GetAllJobPostingsAsync(activeOnly);
            return Ok(postings);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobPostingDto dto)
        {
            var posting = await _jobPostingService.CreateJobPostingAsync(dto);
            return Ok();
        }

        [HttpPatch("{id}/close")]
        public async Task<IActionResult> Close(int id)
        {
            await _jobPostingService.CloseJobPostingAsync(id);
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var posting = await _jobPostingService.GetJobPostingByIdAsync(id);
            if (posting == null)
                return NotFound();
            return Ok(posting);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CreateJobPostingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _jobPostingService.UpdateJobPostingAsync(id, dto);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _jobPostingService.DeleteJobPostingAsync(id);
            return Ok();
        }
    }
}
