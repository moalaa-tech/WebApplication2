using CRM.Domain.Base;

namespace CRM.Domain.Entities
{
    public class Currency : BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Code { get; set; }

        public ICollection<Company> Companies { get; set; }

    }
}
