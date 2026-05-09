using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.HumanResources.LeaveRequest
{
    public class CreateLeaveRequestDto
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int LeaveTypeId { get; set; }


        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string Reason { get; set; }
    }
}
