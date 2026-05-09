using CRM.Domain.Base;


namespace CRM.Domain.Entities.DocumentManagement
{
    public class DocumentCategory : BaseEntity
    {
       
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Description { get; set; }       
        public string Slug { get; set; }
        public int Order { get; set; }
        public int? ParentCategoryId { get; set; }
        public DocumentCategory ParentCategory { get; set; }

        public ICollection<Document> Documents { get; set; }
        public ICollection<DocumentCategory> SubCategories { get; set; }
    }
}
