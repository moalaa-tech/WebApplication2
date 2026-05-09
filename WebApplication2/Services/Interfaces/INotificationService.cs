using CRM.WebApp.DTOs.Notification;

namespace CRM.WebApp.Services.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDto>> GetAllNotificationsAsync();
        Task<NotificationDto> GetNotificationByIdAsync(int id);
        Task<bool> CreateNotificationAsync(CreateNotificationDto dto);
        Task<bool> UpdateNotificationAsync(UpdateNotificationDto dto);
        Task<bool> DeleteNotificationAsync(int id);
        Task<IEnumerable<NotificationDto>> GetNotificationsByUserIdAsync(string userId);
        Task<bool> MarkAsReadAsync(int id);
    }
}
