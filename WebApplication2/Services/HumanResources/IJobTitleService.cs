using CRM.WebApp.DTOs.HumanResources.JobTitle;
using CRM.WebApp.ViewModels.JobTitle;

namespace CRM.WebApp.Services.HumanResources
{
    public interface IJobTitleService
    {
        Task<List<JobTitleDto>> GetAllAsync();
        Task<JobTitleDto> GetByIdAsync(int id);
        Task CreateAsync(CreateJobTitleDto model);
        Task UpdateAsync(UpdateJobTitleDto model);
        Task DeleteAsync(int id);
        Task<JobTitleViewModel> GetCreateModelAsync();

    }
}
