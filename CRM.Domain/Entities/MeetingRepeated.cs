using CRM.Domain.Base;

namespace CRM.Domain.Entities
{
    public class MeetingRepeated : BaseEntity
    {
        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
        public bool AllDay { get; set; }
        public string RepeatedType { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
