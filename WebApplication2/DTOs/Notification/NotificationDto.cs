namespace CRM.WebApp.DTOs.Notification
{
    public class NotificationDto
    {
        public int Id { get; set; }

        public DateTime? DateModified { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool IsActive { get; set; } = true;
        public string UserId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
