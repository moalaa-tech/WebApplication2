using CRM.Domain.Base;
using CRM.Domain.Enums.ProjectManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.ProjectManagment
{
    public class TaskDependency : BaseEntity
    {
        public int TaskId { get; set; }
        public ProjectTask Task { get; set; }
        public int DependsOnTaskId { get; set; }
        public ProjectTask DependsOnTask { get; set; }
        public DependencyType DependencyType { get; set; }
        public string Description { get; set; }
    }
}
