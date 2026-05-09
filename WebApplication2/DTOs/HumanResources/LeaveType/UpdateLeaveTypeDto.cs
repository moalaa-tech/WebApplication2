using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.HumanResources.LeaveType
{
    public class UpdateLeaveTypeDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string NameAr { get; set; }
    }
}
