using CRM.Domain.Base;

namespace CRM.Domain.Entities
{
    public class Note : BaseEntity
    {

        public string Content { get; set; }

        public int CompanyId { get; set; }

        public int UserId { get; set; }

        public int IsDeleted { get; set; }
    }
}
