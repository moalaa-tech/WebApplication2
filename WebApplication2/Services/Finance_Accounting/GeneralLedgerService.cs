using AutoMapper;
using CRM.Domain.Entities.Accounting;
using CRM.WebApp.DTOs.Accounting;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.Finance_Accounting
{
    public class GeneralLedgerService : IGeneralLedgerService
    {
        private readonly IRepository<GLAccount> _repository;
        private readonly IMapper _mapper;

        public GeneralLedgerService(IRepository<GLAccount> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GeneralLedgerDTO>> GetAllAsync()
        {
            var ledgers = await _repository.GetAllAsync().ToListAsync();
            return _mapper.Map<IEnumerable<GeneralLedgerDTO>>(ledgers);
        }

        public async Task<GeneralLedgerDTO> GetByIdAsync(int id)
        {
            var ledger = await _repository.GetByIdAsync(id);
            return _mapper.Map<GeneralLedgerDTO>(ledger);
        }

        public async Task CreateAsync(CreateGeneralLedgerDto generalLedgerDto)
        {
            var ledger = _mapper.Map<GLAccount>(generalLedgerDto);           
            await _repository.AddAsync(ledger);
            await _repository.SaveChangesAsync();

        }

        public async Task UpdateAsync(GeneralLedgerDTO generalLedgerDto)
        {
            var existingLedger = await _repository.GetByIdAsync(generalLedgerDto.Id);
            if (existingLedger == null)
                throw new KeyNotFoundException("General Ledger not found");

            _mapper.Map(generalLedgerDto, existingLedger);
            _repository.Update(existingLedger);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ledger = await _repository.GetByIdAsync(id);
            if (ledger == null)
                throw new KeyNotFoundException("General Ledger not found");

            _repository.Delete(ledger);
            await _repository.SaveChangesAsync();

        }

        public async Task PostTransactionAsync(int id)
        {
            var ledger = await _repository.GetByIdAsync(id);
            if (ledger == null)
                throw new KeyNotFoundException("General Ledger not found");

            //ledger.IsPosted = true;
            _repository.Update(ledger);
            await _repository.SaveChangesAsync();

        }
    }
}
