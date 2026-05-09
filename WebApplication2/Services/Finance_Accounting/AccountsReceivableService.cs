using CRM.WebApp.DTOs.Accounting;
using CRM.WebApp.Services.Interfaces;

namespace CRM.WebApp.Services.Finance_Accounting
{
    public class AccountsReceivableService : IAccountsReceivableService
    {
        private static readonly List<AccountsReceivableDto> _receivables = new();
        private static int _nextId = 1;

        public Task<IEnumerable<AccountsReceivableDto>> GetAllAsync()
        {
            return Task.FromResult(_receivables.AsEnumerable());
        }

        public Task<AccountsReceivableDto> GetByIdAsync(int id)
        {
            var receivable = _receivables.FirstOrDefault(r => r.Id == id);
            return Task.FromResult(receivable);
        }

        public Task CreateAsync(CreateAccountsReceivableDto dto)
        {
            var receivable = new AccountsReceivableDto
            {
                Id = _nextId++,
                CustomerName = dto.CustomerName,
                InvoiceNumber = dto.InvoiceNumber,
                InvoiceDate = dto.InvoiceDate,
                AmountDue = dto.AmountDue,
                DueDate = dto.DueDate
            };
            _receivables.Add(receivable);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(AccountsReceivableDto dto)
        {
            var receivable = _receivables.FirstOrDefault(r => r.Id == dto.Id);
            if (receivable != null)
            {
                receivable.CustomerName = dto.CustomerName;
                receivable.InvoiceNumber = dto.InvoiceNumber;
                receivable.InvoiceDate = dto.InvoiceDate;
                receivable.AmountDue = dto.AmountDue;
                receivable.DueDate = dto.DueDate;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var receivable = _receivables.FirstOrDefault(r => r.Id == id);
            if (receivable != null)
            {
                _receivables.Remove(receivable);
            }
            return Task.CompletedTask;
        }
    }
}