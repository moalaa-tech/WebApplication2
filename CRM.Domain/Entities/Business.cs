using CRM.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Entities
{
    public class Business : BaseEntity
    {
        [StringLength(60, MinimumLength = 2)]
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }
        public string NameAr { get; set; }


        public virtual ICollection<Company> Companies { get; set; }
    }
}
