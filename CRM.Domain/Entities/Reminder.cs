using CRM.Domain.Base;

namespace CRM.Domain.Entities
{
    public class Reminder : BaseEntity
    {
        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }

        public bool IsBefore { get; set; }
        public bool IsAfter { get; set; }

        public int Minute { get; set; }
        public int Hour { get; set; }

        public int Day { get; set; }

    }
}
