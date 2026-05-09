using CRM.Domain.Enums;
using CRM.WebApp.DTOs.Project;

namespace CRM.WebApp.Services.ProjectManagment
{
    public interface IJobPhaseService
    {
        Task<JobPhaseDto> GetPhaseByIdAsync(int id);
        Task<IEnumerable<JobPhaseDto>> GetPhasesByProjectAsync(int projectId);
        Task<JobPhaseDto> CreatePhaseAsync(CreateJobPhaseDto phaseDto);
        Task UpdatePhaseAsync(UpdateJobPhaseDto phaseDto);
        Task DeletePhaseAsync(int id);
        Task<PhaseCostSummaryDto> GetPhaseCostSummaryAsync(int phaseId);
        Task UpdatePhaseStatusAsync(int phaseId, PhaseStatus status);
        Task<IEnumerable<TimeEntryDto>> GetTimeEntriesForPhaseAsync(int phaseId, DateTime? fromDate, DateTime? toDate);
        Task<IEnumerable<PhaseExpenseDto>> GetExpensesForPhaseAsync(int phaseId, DateTime? fromDate, DateTime? toDate);
    }
}
