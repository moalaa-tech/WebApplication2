using CRM.Domain.Enums.SalesManagement;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.SalesManagement
{
    public class LeadViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public required string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public required string LastName { get; set; }

        [Display(Name = "Company")]
        public required string Company { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        [Phone]
        public required string Phone { get; set; }

        [Display(Name = "Source")]
        public required string Source { get; set; }

        [Display(Name = "Status")]
        public LeadStatus Status { get; set; }

        [Display(Name = "Notes")]
        public required string Notes { get; set; }

        [Display(Name = "Assign To")]
        public required string AssignedToUserId { get; set; }

        public required SelectList UsersSelectList { get; set; }
        public required SelectList StatusSelectList { get; set; }
        public required SelectList SourceSelectList { get; set; }
    }
}
