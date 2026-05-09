using CRM.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;


namespace CRM.Domain.Entities.HR
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public decimal Budget { get; set; }

        public DateTime EstablishedDate { get; set; }

        [NotMapped]
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }
    }
}
