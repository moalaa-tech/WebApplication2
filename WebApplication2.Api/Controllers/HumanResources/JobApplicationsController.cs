using CRM.WebApp.DTOs.HumanResources.JobApplication;
using CRM.WebApp.Services.HumanResources;
using CRM.Domain.Enums.HumanResources;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.HumanResources
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobApplicationService _applicationService;

        public JobApplicationsController(IJobApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var applications = await _applicationService.GetAllApplicationsAsync();
            return Ok(applications);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SubmitApplication([FromForm] CreateJobApplicationDto dto)
        {
            var resume = Request.Form.Files["resume"];
            var coverLetter = Request.Form.Files["coverLetter"];
            var application = await _applicationService.SubmitApplicationAsync(dto, resume, coverLetter);
            return Ok();
        }

        [HttpGet("job/{jobPostingId}")]
        public async Task<IActionResult> GetByJob(int jobPostingId)
        {
            var applications = await _applicationService.GetApplicationsForJobAsync(jobPostingId);
            return Ok(applications);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateJobApplicationDto dto)
        {
            await _applicationService.UpdateApplicationStatusAsync(id, dto);
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var application = await _applicationService.GetApplicationByIdAsync(id);
            if (application == null)
                return NotFound();
            return Ok(application);
        }

        [HttpGet("DownloadResume/{id:int}")]
        public async Task<IActionResult> DownloadResume(int id)
        {
            // This action will be implemented in the view to trigger file download via a dedicated endpoint if needed
            var application = await _applicationService.GetApplicationByIdAsync(id);
            if (application == null || string.IsNullOrWhiteSpace(application.ResumePath))
                return NotFound();

            // Let file be handled by a FilesController or direct static file, so redirect to the path
            return Redirect("~/" + application.ResumePath.Replace("\\", "/"));
        }

        [HttpGet("DownloadCoverLetter/{id:int}")]
        public async Task<IActionResult> DownloadCoverLetter(int id)
        {
            var application = await _applicationService.GetApplicationByIdAsync(id);
            if (application == null || string.IsNullOrWhiteSpace(application.CoverLetterPath))
                return NotFound();

            return Redirect("~/" + application.CoverLetterPath.Replace("\\", "/"));
        }

        [HttpGet("ScheduleInterview/{id:int}")]
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
            return Ok(model);
        }

        [HttpPost("ScheduleInterview/{id:int}")]
        public async Task<IActionResult> ScheduleInterview(int id, [FromBody] UpdateJobApplicationDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            dto.Status = ApplicationStatus.Interview;
            await _applicationService.UpdateApplicationStatusAsync(id, dto);
            return Ok();
        }
    }
}
