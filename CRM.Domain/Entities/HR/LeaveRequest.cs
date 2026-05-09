using CRM.Domain.Base;
using CRM.Domain.Enums.HumanResources;

namespace CRM.Domain.Entities.HR
{
    public class LeaveRequest : BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfDays { get; set; }
        public string Reason { get; set; }
        public LeaveStatus Status { get; set; } // Enum: Pending, Approved, Rejected
        public DateTime RequestedDate { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApproverComments { get; set; }



    }
}
