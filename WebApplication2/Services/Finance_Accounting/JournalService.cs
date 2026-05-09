using AutoMapper;
using CRM.Domain.Entities.Accounting;
using CRM.WebApp.DTOs.Accounting;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.Finance_Accounting
{
    public class JournalService : IJournalService
    {
        private readonly IRepository<JournalEntry> _journalEntryRepository;
        private readonly IRepository<GLAccount> _glAccountRepository;
        private readonly IMapper _mapper;

        public JournalService(
            IRepository<JournalEntry> journalEntryRepository,
            IRepository<GLAccount> glAccountRepository,
            IMapper mapper)
        {
            _journalEntryRepository = journalEntryRepository;
            _glAccountRepository = glAccountRepository;
            _mapper = mapper;
        }

        public async Task<JournalEntryDto> CreateJournalEntryAsync(CreateJournalEntryDto journalEntryDto)
        {
            // Validate debit = credit
            var totalDebit = journalEntryDto.Lines.Sum(l => l.Debit);
            var totalCredit = journalEntryDto.Lines.Sum(l => l.Credit);

            if (totalDebit != totalCredit)
            {
                throw new InvalidOperationException("Total debits must equal total credits");
            }

            var journalEntry = _mapper.Map<JournalEntry>(journalEntryDto);

            // Validate all accounts exist
            foreach (var line in journalEntry.Lines)
            {
                var account = await _glAccountRepository.GetByIdAsync(line.AccountId);
                if (account == null)
                {
                    throw new ArgumentException($"Account with ID {line.AccountId} not found");
                }
            }

            await _journalEntryRepository.AddAsync(journalEntry);
            return _mapper.Map<JournalEntryDto>(journalEntry);
        }

        public async Task PostJournalEntryAsync(int id)
        {
            var journalEntry = await _journalEntryRepository.GetByIdAsync(id);
            if (journalEntry == null)
            {
                throw new ArgumentException("Journal entry not found");
            }

            if (journalEntry.IsPosted)
            {
                throw new InvalidOperationException("Journal entry is already posted");
            }

            journalEntry.IsPosted = true;
            _journalEntryRepository.Update(journalEntry);
        }

        public async Task<IEnumerable<JournalEntryDto>> GetUnpostedEntriesAsync()
        {
            var entries = await _journalEntryRepository.GetAllAsync(entry => entry.IsPosted == false).ToListAsync();
            return _mapper.Map<IEnumerable<JournalEntryDto>>(entries);
        }

        public async Task<IEnumerable<JournalEntryDto>> GetJournalEntriesAsync(DateTime? fromDate, DateTime? toDate, bool? postedOnly)
        {
            IQueryable<JournalEntry> query = _journalEntryRepository.GetAll();

            // Apply date filters if provided
            if (fromDate.HasValue)
            {
                query = query.Where(e => e.EntryDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(e => e.EntryDate <= toDate.Value);
            }

            // Apply posted status filter if provided
            if (postedOnly.HasValue)
            {
                query = query.Where(e => e.IsPosted == postedOnly.Value);
            }

            // Order the results (optional)
            query = query.OrderBy(e => e.EntryDate);

            // Execute the query asynchronously
            var result = await query.ToListAsync();
            var ResultDto = _mapper.Map<IEnumerable<JournalEntryDto>>(result);
            return ResultDto;

        }

        public async Task<JournalEntryDto> GetByIdAsync(int id)
        {
            var entry = await _journalEntryRepository.GetByIdAsync(id);
            if (entry == null) return null;

            var entryDto = _mapper.Map<JournalEntryDto>(entry);
            var lines = await _journalEntryRepository.GetAsync(jl => jl.Id == id);
            entryDto.Lines = _mapper.Map<List<JournalLineDto>>(lines);

            return entryDto;
        }
    }

}
