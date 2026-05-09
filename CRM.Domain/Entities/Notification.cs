using CRM.Domain.Base;

namespace CRM.Domain.Entities
{
    public class Notification : BaseEntity
    {

        public string UserId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
