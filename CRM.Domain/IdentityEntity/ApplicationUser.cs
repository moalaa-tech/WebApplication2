using CRM.Domain.Entities.DocumentManagement;
using CRM.Domain.Entities.SalesManagement;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Domain.IdentityEntity
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [MaxLength(256)]
        public required string NameAR { get; set; }


        public int OpportunitiesId { get; set; }
        public ICollection<Opportunity>? Opportunities { get; set; }

        public ICollection<DocumentShare>? DocumentShares { get; set; }

    }
}
