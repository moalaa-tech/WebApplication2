namespace CRM.Domain.Entities.HR
{
    public class EmployeeTitle
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int JobTitleId { get; set; }
        public JobTitle JobTitle { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
