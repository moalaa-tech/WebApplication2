using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.MarketingAutomation
{
    public class EmailTemplateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Template Name")]
        public string Name { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Created On")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Last Modified On")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? LastModifiedDate { get; set; }
    }
}
