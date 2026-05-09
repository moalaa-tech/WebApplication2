
namespace CRM.WebApi.DbContext
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }

        public string? Description { get; set; }
        public string? Email { get; set; }

        public string? Address { get; set; }
        public string Phone { get; set; }

        public CreditTerm CreditTerm { get; set; }

    }
}
