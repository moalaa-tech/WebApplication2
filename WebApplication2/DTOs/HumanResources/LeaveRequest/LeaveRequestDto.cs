using CRM.WebApp.DTOs.HumanResources.Employee;
using CRM.WebApp.DTOs.HumanResources.LeaveType;

namespace CRM.WebApp.DTOs.HumanResources.LeaveRequest
{
    public class LeaveRequestDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeDto Employee { get; set; }

        public int LeaveTypeId { get; set; }
        public LeaveTypeDto LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfDays { get; set; }
        public string? Reason { get; set; }
        public string? Status { get; set; }
        public DateTime RequestedDate { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
}
