using CRM.WebApp.DTOs.LoggingDto;
using CRM.WebApp.Paging;

namespace CRM.WebApp.Services.Interfaces
{
    public interface ILogService
    {
        Task<PaginatedList<LogDto>> GetPaginatedLogsAsync(LogFilterDto filter);
        Task<LogDto> GetLogByIdAsync(int id);
        Task<bool> CreateLogAsync(CreateLogDto dto);
        Task ClearLogsAsync(int daysToKeep);
        Task<LogSummaryDto> GetLogSummaryAsync();
    }
}
