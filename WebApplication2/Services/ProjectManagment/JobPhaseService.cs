using AutoMapper;
using CRM.Domain.Entities.ProjectManagment;
using CRM.Domain.Enums;
using CRM.WebApp.DTOs.Project;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.ProjectManagment
{
    public class JobPhaseService : IJobPhaseService
    {
        private readonly IRepository<JobPhase> _phaseRepository;
        private readonly IRepository<TimeEntry> _timeEntryRepository;
        private readonly IRepository<PhaseExpense> _expenseRepository;
        private readonly IMapper _mapper;

        public JobPhaseService(
            IRepository<JobPhase> phaseRepository,
            IRepository<TimeEntry> timeEntryRepository,
            IRepository<PhaseExpense> expenseRepository,
            IMapper mapper)
        {
            _phaseRepository = phaseRepository;
            _timeEntryRepository = timeEntryRepository;
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<JobPhaseDto> GetPhaseByIdAsync(int id)
        {
            if (id == 0 )
            {
                return new JobPhaseDto();
            }
            var phase = await _phaseRepository.GetByIdAsync(id);
            if (phase == null) throw new KeyNotFoundException($"Phase with ID {id} not found.");

            var phaseDto = _mapper.Map<JobPhaseDto>(phase);
            phaseDto.TimeEntries = await GetTimeEntriesForPhaseAsync(id, null, null);
            phaseDto.Expenses = await GetExpensesForPhaseAsync(id, null, null);

            return phaseDto;
        }

        public async Task<IEnumerable<JobPhaseDto>> GetPhasesByProjectAsync(int projectId)
        {
            var phases = await _phaseRepository.GetAsync(p => p.ProjectId == projectId);
            return _mapper.Map<IEnumerable<JobPhaseDto>>(phases);
        }

        public async Task<JobPhaseDto> CreatePhaseAsync(CreateJobPhaseDto phaseDto)
        {
            var phase = _mapper.Map<JobPhase>(phaseDto);
            await _phaseRepository.AddAsync(phase);
            return _mapper.Map<JobPhaseDto>(phase);
        }

        public async Task UpdatePhaseAsync(UpdateJobPhaseDto phaseDto)
        {
            var phase = await _phaseRepository.GetByIdAsync(phaseDto.Id);
            if (phase == null) throw new KeyNotFoundException($"Phase with ID {phaseDto.Id} not found.");

            _mapper.Map(phaseDto, phase);
            _phaseRepository.Update(phase);
        }

        public async Task DeletePhaseAsync(int id)
        {
            var phase = await _phaseRepository.GetByIdAsync(id);
            if (phase == null) throw new KeyNotFoundException($"Phase with ID {id} not found.");

            _phaseRepository.Delete(phase);
        }

        public async Task<PhaseCostSummaryDto> GetPhaseCostSummaryAsync(int phaseId)
        {
            var phase = await _phaseRepository.GetByIdAsync(phaseId);
            if (phase == null) throw new KeyNotFoundException($"Phase with ID {phaseId} not found.");

            var timeEntries = await _timeEntryRepository.GetByCondition(t => t.JobPhaseId == phaseId).ToListAsync();
            var expenses = await _expenseRepository.GetByCondition(e => e.JobPhaseId == phaseId).ToListAsync();

            return new PhaseCostSummaryDto
            {
                PhaseId = phaseId,
                PhaseName = phase.Name,
                EstimatedHours = phase.EstimatedHours,
                EstimatedCost = phase.EstimatedCost,
                ActualHours = timeEntries.Sum(t => t.Hours),
                ActualCost = timeEntries.Sum(t => t.Hours * t.Rate) + expenses.Sum(e => e.Amount),
                LaborCost = timeEntries.Sum(t => t.Hours * t.Rate),
                ExpenseCost = expenses.Sum(e => e.Amount),
                Variance = phase.EstimatedCost - (timeEntries.Sum(t => t.Hours * t.Rate) + expenses.Sum(e => e.Amount))
            };
        }

        public async Task UpdatePhaseStatusAsync(int phaseId, PhaseStatus status)
        {
            var phase = await _phaseRepository.GetByIdAsync(phaseId);
            if (phase == null) throw new KeyNotFoundException($"Phase with ID {phaseId} not found.");

            phase.Status = status;
            _phaseRepository.Update(phase);
        }

        public async Task<IEnumerable<TimeEntryDto>> GetTimeEntriesForPhaseAsync(int phaseId, DateTime? fromDate, DateTime? toDate)
        {
            var query = _timeEntryRepository.GetAll()
                .Where(t => t.JobPhaseId == phaseId);

            if (fromDate.HasValue) query = query.Where(t => t.EntryDate >= fromDate.Value);
            if (toDate.HasValue) query = query.Where(t => t.EntryDate <= toDate.Value);

            var entries = await query.ToListAsync();
            return _mapper.Map<IEnumerable<TimeEntryDto>>(entries);
        }

        public async Task<IEnumerable<PhaseExpenseDto>> GetExpensesForPhaseAsync(int phaseId, DateTime? fromDate, DateTime? toDate)
        {
            var query = _expenseRepository.GetAll()
                .Where(e => e.JobPhaseId == phaseId);

            if (fromDate.HasValue) query = query.Where(e => e.ExpenseDate >= fromDate.Value);
            if (toDate.HasValue) query = query.Where(e => e.ExpenseDate <= toDate.Value);

            var expenses = await query.ToListAsync();
            return _mapper.Map<IEnumerable<PhaseExpenseDto>>(expenses);
        }
    }
}
