using CRM.Domain.Base;
using CRM.Domain.Entities.SupplyChainManagement;
using CRM.Domain.Enums.ProjectManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.ProjectManagment
{
    public class ProjectTask : BaseEntity
    {
        public string TaskName { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int? DemandPlanId { get; set; }
        public DemandPlan DemandPlan { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string AssignedTo { get; set; }
        public decimal EstimatedHours { get; set; }
        public decimal ActualHours { get; set; }
        public decimal CompletionPercentage { get; set; }
        public string Notes { get; set; }
        public int? ParentTaskId { get; set; }
        public ProjectTask ParentTask { get; set; }
        public ICollection<ProjectTask> SubTasks { get; set; }
        public ICollection<TaskDependency> Dependencies { get; set; }
    }
}
