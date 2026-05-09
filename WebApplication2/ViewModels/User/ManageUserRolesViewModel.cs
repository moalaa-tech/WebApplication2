using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRM.WebApp.Validation;

namespace CRM.WebApp.ViewModels.User
{
    public class ManageUserRolesViewModel
    {
        [Required]
        [BusinessStringLength(450)] // Identity user ID max length
        public required string UserId { get; set; }
        
        [Required]
        [BusinessStringLength(256)] // Identity user name max length
        public required string UserName { get; set; }
        
        public List<SelectRoleViewModel> Roles { get; set; } = new();
    }

    public class SelectRoleViewModel
    {
        [Required]
        [BusinessStringLength(256)] // Identity role name max length
        public required string RoleName { get; set; }
        
        public bool Selected { get; set; }
    }
}
