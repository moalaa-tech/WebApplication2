using CRM.Domain.Base;

namespace CRM.Domain.Entities.HR
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Description { get; set; }

        public DateTime DateOfJoining { get; set; }
        public string Gender { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public DateTime? TerminationDate { get; set; }
        public ICollection<Payroll> Payrolls { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<LeaveRequest> LeaveRequests { get; set; }

    }
}
