using CRM.Domain.Base;

namespace CRM.Domain.Entities
{
    public class City : BaseEntity
    {
        public required string Name { get; set; }
        public required string NameAr { get; set; }
        public int StateId { get; set; }
        public virtual State? State { get; set; }
    }
}
