using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.CustomerService
{
    public class ServiceRequestViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Request Type")]
        public string RequestType { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Resolution Notes")]
        public string ResolutionNotes { get; set; }
    }
}