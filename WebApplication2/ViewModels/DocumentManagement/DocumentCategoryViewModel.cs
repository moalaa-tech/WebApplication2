namespace CRM.WebApp.ViewModels.DocumentManagement
{
    public class DocumentCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ParentName { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public int DocumentCount { get; set; }

        public string Slug { get; set; }
        public int? ParentId { get; set; }
        public DateTime CreatedAt { get; set; }

       
    }
}
