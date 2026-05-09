using AutoMapper;
using CRM.Domain.Entities.ProjectManagment;
using CRM.Domain.Enums;
using CRM.WebApp.DTOs.Project;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.ProjectManagment
{
    public class TimeEntryService : ITimeEntryService
    {
        private readonly IRepository<TimeEntry> _timeEntryRepository;
        private readonly IRepository<JobPhase> _phaseRepository;
        private readonly IMapper _mapper;

        public TimeEntryService(
            IRepository<TimeEntry> timeEntryRepository,
            IRepository<JobPhase> phaseRepository,
            IMapper mapper)
        {
            _timeEntryRepository = timeEntryRepository;
            _phaseRepository = phaseRepository;
            _mapper = mapper;
        }

        public async Task<TimeEntryDto> GetTimeEntryByIdAsync(int id)
        {
            var entry = await _timeEntryRepository.GetByIdAsync(id);
            if (entry == null) throw new KeyNotFoundException($"Time entry with ID {id} not found.");
            return _mapper.Map<TimeEntryDto>(entry);
        }

        public async Task<IEnumerable<TimeEntryDto>> GetTimeEntriesByPhaseAsync(int phaseId)
        {
            var entries = await _timeEntryRepository.GetAsync(t => t.JobPhaseId == phaseId);
            return _mapper.Map<IEnumerable<TimeEntryDto>>(entries);
        }

        public async Task<IEnumerable<TimeEntryDto>> GetTimeEntriesByUserAsync(string userId, DateTime? fromDate, DateTime? toDate)
        {
            var query = _timeEntryRepository.GetAll().Where(t => t.UserId == Convert.ToInt32(userId));

            if (fromDate.HasValue) query = query.Where(t => t.EntryDate >= fromDate.Value);
            if (toDate.HasValue) query = query.Where(t => t.EntryDate <= toDate.Value);

            var entries = await query.ToListAsync();
            return _mapper.Map<IEnumerable<TimeEntryDto>>(entries);
        }

        public async Task<TimeEntryDto> CreateTimeEntryAsync(CreateTimeEntryDto entryDto)
        {
            var phase = await _phaseRepository.GetByIdAsync(entryDto.JobPhaseId);
            if (phase == null) throw new KeyNotFoundException($"Phase with ID {entryDto.JobPhaseId} not found.");

            var entry = _mapper.Map<TimeEntry>(entryDto);
            await _timeEntryRepository.AddAsync(entry);
            return _mapper.Map<TimeEntryDto>(entry);
        }

        public async Task UpdateTimeEntryAsync(UpdateTimeEntryDto entryDto)
        {
            var entry = await _timeEntryRepository.GetByIdAsync(entryDto.Id);
            if (entry == null) throw new KeyNotFoundException($"Time entry with ID {entryDto.Id} not found.");

            _mapper.Map(entryDto, entry);
            _timeEntryRepository.Update(entry);
        }

        public async Task DeleteTimeEntryAsync(int id)
        {
            var entry = await _timeEntryRepository.GetByIdAsync(id);
            if (entry == null) throw new KeyNotFoundException($"Time entry with ID {id} not found.");

            _timeEntryRepository.Delete(entry);
        }

        public async Task UpdateTimeEntryStatusAsync(int entryId, TimeEntryStatus status)
        {
            var entry = await _timeEntryRepository.GetByIdAsync(entryId);
            if (entry == null) throw new KeyNotFoundException($"Time entry with ID {entryId} not found.");

            entry.Status = status;
            _timeEntryRepository.Update(entry);
        }

        public async Task<decimal> GetTotalHoursByPhaseAsync(int phaseId, DateTime? fromDate, DateTime? toDate)
        {
            var query = _timeEntryRepository.GetAll()
                .Where(t => t.JobPhaseId == phaseId);

            if (fromDate.HasValue) query = query.Where(t => t.EntryDate >= fromDate.Value);
            if (toDate.HasValue) query = query.Where(t => t.EntryDate <= toDate.Value);

            return await query.SumAsync(t => t.Hours);
        }
    }
}
