using CRM.Domain.Enums;
using CRM.WebApp.DTOs.Project;

namespace CRM.WebApp.Services.ProjectManagment
{
    public interface ITimeEntryService
    {
        Task<TimeEntryDto> GetTimeEntryByIdAsync(int id);
        Task<IEnumerable<TimeEntryDto>> GetTimeEntriesByPhaseAsync(int phaseId);
        Task<IEnumerable<TimeEntryDto>> GetTimeEntriesByUserAsync(string userId, DateTime? fromDate, DateTime? toDate);
        Task<TimeEntryDto> CreateTimeEntryAsync(CreateTimeEntryDto entryDto);
        Task UpdateTimeEntryAsync(UpdateTimeEntryDto entryDto);
        Task DeleteTimeEntryAsync(int id);
        Task UpdateTimeEntryStatusAsync(int entryId, TimeEntryStatus status);
        Task<decimal> GetTotalHoursByPhaseAsync(int phaseId, DateTime? fromDate, DateTime? toDate);
    }
}
