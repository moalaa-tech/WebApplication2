using AutoMapper;
using CRM.Domain.Entities.ProjectManagment;
using CRM.Domain.Enums;
using CRM.WebApp.DTOs.Project;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.ProjectManagment
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<JobPhase> _phaseRepository;
        private readonly IRepository<TimeEntry> _timeEntryRepository;
        private readonly IRepository<PhaseExpense> _expenseRepository;
        private readonly IMapper _mapper;

        public ProjectService(
            IRepository<Project> projectRepository,
            IRepository<JobPhase> phaseRepository,
            IRepository<TimeEntry> timeEntryRepository,
            IRepository<PhaseExpense> expenseRepository,
            IMapper mapper
            )
        {
            _projectRepository = projectRepository;
            _phaseRepository = phaseRepository;
            _timeEntryRepository = timeEntryRepository;
            _expenseRepository = expenseRepository;
            _mapper = mapper;

        }

        public async Task CreateProjectAsync(CreateProjectDto project)
        {
            var pro = _mapper.Map<Project>(project);
            await _projectRepository.AddAsync(pro);
            await _projectRepository.SaveChangesAsync();
        }


        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync(a => a.Customer).ToListAsync();
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }


        public Task<IEnumerable<PhaseExpenseDto>> GetExpensesForProjectAsync(int projectId, DateTime? fromDate, DateTime? toDate)
        {
            throw new NotImplementedException();
        }
        private async Task<decimal> GetTotalProjectHoursAsync(int projectId)
        {
            return await _timeEntryRepository.GetAll()
                .Where(t => t.JobPhase.ProjectId == projectId)
                .SumAsync(t => t.Hours);
        }

        private async Task<decimal> GetTotalProjectExpensesAsync(int projectId)
        {
            var laborCost = await _timeEntryRepository.GetAll()
                .Where(t => t.JobPhase.ProjectId == projectId)
                .SumAsync(t => t.Hours * t.Rate);

            var expenseCost = await _expenseRepository.GetAll()
                .Where(e => e.JobPhase.ProjectId == projectId)
                .SumAsync(e => e.Amount);

            return laborCost + expenseCost;
        }


        public async Task<ProjectDto> GetProjectByIdAsync(int id)
        {
            var project = await _projectRepository.GetAll()
                .Include(p => p.Customer)
                .Include(p => p.Phases)
                .Include(p => p.Notes)
                    .ThenInclude(n => n.User)
                .Include(p => p.Documents)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found.");
            }

            var projectDto = _mapper.Map<ProjectDto>(project);

            // Calculate project statistics
            projectDto.TotalHours = await GetTotalProjectHoursAsync(id);
            projectDto.TotalExpenses = await GetTotalProjectExpensesAsync(id);
            projectDto.RemainingBudget = project.Budget - projectDto.TotalExpenses;

            return projectDto;
        }

        public async Task<ProjectCostSummaryDto> GetProjectCostSummaryAsync(int projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {projectId} not found.");
            }

            var phases = await _phaseRepository.GetAll()
                .Where(p => p.ProjectId == projectId)
                .ToListAsync();

            var timeEntries = await _timeEntryRepository.GetAll()
                .Where(t => t.JobPhase.ProjectId == projectId)
                .ToListAsync();

            var expenses = await _expenseRepository.GetAll()
                .Where(e => e.JobPhase.ProjectId == projectId)
                .ToListAsync();

            var summary = new ProjectCostSummaryDto
            {
                ProjectId = project.Id,
                ProjectName = project.Name,
                ProjectCode = project.ProjectCode,
                Budget = project.Budget,
                TotalEstimatedCost = phases.Sum(p => p.EstimatedCost),
                TotalActualCost = timeEntries.Sum(t => t.Hours * t.Rate) + expenses.Sum(e => e.Amount),
                LaborCost = timeEntries.Sum(t => t.Hours * t.Rate),
                ExpenseCost = expenses.Sum(e => e.Amount),
                TotalHours = timeEntries.Sum(t => t.Hours),
                EstimatedHours = phases.Sum(p => p.EstimatedHours),
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                TotalPhases = phases.Count,
                CompletedPhases = phases.Count(p => p.Status == PhaseStatus.Completed),
                ActivePhases = phases.Count(p => p.Status == PhaseStatus.Active),
                PlannedPhases = phases.Count(p => p.Status == PhaseStatus.Planning)
            };

            return summary;
        }

        public async Task<IEnumerable<TimeEntryDto>> GetTimeEntriesForProjectAsync(int projectId, DateTime? fromDate, DateTime? toDate)
        {
            var query = _timeEntryRepository.GetAll()
                .Include(t => t.JobPhase)
                .Include(t => t.User)
                .Where(t => t.JobPhase.ProjectId == projectId);

            if (fromDate.HasValue)
            {
                query = query.Where(t => t.EntryDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(t => t.EntryDate <= toDate.Value);
            }

            var timeEntries = await query
                .OrderByDescending(t => t.EntryDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TimeEntryDto>>(timeEntries);
        }

        public async Task UpdateProjectAsync(UpdateProjectDto projectDto)
        {
            var project = await _projectRepository.GetByIdAsync(projectDto.Id);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {projectDto.Id} not found.");
            }

            _mapper.Map(projectDto, project);

            // Update modified date
            project.DateModified = DateTime.UtcNow;

            _projectRepository.Delete(project);
        }
        public async Task DeleteProjectAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found.");
            }

            // Check if project has any time entries or expenses
            var hasTimeEntries = await _timeEntryRepository.GetAll()
                .AnyAsync(t => t.JobPhase.ProjectId == id);

            var hasExpenses = await _expenseRepository.GetAll()
                .AnyAsync(e => e.JobPhase.ProjectId == id);

            if (hasTimeEntries || hasExpenses)
            {
                throw new InvalidOperationException(
                    "Cannot delete project with existing time entries or expenses. Archive instead.");
            }

            _projectRepository.Delete(project);
        }
    }
}
