using CRM.WebApp.DTOs.Project;

namespace CRM.WebApp.Services.ProjectManagment
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto> GetProjectByIdAsync(int id);
        Task CreateProjectAsync(CreateProjectDto project);
        Task UpdateProjectAsync(UpdateProjectDto project);
        Task DeleteProjectAsync(int id);
        Task<ProjectCostSummaryDto> GetProjectCostSummaryAsync(int projectId);
        Task<IEnumerable<TimeEntryDto>> GetTimeEntriesForProjectAsync(int projectId, DateTime? fromDate, DateTime? toDate);
        Task<IEnumerable<PhaseExpenseDto>> GetExpensesForProjectAsync(int projectId, DateTime? fromDate, DateTime? toDate);
    }
}
