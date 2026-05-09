using CRM.Domain.Base;

namespace CRM.Domain.Entities
{
    public class Account : BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string phone { get; set; }
        public string WebSite { get; set; }
        public string AccountOwner { get; set; }
    }
}
