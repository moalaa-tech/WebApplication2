using CRM.Domain.Base;

namespace CRM.Domain.Entities
{
    public class State : BaseEntity
    {
        public required string Name { get; set; }
        public required string NameAr { get; set; }
        public int CountryId { get; set; }
        public virtual Country? Country { get; set; }



        public ICollection<City>? Cities { get; set; }
    }
}
