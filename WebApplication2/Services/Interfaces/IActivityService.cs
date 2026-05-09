using CRM.Domain.Enums;
using CRM.WebApp.DTOs.SalesPipeline;
using CRM.WebApp.Paging;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.Services.Interfaces
{
    public interface IActivityService
    {
        Task<ActivityDto> GetActivityByIdAsync(int id);
        Task<IEnumerable<ActivityDto>> GetAllActivitiesAsync();
        Task<PaginatedList<ActivityDto>> GetActivitiesPaginatedAsync(int pageNumber, int pageSize, ActivityFilterDto filter);
        Task<IEnumerable<ActivityDto>> GetUpcomingActivitiesAsync(int daysAhead);
        Task<IEnumerable<ActivityDto>> GetActivitiesByEntityAsync(ActivityType entityType, int entityId);
        Task<int> CreateActivityAsync(CreateActivityDto model);
        Task UpdateActivityAsync(UpdateActivityDto model);
        Task DeleteActivityAsync(int id);
        Task CompleteActivityAsync(int id, string outcomeNotes);
        Task<ActivityDto> GetActivityViewModelForEditAsync(int id);
        Task<ActivityDto> GetActivityViewModelForCreateAsync(ActivityType? entityType = null, int? entityId = null);
        Task<ActivityDto> GetActivityStatsAsync(DateTime? startDate = null, DateTime? endDate = null);
        Task<SelectList> GetUsersSelectListAsync();
    }
}
