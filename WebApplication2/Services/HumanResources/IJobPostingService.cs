using CRM.WebApp.DTOs.HumanResources.JobPosting;

namespace CRM.WebApp.Services.HumanResources
{
    public interface IJobPostingService
    {
        Task<IEnumerable<JobPostingDto>> GetAllJobPostingsAsync(bool activeOnly = true);
        Task<JobPostingDto> CreateJobPostingAsync(CreateJobPostingDto dto);
        Task<JobPostingDto> GetJobPostingByIdAsync(int id);
        Task UpdateJobPostingAsync(int id, CreateJobPostingDto dto);
        Task CloseJobPostingAsync(int id);
        Task DeleteJobPostingAsync(int id);
    }
}
