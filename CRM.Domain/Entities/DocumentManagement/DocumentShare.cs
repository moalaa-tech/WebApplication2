using CRM.Domain.Base;
using CRM.Domain.IdentityEntity;
using System;


namespace CRM.Domain.Entities.DocumentManagement
{
    public class DocumentShare : BaseEntity
    {
        public int DocumentId { get; set; }
        public Document Document { get; set; }

        public int SharedWithUserId { get; set; }
        public virtual ApplicationUser SharedWithUser { get; set; }

        public int SharedByUserId { get; set; }
        public virtual ApplicationUser SharedByUser { get; set; }

        public DateTime ShareDate { get; set; } = DateTime.UtcNow;

        public DateTime? ExpiryDate { get; set; }

        public string PermissionLevel { get; set; } // View, Edit, Delete

        public bool CanDownload { get; set; } = true;
    }
}
