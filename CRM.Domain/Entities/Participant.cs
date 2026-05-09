using CRM.Domain.Base;

namespace CRM.Domain.Entities
{
    public class Participant : BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Email { get; set; }
    }
}
