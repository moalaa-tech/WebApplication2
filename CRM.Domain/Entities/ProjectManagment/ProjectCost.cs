using CRM.Domain.Base;
using CRM.Domain.Enums.ProjectManagment;

namespace CRM.Domain.Entities.ProjectManagment
{
    public class ProjectCost : BaseEntity
    {
        public int ProjectId { get; set; }
        public CostType Type { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateIncurred { get; set; }

        // Navigation properties
        public Project Project { get; set; }
    }
}
