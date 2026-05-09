using CRM.Domain.Base;

namespace CRM.Domain.Entities.HR
{
    public class JobTitle : BaseEntity
    {
        public string Title { get; set; }
        public string TitleAr { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
