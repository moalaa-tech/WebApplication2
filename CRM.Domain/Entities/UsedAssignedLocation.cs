using CRM.Domain.Base;
using CRM.Domain.Entities.HR;

namespace CRM.Domain.Entities
{
    public class UsedAssignedLocation : BaseEntity
    {
        public int EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }

        public int CountryId { get; set; }
        public virtual Country? Country { get; set; }

        public int StatesId { get; set; }
        public virtual State? State { get; set; }

        public int CityId { get; set; }
        public virtual City? City { get; set; }
    }
}
