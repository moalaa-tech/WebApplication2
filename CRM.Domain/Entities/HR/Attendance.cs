using CRM.Domain.Base;
using CRM.Domain.Enums.HumanResources;

namespace CRM.Domain.Entities.HR
{
    public class Attendance : BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime ClockInTime { get; set; }
        public DateTime? ClockOutTime { get; set; }
        public AttendanceStatus Status { get; set; } // Enum: Present, Absent, Late, HalfDay

    }
}
