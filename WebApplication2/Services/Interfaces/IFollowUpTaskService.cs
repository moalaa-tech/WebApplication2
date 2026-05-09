using CRM.WebApp.DTOs.FollowUpTask;

namespace CRM.WebApp.Services.Interfaces
{
    public interface IFollowUpTaskService
    {
        Task<IEnumerable<FollowUpTaskDto>> GetAllTasksAsync();
        Task<FollowUpTaskDto> GetTaskByIdAsync(int id);
        Task<bool> CreateTaskAsync(CreateFollowUpTaskDto taskDto);
        Task<bool> UpdateTaskAsync(UpdateFollowUpTaskDto taskDto);
        Task<bool> DeleteTaskAsync(int id);
        Task<FollowUpTaskDto> MarkComplete(int id);
    }
}
