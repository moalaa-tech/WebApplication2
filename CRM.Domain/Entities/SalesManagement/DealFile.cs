using CRM.Domain.Base;

namespace CRM.Domain.Entities.SalesManagement
{
    public class DealFile : BaseEntity
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int DealId { get; set; }
        public Deal Deal { get; set; }
    }
}
