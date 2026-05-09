using System.ComponentModel.DataAnnotations;
using CRM.Domain.Enums.CustomerService;

namespace CRM.WebApp.ViewModels.CustomerService
{
    public class TicketViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Subject")]
        public string Subject { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public TicketStatus Status { get; set; } = TicketStatus.Open;

        [Display(Name = "Priority")]
        public TicketPriority Priority { get; set; } = TicketPriority.Medium;

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Display(Name = "Customer Name")]
        public string? CustomerName { get; set; }

        [Display(Name = "Assigned To")]
        public int? AssignedTo { get; set; }

        [Display(Name = "Assigned User")]
        public string? AssignedUserName { get; set; }

        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Last Updated")]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime LastUpdated { get; set; }

        [Display(Name = "Resolution Notes")]
        [StringLength(2000)]
        public string? ResolutionNotes { get; set; }

        [Display(Name = "Tags")]
        public List<string> Tags { get; set; } = new();
    }

    public class CreateTicketViewModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Subject")]
        public string Subject { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Priority")]
        public TicketPriority Priority { get; set; } = TicketPriority.Medium;

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Display(Name = "Assigned To")]
        public int? AssignedTo { get; set; }

        [Display(Name = "Tags")]
        public List<string> Tags { get; set; } = new();
    }

    public class UpdateTicketViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Subject")]
        public string Subject { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public TicketStatus Status { get; set; }

        [Display(Name = "Priority")]
        public TicketPriority Priority { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Display(Name = "Assigned To")]
        public int? AssignedTo { get; set; }

        [Display(Name = "Resolution Notes")]
        [StringLength(2000)]
        public string? ResolutionNotes { get; set; }

        [Display(Name = "Tags")]
        public List<string> Tags { get; set; } = new();
    }
}