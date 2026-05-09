using CRM.Domain.Base;

namespace CRM.Domain.Entities
{
    public class Country : BaseEntity
    {
        public string? Name { get; set; }
        public string? NameAr { get; set; }
        public string? ISO2 { get; set; }
        public string? ISO3 { get; set; }
        public ICollection<State> States { get; set; }
        public ICollection<Company> Companies { get; set; }

    }
}
