using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.HumanResources.LeaveType
{
    public class CreateLeaveTypeDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string NameAr { get; set; }
    }
}
