using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.Project
{
    public class ProjectDocumentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DocumentType { get; set; }

        [Display(Name = "Uploaded")]
        [DataType(DataType.Date)]
        public DateTime UploadDate { get; set; }

        public string FileType { get; set; }
        public string FileSizeDisplay { get; set; }
        public bool IsApproved { get; set; }
    }
}
