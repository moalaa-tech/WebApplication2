using CRM.Domain.Base;
using CRM.Domain.IdentityEntity;

namespace CRM.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public int BusinessId { get; set; }
        public Business Business { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime CreationDate { get; set; }
        public int IsDeleted { get; set; }       
    }
}
