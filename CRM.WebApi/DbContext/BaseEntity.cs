namespace CRM.WebApi.DbContext
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime? DateModified { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
