using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.DocumentManagement
{
    public class DocumentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Type")]
        public string DocumentType { get; set; }

        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Display(Name = "Size")]
        public long FileSize { get; set; }

        [Display(Name = "Extension")]
        public string FileExtension { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Display(Name = "Folder")]
        public string FolderName { get; set; }

        [Display(Name = "Version")]
        public int Version { get; set; }

        [Display(Name = "Uploaded By")]
        public string UploadedBy { get; set; }

        [Display(Name = "Upload Date")]
        public DateTime UploadDate { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "Expiry Date")]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name = "Public")]
        public bool IsPublic { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Downloads")]
        public int DownloadCount { get; set; }

        public string FileSizeFormatted { get; set; }
        public string Tags { get; set; }
    }
}
