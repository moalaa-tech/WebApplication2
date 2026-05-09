using CRM.WebApp.DTOs.HumanResources.JobApplication;

namespace CRM.WebApp.Services.HumanResources
{
    public interface IJobApplicationService
    {
        Task<JobApplicationDto> SubmitApplicationAsync(CreateJobApplicationDto dto, IFormFile resume, IFormFile coverLetter);
        Task UpdateApplicationStatusAsync(int id, UpdateJobApplicationDto dto);
        Task<IEnumerable<JobApplicationDto>> GetApplicationsForJobAsync(int jobPostingId);
        Task<IEnumerable<JobApplicationDto>> GetAllApplicationsAsync();
        Task<JobApplicationDto?> GetApplicationByIdAsync(int id);
    }
}
