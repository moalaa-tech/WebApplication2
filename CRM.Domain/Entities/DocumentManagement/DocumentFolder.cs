using CRM.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.DocumentManagement
{
    public class DocumentFolder : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int? ParentFolderId { get; set; }
        public DocumentFolder ParentFolder { get; set; }

        public string Path { get; set; }

        public bool IsSystemFolder { get; set; }

        public ICollection<Document> Documents { get; set; }
        public ICollection<DocumentFolder> SubFolders { get; set; }
    }
}
