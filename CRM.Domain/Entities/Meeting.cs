using CRM.Domain.Base;

namespace CRM.Domain.Entities
{
    public class Meeting : BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Location { get; set; }

        public string? Description { get; set; }

        public bool AllDay { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Host { get; set; }

        public bool IsHasParticipants { get; set; }
        public ICollection<Participant>? Participants { get; set; }

        public Reminder Reminder { get; set; }
        public int RelatedTo { get; set; }
        public bool IsRepeated { get; set; }
        public MeetingRepeated? MeetingRepeated { get; set; }
    }
}
