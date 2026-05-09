using AutoMapper;
using CRM.Domain.Entities;
using CRM.WebApp.DTOs.Notification;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(IRepository<Notification> notificationRepository, IMapper mapper, ILogger<NotificationService> logger)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<NotificationDto>> GetAllNotificationsAsync()
        {
            try
            {
                _logger.LogInformation("Getting all notifications");

                var notifications = await _notificationRepository.GetAllAsync().ToListAsync();
                return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error getting all notifications");
                throw;
            }

        }

        public async Task<NotificationDto> GetNotificationByIdAsync(int id)
        {
            var notification = await _notificationRepository.GetByIdAsync(id);
            return _mapper.Map<NotificationDto>(notification);
        }

        public async Task<bool> CreateNotificationAsync(CreateNotificationDto dto)
        {
            var notification = _mapper.Map<Notification>(dto);
            await _notificationRepository.AddAsync(notification);
            await _notificationRepository.SaveChangesAsync();

            return true;

        }

        public async Task<bool> UpdateNotificationAsync(UpdateNotificationDto dto)
        {
            var notification = await _notificationRepository.GetByIdAsync(dto.Id);
            if (notification == null) throw new Exception("Notification not found");

            _mapper.Map(dto, notification);
            _notificationRepository.Update(notification);
            await _notificationRepository.SaveChangesAsync();
            return true;

        }

        public async Task<bool> DeleteNotificationAsync(int id)
        {
            var notification = await _notificationRepository.GetByIdAsync(id);
            if (notification == null) throw new Exception("Notification not found");

            _notificationRepository.Delete(notification);
            await _notificationRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<NotificationDto>> GetNotificationsByUserIdAsync(string userId)
        {
            var notifications = await _notificationRepository.GetByCondition(a => a.UserId == userId).ToListAsync();
            return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
        }

        public async Task<bool> MarkAsReadAsync(int id)
        {
            var notification = await _notificationRepository.GetByIdAsync(id);
            if (notification == null) throw new Exception("Notification not found");

            notification.IsRead = true;
            _notificationRepository.Update(notification);
            await _notificationRepository.SaveChangesAsync();

            return true;

        }
    }
}
