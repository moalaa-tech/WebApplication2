using CRM.WebApp.DTOs.Accounting;

namespace CRM.WebApp.Services.Finance_Accounting
{
    public interface IJournalService
    {
        Task<JournalEntryDto> CreateJournalEntryAsync(CreateJournalEntryDto journalEntryDto);
        Task PostJournalEntryAsync(int id);
        Task<IEnumerable<JournalEntryDto>> GetUnpostedEntriesAsync();
        Task<IEnumerable<JournalEntryDto>> GetJournalEntriesAsync(DateTime? fromDate, DateTime? toDate, bool? postedOnly);
        Task<JournalEntryDto> GetByIdAsync(int id);
    }
}
