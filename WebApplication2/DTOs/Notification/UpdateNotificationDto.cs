using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.Notification
{
    public class UpdateNotificationDto
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }

        public bool IsRead { get; set; }
        public bool IsActive { get; set; }
    }
}
