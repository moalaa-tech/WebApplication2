using CRM.WebApp.DTOs.HumanResources.JobApplication;
using CRM.WebApp.Services.HumanResources;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.HumanResources
{
    public class JobApplicationsController : Controller
    {
        private readonly IJobApplicationService _applicationService;

        public JobApplicationsController(IJobApplicationService applicationService)
        {
            _applicationService = applicationService;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobApplicationDto>>> Index()
        {
            var applications = await _applicationService.GetAllApplicationsAsync();
            return View(applications);
        }



        [HttpGet]
        public ActionResult SubmitApplication()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult<JobApplicationDto>> SubmitApplication([FromForm] CreateJobApplicationDto dto, [FromForm] IFormFile resume, [FromForm] IFormFile coverLetter)
        {
            var application = await _applicationService.SubmitApplicationAsync(dto, resume, coverLetter);
            return RedirectToAction(nameof(GetByJob), new { jobPostingId = application.JobPostingId });
        }

        [HttpGet("job/{jobPostingId}")]
        public async Task<ActionResult<IEnumerable<JobApplicationDto>>> GetByJob(int jobPostingId)
        {
            var applications = await _applicationService.GetApplicationsForJobAsync(jobPostingId);
            return View(applications);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateJobApplicationDto dto)
        {
            await _applicationService.UpdateApplicationStatusAsync(id, dto);
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var application = await _applicationService.GetApplicationByIdAsync(id);
            if (application == null)
                return NotFound();
            return View(application);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadResume(int id)
        {
            // This action will be implemented in the view to trigger file download via a dedicated endpoint if needed
            var application = await _applicationService.GetApplicationByIdAsync(id);
            if (application == null || string.IsNullOrWhiteSpace(application.ResumePath))
                return NotFound();

            // Let file be handled by a FilesController or direct static file, so redirect to the path
            return Redirect("~/" + application.ResumePath.Replace("\\", "/"));
        }

        [HttpGet]
        public async Task<IActionResult> DownloadCoverLetter(int id)
        {
            var application = await _applicationService.GetApplicationByIdAsync(id);
            if (application == null || string.IsNullOrWhiteSpace(application.CoverLetterPath))
                return NotFound();

            return Redirect("~/" + application.CoverLetterPath.Replace("\\", "/"));
        }

        [HttpGet]
        public async Task<IActionResult> ScheduleInterview(int id)
        {
            var application = await _applicationService.GetApplicationByIdAsync(id);
            if (application == null)
                return NotFound();
            var model = new UpdateJobApplicationDto
            {
                Id = application.Id,
                FirstName = application.FirstName,
                LastName = application.LastName,
                Email = application.Email,
                Phone = application.Phone,
                JobPostingId = application.JobPostingId,
                JobTitle = application.JobTitle,
                Status = application.Status,
                ApplicationDate = application.ApplicationDate,
                InterviewDate = application.InterviewDate ?? DateTime.Today,
                Notes = application.Notes
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ScheduleInterview(int id, UpdateJobApplicationDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(dto);

            dto.Status = Domain.Enums.HumanResources.ApplicationStatus.Interview;
            await _applicationService.UpdateApplicationStatusAsync(id, dto);
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
