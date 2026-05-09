using CRM.Domain.IdentityEntity;

namespace CRM.Domain.Base
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime? DateModified { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool IsActive { get; set; } = true;
        //public int CreatedById { get; set; }
        //public  ApplicationUser CreatedBy { get; set; }


        //public int ModifiedById { get; set; }
        //public  ApplicationUser ModifiedBy { get; set; }

    }
}
